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
    public class ServiceController : ControllerBase
    {
        private const string cacheKey = "serviceList";
        private readonly BVContext DBContext;
        private IMemoryCache _cache;
        private ILogger<ServiceController> _logger;
        private readonly IMapper _mapper;

        public ServiceController(BVContext DBContext, IMemoryCache cache, ILogger<ServiceController> logger, IMapper mapper)
        {
            this.DBContext = DBContext;
            _cache = cache ?? throw new ArgumentNullException(nameof(cache));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper;
        }

        [HttpGet("GetService")]
        public async Task<ActionResult<List<ServiceDTO>>> Get()
         {
        //     var List = await DBContext.Service.Select(
        //         s => new ServiceDTO
        //         {
        //             Id = s.Id,
        //             ServiceName = s.ServiceName,
        //             Unit = s.Unit,
        //             Rate = s.Rate,
        //             Status = s.Status
        //         }
        //     ).ToListAsync();
         _logger.Log(LogLevel.Information, "Trying to fetch the list of Services from cache.");
            if (_cache.TryGetValue(cacheKey, out List<ServiceDTO> List))
            {
                _logger.Log(LogLevel.Information, "Service list found in cache.");

            }
            else
            {
                _logger.Log(LogLevel.Information, "Service list not found in cache. Fetching from database.");
                List = _mapper.Map<List<ServiceDTO>>(await DBContext.Service.ToListAsync());

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

        [HttpPost("InsertService")]
        public async Task < HttpStatusCode > InsertService(ServiceDTO s) {
            // var entity = new Service() {
            //      ServiceName = s.ServiceName,
            //         Unit = s.Unit,
            //         Rate = s.Rate,
            //         Status = s.Status
            // };
            var entity = _mapper.Map<Service>(s);
            DBContext.Service.Add(entity);
            await DBContext.SaveChangesAsync();
            _cache.Remove(cacheKey);
            return HttpStatusCode.Created;
        }

        [HttpPut("UpdateService")]
        public async Task<HttpStatusCode> UpdateService(ServiceDTO Service) {
            var entity = await DBContext.Service.FirstOrDefaultAsync(s => s.Id == Service.Id);
            entity.ServiceName = Service.ServiceName;
            entity.Unit = Service.Unit;
            entity.Rate = Service.Rate;
            entity.Status = Service.Status;
            await DBContext.SaveChangesAsync();
            _cache.Remove(cacheKey);
            return HttpStatusCode.OK;
        }

        [HttpDelete("DeleteService/{Id}")]
        public async Task < HttpStatusCode > DeleteService(int Id) {
            var entity = new Service() {
                Id = Id
            };
            DBContext.Service.Attach(entity);
            DBContext.Service.Remove(entity);
            await DBContext.SaveChangesAsync();
            _cache.Remove(cacheKey);
            return HttpStatusCode.OK;
        }
         [HttpPost("DeleteServices")]
        public  async Task<HttpStatusCode> DeleteServices(List<ServiceDTO> services) {
            List<Service> entities = services.Select(i => new Service(){
                Id = i.Id
            }).ToList();
            DBContext.Service.AttachRange(entities);
            DBContext.Service.RemoveRange(entities);
            await DBContext.SaveChangesAsync();
            _cache.Remove(cacheKey);
            return HttpStatusCode.OK;
        }
    }
}