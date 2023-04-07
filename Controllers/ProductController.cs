using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using BVPortalApi.DTO;
using BVPortalApi.Models;
using BVPortalAPIPrime.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BVPortalApi.Controllers
{
    [ApiController]
    [Route("api/[controller]"), Authorize(Roles = "ADMIN")]
    public class ProductController : ControllerBase
    {

        private readonly BVContext DBContext;

        public ProductController(BVContext DBContext)
        {
            this.DBContext = DBContext;
        }

        [HttpGet("GetProduct")]
        public async Task<ActionResult<List<ProductDTO>>> Get()
        {
            var List = await DBContext.Product.Select(
                s => new ProductDTO
                {
                    Id = s.Id,
                    ProductName = s.ProductName,
                    Unit = s.Unit,
                    Rate = s.Rate,
                    Status = s.Status
                }
            ).ToListAsync();

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
            var entity = new Product()
            {
                ProductName = s.ProductName,
                Unit = s.Unit,
                Rate = s.Rate,
                Status = s.Status
            };
            DBContext.Product.Add(entity);
            await DBContext.SaveChangesAsync();
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
            // _cache.Remove(cacheKey);
            return HttpStatusCode.OK;
        }
    }
}