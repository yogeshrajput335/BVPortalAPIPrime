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
    public class EmployeeController : ControllerBase
    {
        private const string cacheKey = "employeeList";
        private readonly BVContext DBContext;
        private IMemoryCache _cache;
        private ILogger<EmployeeController> _logger;
        private readonly IMapper _mapper;

        public EmployeeController(BVContext DBContext, IMemoryCache cache, ILogger<EmployeeController> logger, IMapper mapper)
        {
            this.DBContext = DBContext;
            _cache = cache ?? throw new ArgumentNullException(nameof(cache));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper; 
        }

        [HttpGet("GetEmployee")]
        public async Task<ActionResult<List<EmployeeDTO>>> Get()
        {
             _logger.Log(LogLevel.Information, "Trying to fetch the list of Employees from cache.");
            if (_cache.TryGetValue(cacheKey, out List<EmployeeDTO> List))
            {
                _logger.Log(LogLevel.Information, "Employee list found in cache.");
                
            }
            else
            {
                _logger.Log(LogLevel.Information, "Employee list not found in cache. Fetching from database.");
                List = _mapper.Map<List<EmployeeDTO>>(await DBContext.Employee.Include(x=>x.User).ToListAsync());
                
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

        [HttpPost("InsertEmployee")]
        public async Task < HttpStatusCode > InsertEmployee(EmployeeDTO s) {
            var entity = _mapper.Map<Employee>(s);
            DBContext.Employee.Add(entity);
            await DBContext.SaveChangesAsync();
            _cache.Remove(cacheKey);
            return HttpStatusCode.Created;
        }

        [HttpPut("UpdateEmployee")]
        public async Task<HttpStatusCode> UpdateEmployee(EmployeeDTO Employee) {
            var entity = await DBContext.Employee.FirstOrDefaultAsync(s => s.Id == Employee.Id);
            entity.FirstName = Employee.FirstName;
            entity.LastName = Employee.LastName;
            entity.Email = Employee.Email;
            entity.PhoneNumber = Employee.PhoneNumber;
            entity.EmployeeType = Employee.EmployeeType;
            entity.Status = Employee.Status;
            await DBContext.SaveChangesAsync();
            _cache.Remove(cacheKey);
            return HttpStatusCode.OK;
        }
        
        [HttpDelete("DeleteEmployee/{Id}")]
        public async Task < HttpStatusCode > DeleteEmployee(int Id) {
            var entity = new Employee() {
                Id = Id
            };
            DBContext.Employee.Attach(entity);
            DBContext.Employee.Remove(entity);
            await DBContext.SaveChangesAsync();
            _cache.Remove(cacheKey);
            return HttpStatusCode.OK;
        }

        [HttpPost("DeleteEmployees")]
        public  async Task<HttpStatusCode> DeleteEmployee(List<EmployeeDTO> Employees) {
            List<Employee> entities = Employees.Select(i => new Employee(){
                Id = i.Id
            }).ToList();
            DBContext.Employee.AttachRange(entities);
            DBContext.Employee.RemoveRange(entities);
            await DBContext.SaveChangesAsync();
            _cache.Remove(cacheKey);
            return HttpStatusCode.OK;
        }

        [HttpPost("SetClientPerHour/{Id}/{perHour}/{client}")]
        public async Task<HttpStatusCode> SetClientPerHour(int Id, int perHour, int client,[FromBody] SetTermDTO setTerm)
        {
            float oldPerHour = 0;
            var entity = await DBContext.EmpClientPerHour.FirstOrDefaultAsync(s => s.EmployeeId == Id && s.ClientId==client);
            if (entity == null)
            {
                EmpClientPerHour ct = new EmpClientPerHour();
                ct.EmployeeId = Id;
                ct.ClientId = client;
                ct.PerHour = perHour;
                DBContext.EmpClientPerHour.Add(ct);
            }
            else if (entity != null && entity.PerHour != perHour)
            {
                oldPerHour = entity.PerHour;
                entity.PerHour = perHour;
            }
            var emp = await DBContext.Employee.FirstOrDefaultAsync(s => s.Id == setTerm.ChangeBy);
            EmpClientPerHourHistory cth = new EmpClientPerHourHistory();
            cth.ClientId = client;
            cth.EmployeeId = Id;
            cth.OldPerHour = oldPerHour;
            cth.NewPerHour = perHour;
            cth.ReasonForChange = setTerm.ReasonForChange; 
            cth.ChangeDate = DateTime.Now;
            cth.ChangeBy = emp.FirstName+" "+emp.LastName; 
            DBContext.EmpClientPerHourHistory.Add(cth);
            await DBContext.SaveChangesAsync();
            _cache.Remove(cacheKey);
            return HttpStatusCode.OK;
        }

        [HttpGet("GetEmpClientPerHourHistory/{id}")]
        public async Task<ActionResult<List<EmpClientPerHourHistoryDTO>>> GetEmpClientPerHourHistory(int id)
        {
            var List = await DBContext.EmpClientPerHourHistory.Where(x => x.EmployeeId == id).Select(
                s => new EmpClientPerHourHistoryDTO
                {
                    Id = s.Id,
                    EmployeeId = s.EmployeeId,
                    ClientId = s.ClientId,
                    OldPerHour = s.OldPerHour,
                    NewPerHour = s.NewPerHour,
                    Employee = s.Employee.FirstName+ " "+s.Employee.LastName,
                    Client = s.Client.ClientName,
                    ReasonForChange = s.ReasonForChange,
                    ChangeDate = s.ChangeDate,
                    ChangeBy = s.ChangeBy,
                }
            ).ToListAsync();
            if (List.Count < 0)
            {
                return NotFound();
            }
            else
            {
                return List;
            }
        }
        [HttpGet("GetEmpClientPerHour")]
        public async Task<ActionResult<List<EmpClientPerHourDTO>>> GetEmpClientPerHour(int id)
        {
            var List = await DBContext.EmpClientPerHour.Select(
                s => new EmpClientPerHourDTO
                {
                    Id = s.Id,
                    EmployeeId = s.EmployeeId,
                    ClientId = s.ClientId,
                    PerHour = s.PerHour,
                    Employee = s.Employee.FirstName+ " "+s.Employee.LastName,
                    Client = s.Client.ClientName
                }
            ).ToListAsync();

            if (List.Count < 0)
            {
                return NotFound();
            }
            else
            {
                return List;
            }
        }
        [HttpGet("GetEmployeeForDropdown")]
        public async Task<ActionResult<List<EmployeeDTO>>> GetEmployeeForDropdown()
        {

            var List = await DBContext.Employee.Where(x=>x.Status.ToLower() == "active" && x.User == null).Select(
                s => new EmployeeDTO
                {
                    Id = s.Id,
                    FullName = s.LastName+", "+s.FirstName,
                }
            ).ToListAsync();
            
            if (List.Count < 0)
            {
                return NotFound();
            }
            else
            {
                return List;
            }
        }

        [HttpGet("GetEmployeeCount")]
        public ActionResult<int> GetEmployeeCount()
        {
            return  DBContext.Employee.Where(x=>x.Status.ToLower() == "active").Count();
        }
    }
}