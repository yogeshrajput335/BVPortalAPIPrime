using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using BVPortalApi.DTO;
using BVPortalApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BVPortalApi.Controllers
{
    [ApiController]
    [Route("api/[controller]"), Authorize(Roles = "ADMIN,EMPLOYEE")]
    public class LeaveController : ControllerBase
    {
        private readonly BVContext DBContext;

        public LeaveController(BVContext DBContext)
        {
            this.DBContext = DBContext;
        }

        [HttpGet("GetLeave")]
        public async Task<ActionResult<List<LeaveDTO>>> Get()
        {
            var List = await DBContext.Leave.Select(
                s => new LeaveDTO
                {
                Id=s.Id,
                EmployeeId = s.EmployeeId,
                FullName = s.Employee.FirstName+" "+s.Employee.LastName,
                LeaveTypeId=s.LeaveTypeId,
                LeaveType =s.LeaveType.Type,
                FromDate=s.FromDate,
                ToDate=s.ToDate,
                Reason = s.Reason,
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

        [HttpPost("InsertLeave")]
        public async Task < HttpStatusCode > InsertLeave(LeaveDTO s) {
            var entity = new Leave() {
                EmployeeId = s.EmployeeId,
                LeaveTypeId=s.LeaveTypeId,
                FromDate=s.FromDate,
                ToDate=s.ToDate,
                Reason = s.Reason,
                Status = s.Status
            };
            DBContext.Leave.Add(entity);
            await DBContext.SaveChangesAsync();
            return HttpStatusCode.Created;
        }

        [HttpPut("UpdateLeave")]
        public async Task<HttpStatusCode> UpdateLeave(LeaveDTO Leave) {
            var entity = await DBContext.Leave.FirstOrDefaultAsync(s => s.Id == Leave.Id);
            entity.EmployeeId = Leave.EmployeeId;
            entity.LeaveTypeId = Leave.LeaveTypeId;
            entity.FromDate = Leave.FromDate;
            entity.ToDate = Leave.ToDate;
            entity.Status = Leave.Status;
            await DBContext.SaveChangesAsync();
            return HttpStatusCode.OK;
        }
        
        [HttpDelete("DeleteLeave/{Id}")]
        public async Task < HttpStatusCode > DeleteLeave(int Id) {
            var entity = new Leave() {
                Id = Id
            };
            DBContext.Leave.Attach(entity);
            DBContext.Leave.Remove(entity);
            await DBContext.SaveChangesAsync();
            return HttpStatusCode.OK;
        }
    }
}