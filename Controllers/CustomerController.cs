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
    public class CustomerController : ControllerBase
    {

        private const string cacheKey = "customerList";
        private readonly BVContext DBContext;
        private IMemoryCache _cache;
        private ILogger<CustomerController> _logger;
        private readonly IMapper _mapper;

        public CustomerController(BVContext DBContext, IMemoryCache cache, ILogger<CustomerController> logger, IMapper mapper)
        {
            this.DBContext = DBContext;
             _cache = cache ?? throw new ArgumentNullException(nameof(cache));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper;
        }

        [HttpGet("GetCustomer")]
        public async Task<ActionResult<List<CustomerDTO>>> Get()
        {
            _logger.Log(LogLevel.Information, "Trying to fetch the list of Customers from cache.");
            if (_cache.TryGetValue(cacheKey, out List<CustomerDTO> List))
            {
                _logger.Log(LogLevel.Information, "Customer list found in cache.");
                
            }
            else
            {
                _logger.Log(LogLevel.Information, "Customer list not found in cache. Fetching from database.");
                List = _mapper.Map<List<CustomerDTO>>(await DBContext.Customer.ToListAsync());
                
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

        [HttpPost("InsertCustomer")]
        public async Task<HttpStatusCode> InsertCustomer(CustomerDTO s)
        {
            var entity = _mapper.Map<Customer>(s);
            DBContext.Customer.Add(entity);
            await DBContext.SaveChangesAsync();
            _cache.Remove(cacheKey);
            return HttpStatusCode.Created;
        }

        [HttpPut("UpdateCustomer")]
        public async Task<HttpStatusCode> UpdateCustomer(CustomerDTO Customer)
        {
            var entity = await DBContext.Customer.FirstOrDefaultAsync(s => s.Id == Customer.Id);
            entity.CustomerName = Customer.CustomerName;
            entity.AddressLine1 = Customer.AddressLine1;
            entity.AddressLine2 = Customer.AddressLine2;
            entity.AddressLine3 = Customer.AddressLine3;
            if(Customer.EmailAddress != null){
                entity.EmailAddress = Customer.EmailAddress;
            }
            if(Customer.PhoneNumber != null){
                entity.PhoneNumber = Customer.PhoneNumber;
            }
            if(Customer.Status != null){
                entity.Status = Customer.Status;
            }
            entity.Term = Customer.Term;
            
            await DBContext.SaveChangesAsync();
            _cache.Remove(cacheKey);
            return HttpStatusCode.OK;
        }

        [HttpDelete("DeleteCustomer/{Id}")]
        public async Task<HttpStatusCode> DeleteCustomer(int Id)
        {
            var entity = new Customer()
            {
                Id = Id
            };
            DBContext.Customer.Attach(entity);
            DBContext.Customer.Remove(entity);
            await DBContext.SaveChangesAsync();
            _cache.Remove(cacheKey);
            return HttpStatusCode.OK;
        }
         [HttpPost("DeleteCustomers")]
        public  async Task<HttpStatusCode> DeleteCustomers(List<CustomerDTO> customers) {
            List<Customer> entities = customers.Select(i => new Customer(){
                Id = i.Id
            }).ToList();
            DBContext.Customer.AttachRange(entities);
            DBContext.Customer.RemoveRange(entities);
            await DBContext.SaveChangesAsync();
            _cache.Remove(cacheKey);
            return HttpStatusCode.OK;
        }
        [HttpGet("GetCustomerCount")]
        public ActionResult<int> GetCustomerCount()
        {
            return  DBContext.Customer.Where(x=>x.Status.ToLower() == "active").Count();
        }
    }
}