using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using BVPortalApi.DTO;
using BVPortalApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace BVPortalApi.Controllers
{
    [ApiController]
    [Route("api/[controller]"), Authorize(Roles = "ADMIN,EMPLOYEE")]
    public class LeaveController : ControllerBase
    {
        private const string cacheKey = "assetList";
        private readonly BVContext DBContext;
        private IMemoryCache _cache;
        private ILogger<LeaveController> _logger;
        private readonly IMapper _mapper;

        public LeaveController(BVContext DBContext, IMemoryCache cache, ILogger<LeaveController> logger, IMapper mapper)
        {
            this.DBContext = DBContext;
            _cache = cache ?? throw new ArgumentNullException(nameof(cache));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper; 
        }

        [HttpGet("GetLeave")]
        public async Task<ActionResult<List<LeaveDTO>>> Get()
        {
             _logger.Log(LogLevel.Information, "Trying to fetch the list of Leaves from cache.");
            if (_cache.TryGetValue(cacheKey, out List<LeaveDTO> List))
            {
                _logger.Log(LogLevel.Information, "Leave list found in cache.");
                
            }
            else
            {
                _logger.Log(LogLevel.Information, "Leave list not found in cache. Fetching from database.");
                List = _mapper.Map<List<LeaveDTO>>(await DBContext.Leave.Include(x=>x.LeaveType).ToListAsync());
                List = _mapper.Map<List<LeaveDTO>>(await DBContext.Leave.Include(x=>x.Employee).ToListAsync());
                
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

        [HttpPost("InsertLeave")]
        public async Task < HttpStatusCode > InsertLeave(LeaveDTO s) {
            var entity = _mapper.Map<Leave>(s);
            DBContext.Leave.Add(entity);
            await DBContext.SaveChangesAsync();
            _cache.Remove(cacheKey);
            return HttpStatusCode.Created;
        }

        [HttpPut("UpdateLeave")]
        public async Task<HttpStatusCode> UpdateLeave(LeaveDTO Leave) {
            var entity = await DBContext.Leave.FirstOrDefaultAsync(s => s.Id == Leave.Id);
            entity.EmployeeId = Leave.EmployeeId;
            entity.LeaveTypeId = Leave.LeaveTypeId;
            entity.FromDate = Leave.FromDate;
            entity.ToDate = Leave.ToDate;
            entity.Status = Leave.Status;
            await DBContext.SaveChangesAsync();
            _cache.Remove(cacheKey);
            return HttpStatusCode.OK;
        }
        
        [HttpDelete("DeleteLeave/{Id}")]
        public async Task < HttpStatusCode > DeleteLeave(int Id) {
            var entity = new Leave() {
                Id = Id
            };
            DBContext.Leave.Attach(entity);
            DBContext.Leave.Remove(entity);
            await DBContext.SaveChangesAsync();
            _cache.Remove(cacheKey);
            return HttpStatusCode.OK;
        }
        [HttpPost("DeleteLeaves")]
        public  async Task<HttpStatusCode> DeleteLeaves(List<LeaveDTO> leaves) {
            List<Leave> entities = leaves.Select(i => new Leave(){
                Id = i.Id
            }).ToList();
            DBContext.Leave.AttachRange(entities);
            DBContext.Leave.RemoveRange(entities);
            await DBContext.SaveChangesAsync();
             _cache.Remove(cacheKey);
            return HttpStatusCode.OK;
        }
    }
}