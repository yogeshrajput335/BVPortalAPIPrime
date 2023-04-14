using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using BVPortalApi.DTO;
using BVPortalApi.Models;
using BVPortalAPIPrime.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace BVPortalApi.Controllers
{
    [ApiController]
    [Route("api/[controller]"), Authorize(Roles = "ADMIN")]
    public class ProductController : ControllerBase
    {
        private const string cacheKey = "productList";
        private readonly BVContext DBContext;
        private IMemoryCache _cache;
        private ILogger<ProductController> _logger;
        private readonly IMapper _mapper;

        public ProductController(BVContext DBContext, IMemoryCache cache, ILogger<ProductController> logger, IMapper mapper)
        {
            this.DBContext = DBContext;
            _cache = cache ?? throw new ArgumentNullException(nameof(cache));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper; 
        }

        [HttpGet("GetProduct")]
        public async Task<ActionResult<List<ProductDTO>>> Get()
        {
            // var List = await DBContext.Product.Select(
            //     s => new ProductDTO
            //     {
            //         Id = s.Id,
            //         ProductName = s.ProductName,
            //         Unit = s.Unit,
            //         Rate = s.Rate,
            //         Status = s.Status
            //     }
            // ).ToListAsync();
            _logger.Log(LogLevel.Information, "Trying to fetch the list of Products from cache.");
            if (_cache.TryGetValue(cacheKey, out List<ProductDTO> List))
            {
                _logger.Log(LogLevel.Information, "Product list found in cache.");
                
            }
            else
            {
                _logger.Log(LogLevel.Information, "Product list not found in cache. Fetching from database.");
                List = _mapper.Map<List<ProductDTO>>(await DBContext.Product.ToListAsync());
                
                var cacheEntryOptions = new MemoryCacheEntryOptions()
                        .SetSlidingExpiration(TimeSpan.FromSeconds(60))
                        .SetAbsoluteExpiration(TimeSpan.FromSeconds(3600))
                        .SetPriority(CacheItemPriority.Normal)
                        .SetSize(1024);
                _cache.Set(cacheKey, List, cacheEntryOptions);
            }
            if (List.Count < 0)
            {
                return NotFound();
            }
            else
            {
                return List;
            }
        }

        [HttpPost("InsertProduct")]
        public async Task<HttpStatusCode> InsertProduct(ProductDTO s)
        {
            // var entity = new Product()
            // {
            //     ProductName = s.ProductName,
            //     Unit = s.Unit,
            //     Rate = s.Rate,
            //     Status = s.Status
            // };
            var entity = _mapper.Map<Product>(s);
            DBContext.Product.Add(entity);
            await DBContext.SaveChangesAsync();
            _cache.Remove(cacheKey);
            return HttpStatusCode.Created;
        }

        [HttpPut("UpdateProduct")]
        public async Task<HttpStatusCode> UpdateProduct(ProductDTO Product)
        {
            var entity = await DBContext.Product.FirstOrDefaultAsync(s => s.Id == Product.Id);
            entity.ProductName = Product.ProductName;
            entity.Unit = Product.Unit;
            entity.Rate = Product.Rate;
            entity.Status = Product.Status;
            await DBContext.SaveChangesAsync();
            _cache.Remove(cacheKey);
            return HttpStatusCode.OK;
        }

        [HttpDelete("DeleteProduct/{Id}")]
        public async Task<HttpStatusCode> DeleteProduct(int Id)
        {
            var entity = new Product()
            {
                Id = Id
            };
            DBContext.Product.Attach(entity);
            DBContext.Product.Remove(entity);
            await DBContext.SaveChangesAsync();
            _cache.Remove(cacheKey);
            return HttpStatusCode.OK;
        }
        [HttpPost("DeleteProducts")]
        public  async Task<HttpStatusCode> DeleteProducts(List<ProductDTO> products) {
            List<Product> entities = products.Select(i => new Product(){
                Id = i.Id
            }).ToList();
            DBContext.Product.AttachRange(entities);
            DBContext.Product.RemoveRange(entities);
            await DBContext.SaveChangesAsync();
            _cache.Remove(cacheKey);
            return HttpStatusCode.OK;
        }
    }
}