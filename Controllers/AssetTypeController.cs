using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
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
    public class AssetTypeController : ControllerBase
    {
        private const string assetTypeListCacheKey = "assetTypeList";
        private readonly BVContext DBContext;
        private IMemoryCache _cache;
        private ILogger<AssetTypeController> _logger;

        public AssetTypeController(BVContext DBContext, IMemoryCache cache, ILogger<AssetTypeController> logger)
        {
            this.DBContext = DBContext;
            _cache = cache ?? throw new ArgumentNullException(nameof(cache));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet("GetAssetType")]
        public async Task<ActionResult<List<AssetTypeDTO>>> Get()
        {
             _logger.Log(LogLevel.Information, "Trying to fetch the list of asset types from cache.");
            if (_cache.TryGetValue(assetTypeListCacheKey, out List<AssetTypeDTO> List))
            {
                _logger.Log(LogLevel.Information, "Asset type list found in cache.");
                
            }
            else
            {
                _logger.Log(LogLevel.Information, "Asset type list not found in cache. Fetching from database.");
                List = await DBContext.AssetType.Select(
                    s => new AssetTypeDTO
                    {
                        Id = s.Id,
                        Name = s.Name,
                        Description = s.Description,
                        Status = s.Status
                    }
                ).ToListAsync();
                var cacheEntryOptions = new MemoryCacheEntryOptions()
                        .SetSlidingExpiration(TimeSpan.FromSeconds(60))
                        .SetAbsoluteExpiration(TimeSpan.FromSeconds(3600))
                        .SetPriority(CacheItemPriority.Normal)
                        .SetSize(1024);
                _cache.Set(assetTypeListCacheKey, List, cacheEntryOptions);
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

        [HttpPost("InsertAssetType")]
        public async Task < HttpStatusCode > InsertAssetType(AssetTypeDTO s) {
            var entity = new AssetType() {
                Name = s.Name,
                Description = s.Description,
                Status = s.Status
            };
            DBContext.AssetType.Add(entity);
            await DBContext.SaveChangesAsync();
            _cache.Remove(assetTypeListCacheKey);
            return HttpStatusCode.Created;
        }

        [HttpPut("UpdateAssetType")]
        public async Task<HttpStatusCode> UpdateAssetType(AssetTypeDTO AssetType) {
            var entity = await DBContext.AssetType.FirstOrDefaultAsync(s => s.Id == AssetType.Id);
            entity.Name = AssetType.Name;
            entity.Description = AssetType.Description;
            entity.Status = AssetType.Status;
            await DBContext.SaveChangesAsync();
            _cache.Remove(assetTypeListCacheKey);
            return HttpStatusCode.OK;
        }
        
        [HttpDelete("DeleteAssetType/{Id}")]
        public async Task < HttpStatusCode > DeleteAssetType(int Id) {
            var entity = new AssetType() {
                Id = Id
            };
            DBContext.AssetType.Attach(entity);
            DBContext.AssetType.Remove(entity);
            await DBContext.SaveChangesAsync();
            _cache.Remove(assetTypeListCacheKey);
            return HttpStatusCode.OK;
        }
    }
}