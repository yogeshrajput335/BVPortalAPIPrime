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
    public class ReferListController : ControllerBase
    {
        private const string cacheKey = "referListList";
        private readonly BVContext DBContext;
        private IMemoryCache _cache;
        private ILogger<ReferListController> _logger;
        private readonly IMapper _mapper;

        public ReferListController(BVContext DBContext, IMemoryCache cache, ILogger<ReferListController> logger, IMapper mapper)
        {
            this.DBContext = DBContext;
            _cache = cache ?? throw new ArgumentNullException(nameof(cache));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper; 
        }

        [HttpGet("GetReferList")]
        public async Task<ActionResult<List<ReferListDTO>>> Get()
        {
            // var List = await DBContext.ReferList.Select(
            //     s => new ReferListDTO
            //     {
            //         Id = s.Id,
            //         FirstName = s.FirstName,
            //         LastName = s.LastName,
            //         PhoneNo = s.PhoneNo,
            //         Email = s.Email,
            //         Status = s.Status
            //     }
            // ).ToListAsync();
            _logger.Log(LogLevel.Information, "Trying to fetch the list of ReferList from cache.");
            if (_cache.TryGetValue(cacheKey, out List<ReferListDTO> List))
            {
                _logger.Log(LogLevel.Information, "Refer list found in cache.");
                
            }
            else
            {
                _logger.Log(LogLevel.Information, "Refer list not found in cache. Fetching from database.");
                List = _mapper.Map<List<ReferListDTO>>(await DBContext.ReferList.ToListAsync());
                
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

        [HttpPost("InsertReferList")]
        public async Task < HttpStatusCode > InsertReferList(ReferListDTO s) {
            // var entity = new ReferList() {
            //         FirstName = s.FirstName,
            //         LastName = s.LastName,
            //         PhoneNo = s.PhoneNo,
            //         Email = s.Email,
            //         Status = s.Status
            // };
             var entity = _mapper.Map<ReferList>(s);
            DBContext.ReferList.Add(entity);
            await DBContext.SaveChangesAsync();
            _cache.Remove(cacheKey);
            return HttpStatusCode.Created;
        }

        [HttpPut("UpdateReferList")]
        public async Task<HttpStatusCode> UpdateReferList(ReferListDTO ReferList) {
            var entity = await DBContext.ReferList.FirstOrDefaultAsync(s => s.Id == ReferList.Id);
            entity.FirstName = ReferList.FirstName;
            entity.LastName = ReferList.LastName;
            entity.PhoneNo = ReferList.PhoneNo;
            entity.Email = ReferList.Email;
            entity.Status = ReferList.Status;
            await DBContext.SaveChangesAsync();
            _cache.Remove(cacheKey);
            return HttpStatusCode.OK;
        }

        [HttpDelete("DeleteReferList/{Id}")]
        public async Task < HttpStatusCode > DeleteReferList(int Id) {
            var entity = new ReferList() {
                Id = Id
            };
            DBContext.ReferList.Attach(entity);
            DBContext.ReferList.Remove(entity);
            await DBContext.SaveChangesAsync();
            _cache.Remove(cacheKey);
            return HttpStatusCode.OK;
        }
        [HttpPost("DeleteReferLists")]
        public  async Task<HttpStatusCode> DeleteReferLists(List<ReferListDTO> references) {
            List<ReferList> entities = references.Select(i => new ReferList(){
                Id = i.Id
            }).ToList();
            DBContext.ReferList.AttachRange(entities);
            DBContext.ReferList.RemoveRange(entities);
            await DBContext.SaveChangesAsync();
            _cache.Remove(cacheKey);
            return HttpStatusCode.OK;
        }
        
        [HttpDelete("MoveToCandidate/{Id}/{EmployeeId}")]
        public async Task < HttpStatusCode > MoveToCandidate(int Id,int EmployeeId) {
            var entity = await DBContext.ReferList.FirstOrDefaultAsync(s => s.Id == Id);
            entity.Status = "Moved to candidate";

             var candidate = new Candidate() {
                    FirstName = entity.FirstName,
                    LastName = entity.LastName,
                    PhoneNo = entity.PhoneNo,
                    Email = entity.Email,
                    Status = "REFERRED",
                    ReferBy = EmployeeId
            };
            DBContext.Candidates.Add(candidate);

            await DBContext.SaveChangesAsync();
            _cache.Remove(cacheKey);
            return HttpStatusCode.OK;
        }
    }
}