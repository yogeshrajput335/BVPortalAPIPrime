using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using BVPortalApi.DTO;
using BVPortalApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace BVPortalApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmpClientPerHourController : ControllerBase
    {
        private const string cacheKey = "empClientPerHourList";
        private readonly BVContext DBContext;
        private IMemoryCache _cache;
        private ILogger<EmpClientPerHourController> _logger;
        private readonly IMapper _mapper;

        public EmpClientPerHourController(BVContext DBContext, IMemoryCache cache, ILogger<EmpClientPerHourController> logger, IMapper mapper)
        {
            this.DBContext = DBContext;
            _cache = cache ?? throw new ArgumentNullException(nameof(cache));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper;
        }

        [HttpGet("GetEmpClientPerHour")]
        public async Task<ActionResult<List<EmpClientPerHourDTO>>> Get()
        {
             _logger.Log(LogLevel.Information, "Trying to fetch the list of Employee Client Per Hours from cache.");
            if (_cache.TryGetValue(cacheKey, out List<EmpClientPerHourDTO> List))
            {
                _logger.Log(LogLevel.Information, "Employee Client Per Hour list found in cache.");
                
            }
            else
            {
                _logger.Log(LogLevel.Information, "Employee Client Per Hour list not found in cache. Fetching from database.");
                List = _mapper.Map<List<EmpClientPerHourDTO>>(await DBContext.EmpClientPerHour.Include(x=>x.Employee).ToListAsync());
                List = _mapper.Map<List<EmpClientPerHourDTO>>(await DBContext.EmpClientPerHour.Include(x=>x.Client ).ToListAsync());
                
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

        [HttpPost("InsertEmpClientPerHour")]
        public async Task < HttpStatusCode > InsertEmpClientPerHour(EmpClientPerHourDTO s) {
            var entity = _mapper.Map<EmpClientPerHour>(s);
            DBContext.EmpClientPerHour.Add(entity);
            await DBContext.SaveChangesAsync();
            _cache.Remove(cacheKey);
            return HttpStatusCode.Created;
        }

        [HttpPut("UpdateEmpClientPerHour")]
        public async Task<HttpStatusCode> UpdateEmpClientPerHour(EmpClientPerHourDTO EmpClientPerHour) {
            var entity = await DBContext.EmpClientPerHour.FirstOrDefaultAsync(s => s.Id == EmpClientPerHour.Id);
            entity.EmployeeId = EmpClientPerHour.EmployeeId;
            entity.ClientId = EmpClientPerHour.ClientId;
            entity.PerHour = EmpClientPerHour.PerHour;
            await DBContext.SaveChangesAsync();
            _cache.Remove(cacheKey);
            return HttpStatusCode.OK;
        }
        
        [HttpDelete("DeleteEmpClientPerHour/{Id}")]
        public async Task < HttpStatusCode > DeleteEmpClientPerHour(int Id) {
            var entity = new EmpClientPerHour() {
                Id = Id
            };
            DBContext.EmpClientPerHour.Attach(entity);
            DBContext.EmpClientPerHour.Remove(entity);
            await DBContext.SaveChangesAsync();
            _cache.Remove(cacheKey);
            return HttpStatusCode.OK;
        }
    }
}