using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using BVPortalApi.CommonFeatures;
using BVPortalApi.CommonFeatures.Contracts;
using BVPortalApi.DTO;
using BVPortalApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BVPortalApi.Controllers
{
    [ApiController]
    [Route("api/[controller]"), Authorize(Roles = "ADMIN")]
    public class EmployeeController : ControllerBase
    {
        private readonly BVContext DBContext;

        public EmployeeController(BVContext DBContext)
        {
            this.DBContext = DBContext;
        }

        [HttpGet("GetEmployee")]
        public async Task<ActionResult<List<EmployeeDTO>>> Get()
        {
            var List = await DBContext.Employee.Select(
                s => new EmployeeDTO
                {
                    Id = s.Id,
                    FirstName = s.FirstName,
                    LastName = s.LastName,
                    Email = s.Email,
                    PhoneNumber = s.PhoneNumber,
                    EmployeeType = s.EmployeeType,
                    Status = s.Status
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

        [HttpPost("InsertEmployee")]
        public async Task < HttpStatusCode > InsertEmployee(EmployeeDTO s) {
            var entity = new Employee() {
                    FirstName = s.FirstName,
                    LastName = s.LastName,
                    Email = s.Email,
                    PhoneNumber = s.PhoneNumber,
                    EmployeeType = s.EmployeeType,
                    Status = s.Status
            };
            DBContext.Employee.Add(entity);
            await DBContext.SaveChangesAsync();
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
    }
}