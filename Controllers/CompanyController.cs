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
    public class CompanyController : ControllerBase
    {
        private const string cacheKey = "companyList";
        private readonly BVContext DBContext;
        private IMemoryCache _cache;
        private ILogger<CompanyController> _logger;
        private readonly IMapper _mapper; 

        public CompanyController(BVContext DBContext, IMemoryCache cache, ILogger<CompanyController> logger, IMapper mapper)
        {
            this.DBContext = DBContext;
             _cache = cache ?? throw new ArgumentNullException(nameof(cache));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper; 
        }

        [HttpGet("GetCompany")]
        public async Task<ActionResult<List<CompanyDTO>>> Get()
        {
             _logger.Log(LogLevel.Information, "Trying to fetch the list of companies from cache.");
            if (_cache.TryGetValue(cacheKey, out List<CompanyDTO> List))
            {
                _logger.Log(LogLevel.Information, "Company list found in cache.");
                
            }
            else
            {
                _logger.Log(LogLevel.Information, "Company list not found in cache. Fetching from database.");
                List = _mapper.Map<List<CompanyDTO>>(await DBContext.Company.ToListAsync());
                
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

        [HttpPost("InsertCompany")]
        public async Task < HttpStatusCode > InsertCompany(CompanyDTO s) {
            var entity = _mapper.Map<Company>(s);
            DBContext.Company.Add(entity);
            await DBContext.SaveChangesAsync();
            _cache.Remove(cacheKey);
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
            _cache.Remove(cacheKey);
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
            _cache.Remove(cacheKey);
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
             _cache.Remove(cacheKey);
            return HttpStatusCode.OK;
        }
    }
}