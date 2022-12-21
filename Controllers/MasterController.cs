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
    public class MasterController : ControllerBase
    {
        private readonly BVContext DBContext;

        public MasterController(BVContext DBContext)
        {
            this.DBContext = DBContext;
        }
        
        [HttpPost("GetMasters")]
        public async Task<ActionResult<MasterDataDTO>> GetMasters(List<string> MasterNames)
        {
            MasterDataDTO md = new MasterDataDTO();
            md.Company = MasterNames.Contains("Company")? this.DBContext.Company.Where(x=>x.Status.ToLower()=="active").ToList():null;
            md.Customer = MasterNames.Contains("Customer")?this.DBContext.Customer.Where(x=>x.Status.ToLower()=="active").ToList():null;
            md.Product = MasterNames.Contains("Product")?this.DBContext.Product.Where(x=>x.Status.ToLower()=="active").ToList():null;
            md.Service = MasterNames.Contains("Service")?this.DBContext.Service.Where(x=>x.Status.ToLower()=="active").ToList():null;
            md.PaymentOption = MasterNames.Contains("PaymentOption")?this.DBContext.PaymentOption.Where(x=>x.Status.ToLower()=="active").ToList():null;
            
            return md;
        }
    }
}