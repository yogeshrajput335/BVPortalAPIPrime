using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using BVPortalApi.CommonFeatures;
using BVPortalApi.CommonFeatures.Contracts;
using BVPortalApi.DTO;
using BVPortalApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BVPortalApi.Controllers
{
    [ApiController]
    [Route("api/[controller]"), Authorize(Roles = "ADMIN")]
    public class InvoiceController : ControllerBase
    {
        private readonly BVContext DBContext;

        public InvoiceController(BVContext DBContext)
        {
            this.DBContext = DBContext;
        }

        [HttpGet("GetInvoice")]
        public async Task<ActionResult<List<InvoiceDTO>>> Get()
        {
            var List = await DBContext.Invoice.Select(
                s => new InvoiceDTO
                {
                    Id = s.Id,
                    InvoiceNo = s.InvoiceNo,
                    CreatedDate = s.CreatedDate,
                    DueDate = s.DueDate,
                    ClientId = s.ClientId,
                    ClientName = s.Client.ClientName,
                    FromLine1 = s.FromLine1,
                    FromLine2 = s.FromLine2,
                    FromLine3 = s.FromLine3,
                    Term = s.Term,
                    Status = s.Status,
                    Products =  (DBContext.InvoiceProduct.Where(x=>x.InvoiceId == s.Id).Select(
                    s => new InvoiceProductDTO
                    {
                        Id = s.Id,
                        EmployeeId = s.EmployeeId,
                        ProjectId = s.ProjectId,
                        InvoiceId = s.InvoiceId,
                        Employee = s.Employee.FirstName+" "+s.Employee.LastName,
                        Project = s.Project.ProjectName,
                        ProjectType = s.Project.ProjectType,
                        PerHourCost = s.PerHourCost,
                        TotalHours = s.TotalHours,
                        TotalCost = s.TotalCost,
                    }).ToList() )               
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

        [HttpGet("GetInvoiceById/{id}")]
        public async Task<ActionResult<InvoiceDTO>> GetInvoiceById(int id)
        {
            var List = await DBContext.Invoice.Where(x=>x.Id == id).Select(
                s => new InvoiceDTO
                {
                    Id = s.Id,
                    InvoiceNo = s.InvoiceNo,
                    CreatedDate = s.CreatedDate,
                    DueDate = s.DueDate,
                    ClientId = s.ClientId,
                    ClientName = s.Client.ClientName,
                    FromLine1 = s.FromLine1,
                    FromLine2 = s.FromLine2,
                    FromLine3 = s.FromLine3,
                    Term = s.Term,
                    Status = s.Status,
                    // Products = s.InvoiceProduct.Select(s => new InvoiceDTO
                    //             {
                    //                 Emploe
                    //             })

                }
            ).FirstOrDefaultAsync();
            
            if (List==null)
            {
                return NotFound();
            }
            else
            {
                List.Products = DBContext.InvoiceProduct.Where(x=>x.InvoiceId == id).Select(
                s => new InvoiceProductDTO
                {
                    Id = s.Id,
                    EmployeeId = s.EmployeeId,
                    ProjectId = s.ProjectId,
                    InvoiceId = s.InvoiceId,
                    Employee = s.Employee.FirstName+" "+s.Employee.LastName,
                    Project = s.Project.ProjectName,
                    ProjectType = s.Project.ProjectType,
                    PerHourCost = s.PerHourCost,
                    TotalHours = s.TotalHours,
                    TotalCost = s.TotalCost,
                }).ToList();
                return List;
            }
        }

        [HttpPost("InsertInvoice")]
        public async Task < HttpStatusCode > InsertInvoice(InvoiceDTO s) {
            var entity = new Invoice() {
                    InvoiceNo = s.InvoiceNo,
                    CreatedDate = s.CreatedDate,
                    DueDate = s.DueDate,
                    ClientId = s.ClientId,
                    FromLine1 = s.FromLine1,
                    FromLine2 = s.FromLine2,
                    FromLine3 = s.FromLine3,
                    Term = s.Term,
                    Status = s.Status
            };
            DBContext.Invoice.Add(entity);
            await DBContext.SaveChangesAsync();
            List<InvoiceProduct> p = s.Products.Select(
                s => new InvoiceProduct
                {
                    //Id = s.Id,
                    EmployeeId = s.EmployeeId,
                    InvoiceId = entity.Id,
                    ProjectId = s.ProjectId,
                    PerHourCost = s.PerHourCost,
                    TotalHours = s.TotalHours,
                    TotalCost = s.TotalCost
                }
            ).ToList();
            DBContext.InvoiceProduct.AddRange(p);
            await DBContext.SaveChangesAsync();
            return HttpStatusCode.Created;
        }
        [HttpPut("UpdateInvoice")]
        public async Task<HttpStatusCode> UpdateInvoice(InvoiceDTO Invoice) {
            var entity = await DBContext.Invoice.FirstOrDefaultAsync(s => s.Id == Invoice.Id);
            entity.InvoiceNo = Invoice.InvoiceNo;
            entity.CreatedDate = Invoice.CreatedDate;
            entity.DueDate = Invoice.DueDate;
            entity.ClientId = Invoice.ClientId;
            entity.FromLine1 = Invoice.FromLine1;
            entity.FromLine2 = Invoice.FromLine2;
            entity.FromLine3 = Invoice.FromLine3;
            entity.Term = Invoice.Term;

            entity.Status = Invoice.Status;
            await DBContext.SaveChangesAsync();
            IQueryable<InvoiceProduct> ip = DBContext.InvoiceProduct.Where(x=>x.InvoiceId ==Invoice.Id);
            DBContext.InvoiceProduct.RemoveRange(ip);
            List<InvoiceProduct> p = Invoice.Products.Select(
                s => new InvoiceProduct
                {
                    //Id = s.Id,
                    EmployeeId = s.EmployeeId,
                    InvoiceId = entity.Id,
                    ProjectId = s.ProjectId,
                    PerHourCost = s.PerHourCost,
                    TotalHours = s.TotalHours,
                    TotalCost = s.TotalCost
                }
            ).ToList();
            DBContext.InvoiceProduct.AddRange(p);
            await DBContext.SaveChangesAsync();
            return HttpStatusCode.OK;
        }
        
        [HttpDelete("DeleteInvoice/{Id}")]
        public async Task < HttpStatusCode > DeleteInvoice(int Id) {
            var entity = new Invoice() {
                Id = Id
            };
            DBContext.Invoice.Attach(entity);
            DBContext.Invoice.Remove(entity);
            await DBContext.SaveChangesAsync();
            return HttpStatusCode.OK;
        }

        [HttpPost("DeleteInvoices")]
        public  async Task<HttpStatusCode> DeleteInvoices(List<InvoiceDTO> Assets) {
            List<Invoice> entities = Assets.Select(i => new Invoice(){
                Id = i.Id
            }).ToList();
            DBContext.Invoice.AttachRange(entities);
            DBContext.Invoice.RemoveRange(entities);
            await DBContext.SaveChangesAsync();
            return HttpStatusCode.OK;
        }
    }
}