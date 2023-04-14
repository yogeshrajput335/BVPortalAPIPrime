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
    [Route("api/[controller]")]
    public class HolidayMasterController : ControllerBase
    {
        private const string cacheKey = "holidayMasterList";
        private readonly BVContext DBContext;
        private IMemoryCache _cache;
        private ILogger<HolidayMasterController> _logger;
        private readonly IMapper _mapper;

        public HolidayMasterController(BVContext DBContext, IMemoryCache cache, ILogger<HolidayMasterController> logger, IMapper mapper)
        {
            this.DBContext = DBContext;
            _cache = cache ?? throw new ArgumentNullException(nameof(cache));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper; 
        }

        [HttpGet("GetHolidayMaster"), Authorize(Roles = "ADMIN,EMPLOYEE")]
        public async Task<ActionResult<List<HolidayMasterDTO>>> Get()
        { _logger.Log(LogLevel.Information, "Trying to fetch the list of Holiday Master from cache.");
            if (_cache.TryGetValue(cacheKey, out List<HolidayMasterDTO> List))
            {
                _logger.Log(LogLevel.Information, "Holiday Master list found in cache.");
                
            }
            else
            {
                _logger.Log(LogLevel.Information, "Holiday Master list not found in cache. Fetching from database.");
                List = _mapper.Map<List<HolidayMasterDTO>>(await DBContext.HolidayMaster.ToListAsync());
                
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

        [HttpPost("InsertHolidayMaster"), Authorize(Roles = "ADMIN")]
        public async Task<HttpStatusCode> InsertHolidayMaster(HolidayMasterDTO s)
        {
            // var entity = new HolidayMaster()
            // {
            //     Id = s.Id,
            //     HolidayName = s.HolidayName,
            //     Description = s.Description,
            //     Date = s.Date,
            //     Status = s.Status
            // };
            var entity = _mapper.Map<HolidayMaster>(s);
            DBContext.HolidayMaster.Add(entity);
            await DBContext.SaveChangesAsync();
            _cache.Remove(cacheKey);
            return HttpStatusCode.Created;
        }

        [HttpPut("UpdateHolidayMaster"), Authorize(Roles = "ADMIN")]
        public async Task<HttpStatusCode> UpdateHolidayMaster(HolidayMasterDTO HolidayMaster)
        {
            var entity = await DBContext.HolidayMaster.FirstOrDefaultAsync(s => s.Id == HolidayMaster.Id);
            entity.HolidayName = HolidayMaster.HolidayName;
            entity.Description = HolidayMaster.Description;
            entity.Date = HolidayMaster.Date;
            entity.Status = HolidayMaster.Status;
            await DBContext.SaveChangesAsync();
            _cache.Remove(cacheKey);
            return HttpStatusCode.OK;
        }

        [HttpDelete("DeleteHolidayMaster/{Id}"), Authorize(Roles = "ADMIN")]
        public async Task<HttpStatusCode> DeleteHolidayMaster(int Id)
        {
            var entity = new HolidayMaster()
            {
                Id = Id
            };
            DBContext.HolidayMaster.Attach(entity);
            DBContext.HolidayMaster.Remove(entity);
            await DBContext.SaveChangesAsync();
            _cache.Remove(cacheKey);
            return HttpStatusCode.OK;
        }
        [HttpPost("DeleteHolidayMasters")]
        public async Task<HttpStatusCode> DeleteHolidayMasters(List<HolidayMasterDTO> holidays)
        {
            List<HolidayMaster> entities = holidays.Select(i => new HolidayMaster()
            {
                Id = i.Id
            }).ToList();
            DBContext.HolidayMaster.AttachRange(entities);
            DBContext.HolidayMaster.RemoveRange(entities);
            await DBContext.SaveChangesAsync();
             _cache.Remove(cacheKey);
            return HttpStatusCode.OK;
        }
    }
}