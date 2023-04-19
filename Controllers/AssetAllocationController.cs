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
    public class AssetAllocationController : ControllerBase
    {
        private const string cacheKey = "assetAllocationList";
        private readonly BVContext DBContext;
        private IMemoryCache _cache;
        private ILogger<AssetAllocationController> _logger;
        private readonly IMapper _mapper;  

        public AssetAllocationController(BVContext DBContext, IMemoryCache cache, ILogger<AssetAllocationController> logger, IMapper mapper)
        {
            this.DBContext = DBContext;
            _cache = cache ?? throw new ArgumentNullException(nameof(cache));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper;
        }
        
        [HttpGet("GetAssetAllocation")]
        public async Task<ActionResult<List<AssetAllocationDTO>>> Get()
        {
             _logger.Log(LogLevel.Information, "Trying to fetch the list of assets allocated from cache.");
            if (_cache.TryGetValue(cacheKey, out List<AssetAllocationDTO> List))
            {
                _logger.Log(LogLevel.Information, "Asset Allocation list found in cache.");
                
            }
            else
            {
                _logger.Log(LogLevel.Information, "Asset Allocation list not found in cache. Fetching from database.");
                List = _mapper.Map<List<AssetAllocationDTO>>(await DBContext.Assets.Include(x=>x.AssetType).ToListAsync());
                
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

        [HttpPost("InsertAssetAllocation")]
        public async Task < HttpStatusCode > InsertAssetAllocation(AssetAllocationDTO s) {
            var entity = _mapper.Map<AssetAllocation>(s);
            DBContext.AssetAllocation.Add(entity);
            await DBContext.SaveChangesAsync();
            _cache.Remove(cacheKey);
            return HttpStatusCode.Created;
        }

        [HttpPut("UpdateAssetAllocation")]
        public async Task<HttpStatusCode> UpdateAssetAllocation(AssetAllocationDTO AssetAllocation) {
            var entity = await DBContext.AssetAllocation.FirstOrDefaultAsync(s => s.Id == AssetAllocation.Id);
            entity.AssetId  = AssetAllocation.AssetId ;
            entity.AllocatedById = AssetAllocation.AllocatedById;
            entity.AllocatedToId = AssetAllocation.AllocatedToId;
            entity.AllocatedDate = AssetAllocation.AllocatedDate;
            entity.ReturnDate = AssetAllocation.ReturnDate;
            entity.Status = AssetAllocation.Status;
            await DBContext.SaveChangesAsync();
            _cache.Remove(cacheKey);
            return HttpStatusCode.OK;
        }
        
        [HttpDelete("DeleteAssetAllocation/{Id}")]
        public async Task < HttpStatusCode > DeleteAssetAllocation(int Id) {
            var entity = new AssetAllocation() {
                Id = Id
            };
            DBContext.AssetAllocation.Attach(entity);
            DBContext.AssetAllocation.Remove(entity);
            await DBContext.SaveChangesAsync();
            _cache.Remove(cacheKey);
            return HttpStatusCode.OK;
        }
         [HttpPost("DeleteAssetAllocations")]
        public  async Task<HttpStatusCode> DeleteAssetAllocation(List<AssetAllocationDTO> assetAllocations) {
            List<AssetAllocation> entities = assetAllocations.Select(i => new AssetAllocation(){
                Id = i.Id
            }).ToList();
            DBContext.AssetAllocation.AttachRange(entities);
            DBContext.AssetAllocation.RemoveRange(entities);
            await DBContext.SaveChangesAsync();
             _cache.Remove(cacheKey);
            return HttpStatusCode.OK;
        }
    }
}