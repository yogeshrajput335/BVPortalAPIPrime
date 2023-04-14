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
    public class ClientTermHistoryController : ControllerBase
    {
        private const string cacheKey = "clientTermHistoryList";
        private readonly BVContext DBContext;
        private IMemoryCache _cache;
        private ILogger<ClientTermHistoryController> _logger;
        private readonly IMapper _mapper;

        public ClientTermHistoryController(BVContext DBContext, IMemoryCache cache, ILogger<ClientTermHistoryController> logger, IMapper mapper)
        {
            this.DBContext = DBContext;
            _cache = cache ?? throw new ArgumentNullException(nameof(cache));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper;
        }

        [HttpGet("GetClientClientTermHistory")]
        public async Task<ActionResult<List<ClientTermHistoryDTO>>> Get()
        {
            // var List = await DBContext.ClientTermHistory.Select(
            //     s => new ClientTermHistoryDTO
            //     {
            //         Id = s.Id,
            //         ClientId = s.ClientId,
            //         OldTermText = s.OldTermText,
            //         OldTerm = s.OldTerm,
            //         NewTermText = s.NewTermText,
            //         NewTerm =s.NewTerm,
            //         ReasonForChange = s.ReasonForChange,
            //         ChangeDate = s.ChangeDate,
            //         ChangeBy = s.ChangeBy
            //     }
            // ).ToListAsync();
             _logger.Log(LogLevel.Information, "Trying to fetch the list of Client Term History from cache.");
            if (_cache.TryGetValue(cacheKey, out List<ClientTermHistoryDTO> List))
            {
                _logger.Log(LogLevel.Information, "Client Term History list found in cache.");
                
            }
            else
            {
                _logger.Log(LogLevel.Information, "Client Term History list not found in cache. Fetching from database.");
                List = _mapper.Map<List<ClientTermHistoryDTO>>(await DBContext.ClientTermHistory.Include(x=>x.Client).ToListAsync());
                
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

        [HttpPost("InsertClientTermHistory")]
        public async Task < HttpStatusCode > InsertClientTermHistory(ClientTermHistoryDTO s) {
            // var entity = new ClientTermHistory() {
            //         ClientId = s.ClientId,
            //         OldTermText = s.OldTermText,
            //         OldTerm = s.OldTerm,
            //         NewTermText = s.NewTermText,
            //         NewTerm =s.NewTerm,
            //         ReasonForChange = s.ReasonForChange,
            //         ChangeDate = s.ChangeDate,
            //         ChangeBy = s.ChangeBy
            // };
            var entity = _mapper.Map<ClientTermHistory>(s);
            DBContext.ClientTermHistory.Add(entity);
            await DBContext.SaveChangesAsync();
            _cache.Remove(cacheKey);
            return HttpStatusCode.Created;
        }

        [HttpPut("UpdateClientTermHistory")]
        public async Task<HttpStatusCode> UpdateClientTermHistory(ClientTermHistoryDTO ClientTermHistory) {
            var entity = await DBContext.ClientTermHistory.FirstOrDefaultAsync(s => s.Id == ClientTermHistory.Id);
            entity.ClientId = ClientTermHistory.ClientId;
            entity.OldTermText = ClientTermHistory.OldTermText;
            entity.OldTerm = ClientTermHistory.OldTerm;
            entity.NewTermText = ClientTermHistory.NewTermText;
            entity.NewTerm = ClientTermHistory.NewTerm;
            entity.ReasonForChange =ClientTermHistory.ReasonForChange;
            entity.ChangeDate = ClientTermHistory.ChangeDate;
            entity.ChangeBy = ClientTermHistory.ChangeBy;
            await DBContext.SaveChangesAsync();
            _cache.Remove(cacheKey);
            return HttpStatusCode.OK;
        }
        
        [HttpDelete("DeleteClientTermHistory/{Id}")]
        public async Task < HttpStatusCode > DeleteClientTermHistory(int Id) {
            var entity = new ClientTermHistory() {
                Id = Id
            };
            DBContext.ClientTermHistory.Attach(entity);
            DBContext.ClientTermHistory.Remove(entity);
            await DBContext.SaveChangesAsync();
            _cache.Remove(cacheKey);
            return HttpStatusCode.OK;
        }
    }
}