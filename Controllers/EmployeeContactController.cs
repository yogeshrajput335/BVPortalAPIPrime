using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using BVPortalApi.DTO;
using BVPortalApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BVPortalApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeContactController : ControllerBase
    {
        private readonly BVContext DBContext;

        public EmployeeContactController(BVContext DBContext)
        {
            this.DBContext = DBContext;
        }

        [HttpGet("GetEmployeeContact")]
        public async Task<ActionResult<List<EmployeeContactDTO>>> GetEmployeeContact()
        {
            var List = await DBContext.EmployeeContact.Select(
                s => new EmployeeContactDTO
                {
                    Id = s.Id,
                    EmployeeName = s.Employee.FirstName + " "+ s.Employee.LastName,
                    PersonalEmailId = s.PersonalEmailId,
                    PhoneNumber = s.PhoneNumber,
                    WorkEmail = s.WorkEmail,
                    EmergencyContactName = s.EmergencyContactName,
                    EmergencyContactNumber = s.EmergencyContactNumber
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
            var entity = new EmployeeContact() {
                    EmployeeId = s.EmployeeId,
                    PersonalEmailId = s.PersonalEmailId,
                    PhoneNumber = s.PhoneNumber,
                    WorkEmail = s.WorkEmail,
                    EmergencyContactName = s.EmergencyContactName,
                    EmergencyContactNumber = s.EmergencyContactNumber
            };
            DBContext.EmployeeContact.Add(entity);
            await DBContext.SaveChangesAsync();
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
            return HttpStatusCode.OK;
        }
    }
}