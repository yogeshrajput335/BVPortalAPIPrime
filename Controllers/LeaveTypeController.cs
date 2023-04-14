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
    [Route("api/[controller]"), Authorize(Roles = "ADMIN")]
    public class LeaveTypeController : ControllerBase
    {
        private const string cacheKey = "leaveTypeList";
        private readonly BVContext DBContext;
        private IMemoryCache _cache;
        private ILogger<LeaveTypeController> _logger;
        private readonly IMapper _mapper; 


        public LeaveTypeController(BVContext DBContext, IMemoryCache cache, ILogger<LeaveTypeController> logger, IMapper mapper)
        {
            this.DBContext = DBContext;
            _cache = cache ?? throw new ArgumentNullException(nameof(cache));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper; 
        }

        [HttpGet("GetLeaveTypes")]
        public async Task<ActionResult<List<LeaveTypeDTO>>> Get()
        {
            // var List = await DBContext.LeaveType.Select(
            //     s => new LeaveTypeDTO
            //     {
            //         Id = s.Id,
            //         Type = s.Type,
            //         Description = s.Description,
            //         Status = s.Status
            //     }
            // ).ToListAsync();
            _logger.Log(LogLevel.Information, "Trying to fetch the list of Leave Types from cache.");
            if (_cache.TryGetValue(cacheKey, out List<LeaveTypeDTO> List))
            {
                _logger.Log(LogLevel.Information, "Leave Type list found in cache.");
                
            }
            else
            {
                _logger.Log(LogLevel.Information, "LeaveT ype list not found in cache. Fetching from database.");
                List = _mapper.Map<List<LeaveTypeDTO>>(await DBContext.LeaveType.ToListAsync());
                
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

        [HttpPost("InsertLeaveType")]
        public async Task < HttpStatusCode > InsertLeaveType(LeaveTypeDTO s) {
            // var entity = new LeaveType() {
            //     Type = s.Type,
            //     Description = s.Description,
            //     Status = s.Status
            // };
            var entity = _mapper.Map<LeaveType>(s);
            DBContext.LeaveType.Add(entity);
            await DBContext.SaveChangesAsync();
            _cache.Remove(cacheKey);
            return HttpStatusCode.Created;
        }

        [HttpPut("UpdateLeaveType")]
        public async Task<HttpStatusCode> UpdateLeaveType(LeaveTypeDTO LeaveType) {
            var entity = await DBContext.LeaveType.FirstOrDefaultAsync(s => s.Id == LeaveType.Id);
            entity.Type = LeaveType.Type;
            entity.Description = LeaveType.Description;
            entity.Status = LeaveType.Status;
            await DBContext.SaveChangesAsync();
            _cache.Remove(cacheKey);
            return HttpStatusCode.OK;
        }
        
        [HttpDelete("DeleteLeaveType/{Id}")]
        public async Task < HttpStatusCode > DeleteLeaveType(int Id) {
            var entity = new LeaveType() {
                Id = Id
            };
            DBContext.LeaveType.Attach(entity);
            DBContext.LeaveType.Remove(entity);
            await DBContext.SaveChangesAsync();
            _cache.Remove(cacheKey);
            return HttpStatusCode.OK;
        }
        [HttpPost("DeleteLeaveTypes/{Id}")]
        public  async Task<HttpStatusCode> DeleteLeaveTypes(List<LeaveTypeDTO> leaveTypes) {
            List<LeaveType> entities = leaveTypes.Select(i => new LeaveType(){
                Id = i.Id
            }).ToList();
            DBContext.LeaveType.AttachRange(entities);
            DBContext.LeaveType.RemoveRange(entities);
            await DBContext.SaveChangesAsync();
            _cache.Remove(cacheKey);
            return HttpStatusCode.OK;
        }
    }
}