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
    public class EmployeeContactController : ControllerBase
    {
        private const string cacheKey = "employeeContactList";
        private readonly BVContext DBContext;
        private IMemoryCache _cache;
        private ILogger<EmployeeContactController> _logger;
        private readonly IMapper _mapper;  

        public EmployeeContactController(BVContext DBContext, IMemoryCache cache, ILogger<EmployeeContactController> logger, IMapper mapper)
        {
            this.DBContext = DBContext;
            _cache = cache ?? throw new ArgumentNullException(nameof(cache));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper; 
        }

        [HttpGet("GetEmployeeContact")]
        public async Task<ActionResult<List<EmployeeContactDTO>>> GetEmployeeContact()
        {
            _logger.Log(LogLevel.Information, "Trying to fetch the list of Employee Contacts from cache.");
            if (_cache.TryGetValue(cacheKey, out List<EmployeeContactDTO> List))
            {
                _logger.Log(LogLevel.Information, "Employee Contact list found in cache.");
                
            }
            else
            {
                _logger.Log(LogLevel.Information, "Employee Contact list not found in cache. Fetching from database.");
                List = _mapper.Map<List<EmployeeContactDTO>>(await DBContext.EmployeeContact.ToListAsync());
                
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
        
        [HttpGet("GetEmployeeContactByEmpId/{empId}")]
        public async Task<ActionResult<EmployeeContactDTO>> GetEmployeeContactByEmpId(int empId)
        {
            var List = await DBContext.EmployeeContact.Where(x=>x.EmployeeId==empId).Select(
                s => new EmployeeContactDTO
                {
                    Id = s.Id,
                    EmployeeId = s.EmployeeId,
                    EmployeeName = s.Employee.FirstName + " "+ s.Employee.LastName,
                    PersonalEmailId = s.PersonalEmailId,
                    PhoneNumber = s.PhoneNumber,
                    WorkEmail = s.WorkEmail,
                    EmergencyContactName = s.EmergencyContactName,
                    EmergencyContactNumber = s.EmergencyContactNumber
                }
            ).FirstOrDefaultAsync();
            
            if (List==null)
            {
                return new EmployeeContactDTO(){Id =0,EmployeeId=empId};
            }
            else
            {
                return List;
            }
        }

        [HttpPost("InsertEmployeeContact")]
        public async Task < HttpStatusCode > InsertEmployeeContact(EmployeeContactDTO s) {
            var entity = _mapper.Map<EmployeeContact>(s);
            DBContext.EmployeeContact.Add(entity);
            await DBContext.SaveChangesAsync();
            _cache.Remove(cacheKey);
            return HttpStatusCode.Created;
        }

        [HttpPut("UpdateEmployeeContact")]
        public async Task<HttpStatusCode> UpdateEmployeeContact(EmployeeContactDTO Employee) {
            var entity = await DBContext.EmployeeContact.FirstOrDefaultAsync(s => s.Id == Employee.Id);
            entity.PersonalEmailId = Employee.PersonalEmailId;
            entity.PhoneNumber = Employee.PhoneNumber;
            entity.WorkEmail = Employee.WorkEmail;
            entity.EmergencyContactName = Employee.EmergencyContactName;
            entity.EmergencyContactNumber = Employee.EmergencyContactNumber;
            await DBContext.SaveChangesAsync();
            _cache.Remove(cacheKey);
            return HttpStatusCode.OK;
        }
        
        [HttpDelete("DeleteEmployeeContact/{Id}")]
        public async Task < HttpStatusCode > DeleteEmployeeContact(int Id) {
            var entity = new EmployeeContact() {
                Id = Id
            };
            DBContext.EmployeeContact.Attach(entity);
            DBContext.EmployeeContact.Remove(entity);
            await DBContext.SaveChangesAsync();
            _cache.Remove(cacheKey);
            return HttpStatusCode.OK;
        }
    }
}