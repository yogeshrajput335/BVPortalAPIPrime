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
    public class EmployeeBasicInfoController : ControllerBase
    {
        private const string cacheKey = "employeeBasicInfoList";
        private readonly BVContext DBContext;
        private IMemoryCache _cache;
        private ILogger<EmployeeBasicInfoController> _logger;
        private readonly IMapper _mapper;  

        public EmployeeBasicInfoController(BVContext DBContext, IMemoryCache cache, ILogger<EmployeeBasicInfoController> logger, IMapper mapper)
        {
            this.DBContext = DBContext;
            _cache = cache ?? throw new ArgumentNullException(nameof(cache));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper;
        }

        [HttpGet("GetEmployeeBasicInfo")]
        public async Task<ActionResult<List<EmployeeBasicInfoDTO>>> GetEmployeeBasicInfo()
        {
            // var List = await DBContext.EmployeeBasicInfo.Select(
            //     s => new EmployeeBasicInfoDTO
            //     {
            //         Id = s.Id,
            //         EmployeeName = s.Employee.FirstName + " "+ s.Employee.LastName,
            //         FatherName = s.FatherName,
            //         MotherName = s.MotherName,
            //         BloodGroup = s.BloodGroup,
            //         PersonalEmailId = s.PersonalEmailId,
            //         DateOfBirth = s.DateOfBirth,
            //         IsMarried = s.IsMarried,
            //         MaritalStatus = s.MaritalStatus,
            //         SpouseName = s.SpouseName,
            //         PermanentAddress = s.PermanentAddress,
            //         IsBothAddressSame = s.IsBothAddressSame,
            //         CurrentAddress = s.CurrentAddress,
            //         Gender = s.Gender
            //     }
            // ).ToListAsync();
             _logger.Log(LogLevel.Information, "Trying to fetch the list of Employee Basic Info from cache.");
            if (_cache.TryGetValue(cacheKey, out List<EmployeeBasicInfoDTO> List))
            {
                _logger.Log(LogLevel.Information, "Employee Basic Info list found in cache.");
                
            }
            else
            {
                _logger.Log(LogLevel.Information, "Employee Basic Info list not found in cache. Fetching from database.");
                List = _mapper.Map<List<EmployeeBasicInfoDTO>>(await DBContext.EmployeeBasicInfo.ToListAsync());
                
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
        
        [HttpGet("GetEmployeeBasicInfoByEmpId/{empId}")]
        public async Task<ActionResult<EmployeeBasicInfoDTO>> GetEmployeeBasicInfoByEmpId(int empId)
        {
            var List = await DBContext.EmployeeBasicInfo.Where(x=>x.EmployeeId==empId).Select(
                s => new EmployeeBasicInfoDTO
                {
                    Id = s.Id,
                    EmployeeId = s.EmployeeId,
                    EmployeeName = s.Employee.FirstName + " "+ s.Employee.LastName,
                    FatherName = s.FatherName,
                    MotherName = s.MotherName,
                    BloodGroup = s.BloodGroup,
                    PersonalEmailId = s.PersonalEmailId,
                    DateOfBirth = s.DateOfBirth,
                    IsMarried = s.IsMarried,
                    MaritalStatus = s.MaritalStatus,
                    SpouseName = s.SpouseName,
                    PermanentAddress = s.PermanentAddress,
                    IsBothAddressSame = s.IsBothAddressSame,
                    CurrentAddress = s.CurrentAddress,
                    Gender = s.Gender
                }
            ).FirstOrDefaultAsync();
            
            if (List==null)
            {
                return new EmployeeBasicInfoDTO(){Id =0,EmployeeId=empId};
            }
            else
            {
                return List;
            }
        }

        [HttpPost("InsertEmployeeBasicInfo")]
        public async Task < HttpStatusCode > InsertEmployeeBasicInfo(EmployeeBasicInfoDTO s) {
            // var entity = new EmployeeBasicInfo() {
            //         EmployeeId = s.EmployeeId,
            //         FatherName = s.FatherName,
            //         MotherName = s.MotherName,
            //         BloodGroup = s.BloodGroup,
            //         PersonalEmailId = s.PersonalEmailId,
            //         DateOfBirth = s.DateOfBirth,
            //         IsMarried = s.IsMarried,
            //         MaritalStatus = s.MaritalStatus,
            //         SpouseName = s.SpouseName,
            //         PermanentAddress = s.PermanentAddress,
            //         IsBothAddressSame = s.IsBothAddressSame,
            //         CurrentAddress = s.CurrentAddress,
            //         Gender = s.Gender
            // };
            var entity = _mapper.Map<EmployeeBasicInfo>(s);
            DBContext.EmployeeBasicInfo.Add(entity);
            await DBContext.SaveChangesAsync();
            _cache.Remove(cacheKey);
            return HttpStatusCode.Created;
        }

        [HttpPut("UpdateEmployeeBasicInfo")]
        public async Task<HttpStatusCode> UpdateEmployeeBasicInfo(EmployeeBasicInfoDTO Employee) {
            var entity = await DBContext.EmployeeBasicInfo.FirstOrDefaultAsync(s => s.Id == Employee.Id);
            entity.FatherName = Employee.FatherName;
            entity.MotherName = Employee.MotherName;
            entity.BloodGroup = Employee.BloodGroup;
            entity.PersonalEmailId = Employee.PersonalEmailId;
            entity.DateOfBirth = Employee.DateOfBirth;
            entity.IsMarried = Employee.IsMarried;
            entity.MaritalStatus = Employee.MaritalStatus;
            entity.SpouseName = Employee.SpouseName;
            entity.PermanentAddress = Employee.PermanentAddress;
            entity.IsBothAddressSame = Employee.IsBothAddressSame;
            entity.CurrentAddress = Employee.CurrentAddress;
            entity.Gender = Employee.Gender;
            await DBContext.SaveChangesAsync();
            _cache.Remove(cacheKey);
            return HttpStatusCode.OK;
        }
        
        [HttpDelete("DeleteEmployeeBasicInfo/{Id}")]
        public async Task < HttpStatusCode > DeleteEmployeeBasicInfo(int Id) {
            var entity = new EmployeeBasicInfo() {
                Id = Id
            };
            DBContext.EmployeeBasicInfo.Attach(entity);
            DBContext.EmployeeBasicInfo.Remove(entity);
            await DBContext.SaveChangesAsync();
            _cache.Remove(cacheKey);
            return HttpStatusCode.OK;
        }
    }
}