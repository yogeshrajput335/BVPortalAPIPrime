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
    public class CustomerController : ControllerBase
    {

        private readonly BVContext DBContext;

        public CustomerController(BVContext DBContext)
        {
            this.DBContext = DBContext;
        }

        [HttpGet("GetCustomer")]
        public async Task<ActionResult<List<CustomerDTO>>> Get()
        {
            var List = await DBContext.Customer.Select(
                s => new CustomerDTO
                {
                    Id = s.Id,
                    CustomerName = s.CustomerName,
                    AddressLine1 = s.AddressLine1,
                    AddressLine2 = s.AddressLine2,
                    AddressLine3 = s.AddressLine3,
                    EmailAddress = s.EmailAddress,
                    PhoneNumber = s.PhoneNumber,
                    Term = s.Term,
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

        [HttpPost("InsertCustomer")]
        public async Task<HttpStatusCode> InsertCustomer(CustomerDTO s)
        {
            var entity = new Customer()
            {
                CustomerName = s.CustomerName,
                AddressLine1 = s.AddressLine1,
                AddressLine2 = s.AddressLine2,
                AddressLine3 = s.AddressLine3,
                EmailAddress = s.EmailAddress,
                PhoneNumber = s.PhoneNumber,
                Term = s.Term,
                Status = s.Status
            };
            DBContext.Customer.Add(entity);
            await DBContext.SaveChangesAsync();
            return HttpStatusCode.Created;
        }

        [HttpPut("UpdateCustomer")]
        public async Task<HttpStatusCode> UpdateCustomer(CustomerDTO Customer)
        {
            var entity = await DBContext.Customer.FirstOrDefaultAsync(s => s.Id == Customer.Id);
            entity.CustomerName = Customer.CustomerName;
            entity.AddressLine1 = Customer.AddressLine1;
            entity.AddressLine2 = Customer.AddressLine2;
            entity.AddressLine3 = Customer.AddressLine3;
            entity.EmailAddress = Customer.EmailAddress;
            entity.PhoneNumber = Customer.PhoneNumber;
            entity.Term = Customer.Term;
            entity.Status = Customer.Status;
            await DBContext.SaveChangesAsync();
            return HttpStatusCode.OK;
        }

        [HttpDelete("DeleteCustomer/{Id}")]
        public async Task<HttpStatusCode> DeleteCustomer(int Id)
        {
            var entity = new Customer()
            {
                Id = Id
            };
            DBContext.Customer.Attach(entity);
            DBContext.Customer.Remove(entity);
            await DBContext.SaveChangesAsync();
            return HttpStatusCode.OK;
        }
    }
}