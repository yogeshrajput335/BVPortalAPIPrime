using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using BVPortalApi.CommonFeatures;
using BVPortalApi.CommonFeatures.Contracts;
using BVPortalApi.DTO;
using BVPortalApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BVPortalApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeBasicInfoController : ControllerBase
    {
        private readonly BVContext DBContext;

        public EmployeeBasicInfoController(BVContext DBContext)
        {
            this.DBContext = DBContext;
        }

        [HttpGet("GetEmployeeBasicInfo")]
        public async Task<ActionResult<List<EmployeeBasicInfoDTO>>> GetEmployeeBasicInfo()
        {
            var List = await DBContext.EmployeeBasicInfo.Select(
                s => new EmployeeBasicInfoDTO
                {
                    Id = s.Id,
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
            var entity = new EmployeeBasicInfo() {
                    EmployeeId = s.EmployeeId,
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
            };
            DBContext.EmployeeBasicInfo.Add(entity);
            await DBContext.SaveChangesAsync();
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
            return HttpStatusCode.OK;
        }
    }
}