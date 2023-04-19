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
    public class AssetController : ControllerBase
    {
        private const string cacheKey = "assetList";
        private readonly BVContext DBContext;
        private IMemoryCache _cache;
        private ILogger<AssetController> _logger;
        private readonly IMapper _mapper;  

        public AssetController(BVContext DBContext, IMemoryCache cache, ILogger<AssetController> logger, IMapper mapper)
        {
            this.DBContext = DBContext;
            _cache = cache ?? throw new ArgumentNullException(nameof(cache));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper; 
        }

        [HttpGet("GetAsset")]
        public async Task<ActionResult<List<AssetDTO>>> Get()
        {
            _logger.Log(LogLevel.Information, "Trying to fetch the list of assets from cache.");
            if (_cache.TryGetValue(cacheKey, out List<AssetDTO> List))
            {
                _logger.Log(LogLevel.Information, "Asset list found in cache.");
                
            }
            else
            {
                _logger.Log(LogLevel.Information, "Asset list not found in cache. Fetching from database.");
                List = _mapper.Map<List<AssetDTO>>(await DBContext.Assets.Include(x=>x.AssetType).ToListAsync());
                
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

        [HttpPost("InsertAsset")]
        public async Task < HttpStatusCode > InsertAsset(AssetDTO s) {
            var entity = _mapper.Map<Asset>(s);
            DBContext.Assets.Add(entity);
            await DBContext.SaveChangesAsync();
            _cache.Remove(cacheKey);
            return HttpStatusCode.Created;
        }

        [HttpPut("UpdateAsset")]
        public async Task<HttpStatusCode> UpdateAsset(AssetDTO Asset) {
            var entity = await DBContext.Assets.FirstOrDefaultAsync(s => s.Id == Asset.Id);
            entity.Name = Asset.Name;
            entity.TypeId = Asset.TypeId;
            entity.ModelNumber = Asset.ModelNumber;
            entity.Status = Asset.Status;
            await DBContext.SaveChangesAsync();
            _cache.Remove(cacheKey);
            return HttpStatusCode.OK;
        }
        
        [HttpDelete("DeleteAsset/{Id}")]
        public async Task < HttpStatusCode > DeleteAsset(int Id) {
            var entity = new Asset() {
                Id = Id
            };
            DBContext.Assets.Attach(entity);
            DBContext.Assets.Remove(entity);
            await DBContext.SaveChangesAsync();
            _cache.Remove(cacheKey);
            return HttpStatusCode.OK;
        }
        [HttpPost("DeleteAssets")]
        public  async Task<HttpStatusCode> DeleteAsset(List<AssetDTO> Assets) {
            List<Asset> entities = Assets.Select(i => new Asset(){
                Id = i.Id
            }).ToList();
            DBContext.Assets.AttachRange(entities);
            DBContext.Assets.RemoveRange(entities);
            await DBContext.SaveChangesAsync();
            _cache.Remove(cacheKey);
            return HttpStatusCode.OK;
        }
        [HttpGet("GetAssetCount")]
        public ActionResult<int> GetAssetCount()
        {
            return  DBContext.Assets.Where(x=>x.Status.ToLower() == "active").Count();
        }
    }
}