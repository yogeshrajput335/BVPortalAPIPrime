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
    public class PaymentOptionController : ControllerBase
    {

        private readonly BVContext DBContext;

        public PaymentOptionController(BVContext DBContext)
        {
            this.DBContext = DBContext;
        }

        [HttpGet("GetPaymentOption")]
        public async Task<ActionResult<List<PaymentOptionDTO>>> Get()
        {
            var List = await DBContext.PaymentOption.Select(
                s => new PaymentOptionDTO
                {
                    Id = s.Id,
                    PaymentOptionName = s.PaymentOptionName,
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

        [HttpPost("InsertPaymentOption")]
        public async Task<HttpStatusCode> InsertPaymentOption(PaymentOptionDTO s)
        {
            var entity = new PaymentOption()
            {
                PaymentOptionName = s.PaymentOptionName,
                Status = s.Status
            };
            DBContext.PaymentOption.Add(entity);
            await DBContext.SaveChangesAsync();
            return HttpStatusCode.Created;
        }

        [HttpPut("UpdatePaymentOption")]
        public async Task<HttpStatusCode> UpdatePaymentOption(PaymentOptionDTO PaymentOption)
        {
            var entity = await DBContext.PaymentOption.FirstOrDefaultAsync(s => s.Id == PaymentOption.Id);
            entity.PaymentOptionName = PaymentOption.PaymentOptionName;
            entity.Status = PaymentOption.Status;
            await DBContext.SaveChangesAsync();
            return HttpStatusCode.OK;
        }

        [HttpDelete("DeletePaymentOption/{Id}")]
        public async Task<HttpStatusCode> DeletePaymentOption(int Id)
        {
            var entity = new PaymentOption()
            {
                Id = Id
            };
            DBContext.PaymentOption.Attach(entity);
            DBContext.PaymentOption.Remove(entity);
            await DBContext.SaveChangesAsync();
            return HttpStatusCode.OK;
        }
    }
}