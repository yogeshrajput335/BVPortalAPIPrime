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
                    InvoiceId = s.InvoiceId,
                    EmployeeId = s.EmployeeId,
                    ProjectId = s.ProjectId,
                    PerHourCost = s.PerHourCost,
                    TotalHours = s.TotalHours,
                    TotalCost = s.TotalCost,
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
                    InvoiceId = s.InvoiceId,
                    EmployeeId = s.EmployeeId,
                    ProjectId = s.ProjectId,
                    PerHourCost = s.PerHourCost,
                    TotalHours = s.TotalHours,
                    TotalCost = s.TotalCost,
            };
            DBContext.InvoiceProduct.Add(entity);
            await DBContext.SaveChangesAsync();
            return HttpStatusCode.Created;
        }

        [HttpPut("InvoiceProduct")]
        public async Task<HttpStatusCode> UpdateInvoiceProduct(InvoiceProductDTO InvoiceProduct) {
            var entity = await DBContext.InvoiceProduct.FirstOrDefaultAsync(s => s.Id == InvoiceProduct.Id);
            entity.InvoiceId = InvoiceProduct.InvoiceId;
            entity.EmployeeId = InvoiceProduct.EmployeeId;
            entity.ProjectId = InvoiceProduct.ProjectId;
            entity.PerHourCost = InvoiceProduct.PerHourCost;
            entity.TotalHours = InvoiceProduct.TotalHours ;
            entity.TotalCost = InvoiceProduct.TotalCost;
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