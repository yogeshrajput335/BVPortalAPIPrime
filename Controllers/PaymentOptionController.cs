using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using BVPortalApi.DTO;
using AutoMapper;
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
    public class PaymentOptionController : ControllerBase
    {
        private const string cacheKey = "paymentOptionList";
        private readonly BVContext DBContext;
        private IMemoryCache _cache;
        private ILogger<PaymentOptionController> _logger;
        private readonly IMapper _mapper;

        public PaymentOptionController(BVContext DBContext, IMemoryCache cache, ILogger<PaymentOptionController> logger, IMapper mapper)
        {
            this.DBContext = DBContext;
            _cache = cache ?? throw new ArgumentNullException(nameof(cache));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper;
        }

        [HttpGet("GetPaymentOption")]
        public async Task<ActionResult<List<PaymentOptionDTO>>> Get()
        {
            _logger.Log(LogLevel.Information, "Trying to fetch the list of Payment Options from cache.");
            if (_cache.TryGetValue(cacheKey, out List<PaymentOptionDTO> List))
            {
                _logger.Log(LogLevel.Information, "Payment Option list found in cache.");
                
            }
            else
            {
                _logger.Log(LogLevel.Information, "Payment Option list not found in cache. Fetching from database.");
                List = _mapper.Map<List<PaymentOptionDTO>>(await DBContext.PaymentOption.ToListAsync());
                
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

        [HttpPost("InsertPaymentOption")]
        public async Task<HttpStatusCode> InsertPaymentOption(PaymentOptionDTO s)
        {
            var entity = _mapper.Map<PaymentOption>(s);
            DBContext.PaymentOption.Add(entity);
            await DBContext.SaveChangesAsync();
            _cache.Remove(cacheKey);
            return HttpStatusCode.Created;
        }

        [HttpPut("UpdatePaymentOption")]
        public async Task<HttpStatusCode> UpdatePaymentOption(PaymentOptionDTO PaymentOption)
        {
            var entity = await DBContext.PaymentOption.FirstOrDefaultAsync(s => s.Id == PaymentOption.Id);
            entity.PaymentOptionName = PaymentOption.PaymentOptionName;
            entity.Status = PaymentOption.Status;
            await DBContext.SaveChangesAsync();
            _cache.Remove(cacheKey);
            return HttpStatusCode.OK;
        }

        [HttpDelete("DeletePaymentOption/{Id}")]
        public async Task<HttpStatusCode> DeletePaymentOption(int Id)
        {
            var entity = new PaymentOption()
            {
                Id = Id
            };
            DBContext.PaymentOption.Attach(entity);
            DBContext.PaymentOption.Remove(entity);
            await DBContext.SaveChangesAsync();
            _cache.Remove(cacheKey);
            return HttpStatusCode.OK;
        }
         [HttpPost("DeletePaymentOptions")]
        public  async Task<HttpStatusCode> DeletePaymentOptions(List<PaymentOptionDTO> paymentOptions) {
            List<PaymentOption> entities = paymentOptions.Select(i => new PaymentOption(){
                Id = i.Id
            }).ToList();
            DBContext.PaymentOption.AttachRange(entities);
            DBContext.PaymentOption.RemoveRange(entities);
            await DBContext.SaveChangesAsync();
            _cache.Remove(cacheKey);
            return HttpStatusCode.OK;
        }
    }
}