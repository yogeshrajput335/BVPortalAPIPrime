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
    public class EmpClientPerHourHistoryController : ControllerBase
    {
        private const string cacheKey = "empClientPerHourHistoryList";
        private readonly BVContext DBContext;
        private IMemoryCache _cache;
        private ILogger<AssetController> _logger;
        private readonly IMapper _mapper;  

        public EmpClientPerHourHistoryController(BVContext DBContext, IMemoryCache cache, ILogger<AssetController> logger, IMapper mapper)
        {
            this.DBContext = DBContext;
            _cache = cache ?? throw new ArgumentNullException(nameof(cache));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper; 
        }

        [HttpGet("GetEmpClientPerHourHistory")]
        public async Task<ActionResult<List<EmpClientPerHourHistoryDTO>>> Get()
        {
             _logger.Log(LogLevel.Information, "Trying to fetch the list of Employee Client Per Hours History from cache.");
            if (_cache.TryGetValue(cacheKey, out List<EmpClientPerHourHistoryDTO> List))
            {
                _logger.Log(LogLevel.Information, "Employee Client Per Hour History list found in cache.");
                
            }
            else
            {
                _logger.Log(LogLevel.Information, "Employee Client Per Hour Historylist not found in cache. Fetching from database.");
                List = _mapper.Map<List<EmpClientPerHourHistoryDTO>>(await DBContext.EmpClientPerHour.Include(x=>x.Employee).ToListAsync());
                List = _mapper.Map<List<EmpClientPerHourHistoryDTO>>(await DBContext.EmpClientPerHour.Include(x=>x.Client ).ToListAsync());
                
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

        [HttpPost("InsertEmpClientPerHourHistory")]
        public async Task < HttpStatusCode > InsertEmpClientPerHourHistory(EmpClientPerHourHistoryDTO s) {
            var entity = _mapper.Map<EmpClientPerHourHistory>(s);
            DBContext.EmpClientPerHourHistory.Add(entity);
            await DBContext.SaveChangesAsync();
            _cache.Remove(cacheKey);
            return HttpStatusCode.Created;
        }

        [HttpPut("UpdateEmpClientPerHourHistory")]
        public async Task<HttpStatusCode> UpdateEmpClientPerHourHistory(EmpClientPerHourHistoryDTO EmpClientPerHourHistory) {
            var entity = await DBContext.EmpClientPerHourHistory.FirstOrDefaultAsync(s => s.Id == EmpClientPerHourHistory.Id);
            entity.EmployeeId = EmpClientPerHourHistory.EmployeeId;
            entity.ClientId = EmpClientPerHourHistory.ClientId;
            entity.OldPerHour = EmpClientPerHourHistory.OldPerHour;
            entity.NewPerHour = EmpClientPerHourHistory.NewPerHour;
            entity.ReasonForChange = EmpClientPerHourHistory.ReasonForChange;
            entity.ChangeDate = EmpClientPerHourHistory.ChangeDate;
            entity.ChangeBy = EmpClientPerHourHistory.ChangeBy;
            await DBContext.SaveChangesAsync();
            _cache.Remove(cacheKey);
            return HttpStatusCode.OK;
        }
        
        [HttpDelete("DeleteEmpClientPerHourHistory/{Id}")]
        public async Task < HttpStatusCode > DeleteEmpClientPerHourHistory(int Id) {
            var entity = new EmpClientPerHourHistory() {
                Id = Id
            };
            DBContext.EmpClientPerHourHistory.Attach(entity);
            DBContext.EmpClientPerHourHistory.Remove(entity);
            await DBContext.SaveChangesAsync();
            _cache.Remove(cacheKey);
            return HttpStatusCode.OK;
        }
    }
}