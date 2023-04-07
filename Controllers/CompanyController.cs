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
    public class CompanyController : ControllerBase
    {
        
        private readonly BVContext DBContext;

        public CompanyController(BVContext DBContext)
        {
            this.DBContext = DBContext;
        }

        [HttpGet("GetCompany")]
        public async Task<ActionResult<List<CompanyDTO>>> Get()
        {
            var List = await DBContext.Company.Select(
                s => new CompanyDTO
                {
                    Id = s.Id,
                    CompanyName = s.CompanyName,
                    AddressLine1 = s.AddressLine1,
                    AddressLine2 = s.AddressLine2,
                    AddressLine3 = s.AddressLine3,
                    EmailAddress = s.EmailAddress,
                    PhoneNumber = s.PhoneNumber,
                    CompanyLogo = s.CompanyLogo,
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

        [HttpPost("InsertCompany")]
        public async Task < HttpStatusCode > InsertCompany(CompanyDTO s) {
            var entity = new Company() {
                CompanyName = s.CompanyName,
                AddressLine1 = s.AddressLine1,
                AddressLine2 = s.AddressLine2,
                AddressLine3 = s.AddressLine3,
                EmailAddress = s.EmailAddress,
                PhoneNumber = s.PhoneNumber,
                CompanyLogo = s.CompanyLogo,
                Status = s.Status
            };
            DBContext.Company.Add(entity);
            await DBContext.SaveChangesAsync();
            return HttpStatusCode.Created;
        }

        [HttpPut("UpdateCompany")]
        public async Task<HttpStatusCode> UpdateCompany(CompanyDTO Company) {
            var entity = await DBContext.Company.FirstOrDefaultAsync(s => s.Id == Company.Id);
            entity.CompanyName = Company.CompanyName;
            entity.AddressLine1 = Company.AddressLine1;
            entity.AddressLine2 = Company.AddressLine2;
            entity.AddressLine3 = Company.AddressLine3;
            entity.EmailAddress = Company.EmailAddress;
            entity.PhoneNumber = Company.PhoneNumber;
            if(Company.CompanyLogo != null){
                entity.CompanyLogo = Company.CompanyLogo;
            }
            if(Company.Status != null){
                entity.Status = Company.Status;
            }
            await DBContext.SaveChangesAsync();
            return HttpStatusCode.OK;
        }
        
        [HttpDelete("DeleteCompany/{Id}")]
        public async Task < HttpStatusCode > DeleteCompany(int Id) {
            var entity = new Company() {
                Id = Id
            };
            DBContext.Company.Attach(entity);
            DBContext.Company.Remove(entity);
            await DBContext.SaveChangesAsync();
            return HttpStatusCode.OK;
        }
        [HttpPost("DeleteCompanies")]
        public  async Task<HttpStatusCode> DeleteCompanies(List<CompanyDTO> companies) {
            List<Company> entities = companies.Select(i => new Company(){
                Id = i.Id
            }).ToList();
            DBContext.Company.AttachRange(entities);
            DBContext.Company.RemoveRange(entities);
            await DBContext.SaveChangesAsync();
            // _cache.Remove(cacheKey);
            return HttpStatusCode.OK;
        }
    }
}