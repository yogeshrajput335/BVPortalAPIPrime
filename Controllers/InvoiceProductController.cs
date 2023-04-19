using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using BVPortalApi.DTO;
using BVPortalApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace BVPortalApi.Controllers
{
    [ApiController]
    [Route("api/[controller]"), Authorize(Roles = "ADMIN")]
    public class InvoiceProductController : ControllerBase
    {
        private const string cacheKey = "invoiceProductList";
        private readonly BVContext DBContext;
        private IMemoryCache _cache;
        private ILogger<InvoiceProductController> _logger;
        private readonly IMapper _mapper;

        public InvoiceProductController(BVContext DBContext, IMemoryCache cache, ILogger<InvoiceProductController> logger, IMapper mapper)
        {
            this.DBContext = DBContext;
            _cache = cache ?? throw new ArgumentNullException(nameof(cache));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper; 
        }

        [HttpGet("GetInvoiceProduct")]
        public async Task<ActionResult<List<InvoiceProductDTO>>> Get()
        {
            _logger.Log(LogLevel.Information, "Trying to fetch the list of Invoice Products from cache.");
            if (_cache.TryGetValue(cacheKey, out List<InvoiceProductDTO> List))
            {
                _logger.Log(LogLevel.Information, "Invoice Product list found in cache.");
                
            }
            else
            {
                _logger.Log(LogLevel.Information, "Invoice Product list not found in cache. Fetching from database.");
                List = _mapper.Map<List<InvoiceProductDTO>>(await DBContext.InvoiceProduct.Include(x=>x.Invoice).ToListAsync());
                
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

        [HttpPost("InsertInvoiceProduct")]
        public async Task < HttpStatusCode > InsertInvoiceProduct(InvoiceProductDTO s) {
            var entity = _mapper.Map<InvoiceProduct>(s);
            DBContext.InvoiceProduct.Add(entity);
            await DBContext.SaveChangesAsync();
            _cache.Remove(cacheKey);
            return HttpStatusCode.Created;
        }

        [HttpPut("InvoiceProduct")]
        public async Task<HttpStatusCode> UpdateInvoiceProduct(InvoiceProductDTO s) {
            var entity = await DBContext.InvoiceProduct.FirstOrDefaultAsync(x => x.Id == s.Id);
            entity.Product = s.Product;
            entity.Service = s.Service;
            entity.InvoiceId = s.InvoiceId;
            entity.ItemTypeId = s.ItemTypeId;
            entity.Unit = s.Unit;
            entity.Rate = s.Rate;
            entity.Quantity = s.Quantity;
            entity.Total = s.Total;
            entity.IsProduct = s.IsProduct;
            await DBContext.SaveChangesAsync();
            _cache.Remove(cacheKey);
            return HttpStatusCode.OK;
        }
        
        [HttpDelete("DeleteInvoiceProduct/{Id}")]
        public async Task < HttpStatusCode > DeleteInvoiceProduct(int Id) {
            var entity = new InvoiceProduct() {
                Id = Id
            };
            DBContext.InvoiceProduct.Attach(entity);
            DBContext.InvoiceProduct.Remove(entity);
            await DBContext.SaveChangesAsync();
            _cache.Remove(cacheKey);
            return HttpStatusCode.OK;
        }
    }
}