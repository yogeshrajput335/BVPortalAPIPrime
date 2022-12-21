using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using BVPortalApi.DTO;
using BVPortalApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BVPortalApi.Controllers
{
    [ApiController]
    [Route("api/[controller]"), Authorize(Roles = "ADMIN")]
    public class InvoiceProductController : ControllerBase
    {
        private readonly BVContext DBContext;

        public InvoiceProductController(BVContext DBContext)
        {
            this.DBContext = DBContext;
        }

        [HttpGet("GetInvoiceProduct")]
        public async Task<ActionResult<List<InvoiceProductDTO>>> Get()
        {
            var List = await DBContext.InvoiceProduct.Select(
                s => new InvoiceProductDTO
                {
                    Id = s.Id,
                    ProductId = s.ProductId,
                    ServiceId = s.ServiceId,
                    InvoiceId = s.InvoiceId,
                    ItemTypeId = s.ItemTypeId,
                    Unit = s.Unit,
                    Rate = s.Rate,
                    Quantity = s.Quantity,
                    Total = s.Total,
                    Product = s.Product.ProductName,
                    Service = s.Service.ServiceName,
                    IsProduct = s.IsProduct
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

        [HttpPost("InsertInvoiceProduct")]
        public async Task < HttpStatusCode > InsertInvoiceProduct(InvoiceProductDTO s) {
            var entity = new InvoiceProduct() {
                    ProductId = s.ProductId,
                    ServiceId = s.ServiceId,
                    InvoiceId = s.InvoiceId,
                    ItemTypeId = s.ItemTypeId,
                    Unit = s.Unit,
                    Rate = s.Rate,
                    Quantity = s.Quantity,
                    Total = s.Total,
                    IsProduct = s.IsProduct
            };
            DBContext.InvoiceProduct.Add(entity);
            await DBContext.SaveChangesAsync();
            return HttpStatusCode.Created;
        }

        [HttpPut("InvoiceProduct")]
        public async Task<HttpStatusCode> UpdateInvoiceProduct(InvoiceProductDTO s) {
            var entity = await DBContext.InvoiceProduct.FirstOrDefaultAsync(x => x.Id == s.Id);
            entity.ProductId = s.ProductId;
            entity.ServiceId = s.ServiceId;
            entity.InvoiceId = s.InvoiceId;
            entity.ItemTypeId = s.ItemTypeId;
            entity.Unit = s.Unit;
            entity.Rate = s.Rate;
            entity.Quantity = s.Quantity;
            entity.Total = s.Total;
            entity.IsProduct = s.IsProduct;
            await DBContext.SaveChangesAsync();
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
            return HttpStatusCode.OK;
        }
    }
}