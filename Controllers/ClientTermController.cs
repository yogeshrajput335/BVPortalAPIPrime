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
    public class ClientTermController : ControllerBase
    {
        private const string cacheKey = "clientTermList";
        private readonly BVContext DBContext;
        private IMemoryCache _cache;
        private ILogger<ClientTermController> _logger;
        private readonly IMapper _mapper;
        public ClientTermController(BVContext DBContext, IMemoryCache cache, ILogger<ClientTermController> logger, IMapper mapper)
        {
            this.DBContext = DBContext;
            _cache = cache ?? throw new ArgumentNullException(nameof(cache));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper;
        }

         [HttpGet("GetClientTerm")]
        public async Task<ActionResult<List<ClientTermDTO>>> Get()
        {
            // var List = await DBContext.ClientTerm.Select(
            //     s => new ClientTermDTO
            //     {
            //         Id = s.Id,
            //         ClientId = s.ClientId,
            //         TermText = s.TermText,
            //         Term = s.Term,
            //     }
            // ).ToListAsync();
            _logger.Log(LogLevel.Information, "Trying to fetch the list of Client Terms from cache.");
            if (_cache.TryGetValue(cacheKey, out List<ClientTermDTO> List))
            {
                _logger.Log(LogLevel.Information, "Client Term list found in cache.");
                
            }
            else
            {
                _logger.Log(LogLevel.Information, "Client Term list not found in cache. Fetching from database.");
                List = _mapper.Map<List<ClientTermDTO>>(await DBContext.ClientTerm.Include(x=>x.Client).ToListAsync());
                
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

        [HttpPost("InsertClientTerm")]
        public async Task < HttpStatusCode > InsertClientTerm(ClientTermDTO s) {
            // var entity = new ClientTerm() {
            //         ClientId = s.ClientId,
            //         TermText = s.TermText,
            //         Term = s.Term,
            // };
            var entity = _mapper.Map<ClientTerm>(s);
            DBContext.ClientTerm.Add(entity);
            await DBContext.SaveChangesAsync();
            _cache.Remove(cacheKey);
            return HttpStatusCode.Created;
        }

        [HttpPut("UpdateClientTerm")]
        public async Task<HttpStatusCode> UpdateClientTerm(ClientTermDTO ClientTerm) {
            var entity = await DBContext.ClientTerm.FirstOrDefaultAsync(s => s.Id == ClientTerm.Id);
            entity.ClientId= ClientTerm.ClientId;
            entity.TermText = ClientTerm.TermText;
            entity.Term = ClientTerm.Term;
            await DBContext.SaveChangesAsync();
            _cache.Remove(cacheKey);
            return HttpStatusCode.OK;
        }
        
        [HttpDelete("DeleteClientTerm/{Id}")]
        public async Task < HttpStatusCode > DeleteClientTerm(int Id) {
            var entity = new ClientTerm() {
                Id = Id
            };
            DBContext.ClientTerm.Attach(entity);
            DBContext.ClientTerm.Remove(entity);
            await DBContext.SaveChangesAsync();
            _cache.Remove(cacheKey);
            return HttpStatusCode.OK;
        }
    }
}