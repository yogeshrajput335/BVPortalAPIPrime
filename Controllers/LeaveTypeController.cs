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
    public class LeaveTypeController : ControllerBase
    {
        private readonly BVContext DBContext;

        public LeaveTypeController(BVContext DBContext)
        {
            this.DBContext = DBContext;
        }

        [HttpGet("GetLeaveTypes")]
        public async Task<ActionResult<List<LeaveTypeDTO>>> Get()
        {
            var List = await DBContext.LeaveType.Select(
                s => new LeaveTypeDTO
                {
                    Id = s.Id,
                    Type = s.Type,
                    Description = s.Description,
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

        [HttpPost("InsertLeaveType")]
        public async Task < HttpStatusCode > InsertLeaveType(LeaveTypeDTO s) {
            var entity = new LeaveType() {
                Type = s.Type,
                Description = s.Description,
                Status = s.Status
            };
            DBContext.LeaveType.Add(entity);
            await DBContext.SaveChangesAsync();
            return HttpStatusCode.Created;
        }

        [HttpPut("UpdateLeaveType")]
        public async Task<HttpStatusCode> UpdateLeaveType(LeaveTypeDTO LeaveType) {
            var entity = await DBContext.LeaveType.FirstOrDefaultAsync(s => s.Id == LeaveType.Id);
            entity.Type = LeaveType.Type;
            entity.Description = LeaveType.Description;
            entity.Status = LeaveType.Status;
            await DBContext.SaveChangesAsync();
            return HttpStatusCode.OK;
        }
        
        [HttpDelete("DeleteLeaveType/{Id}")]
        public async Task < HttpStatusCode > DeleteLeaveType(int Id) {
            var entity = new LeaveType() {
                Id = Id
            };
            DBContext.LeaveType.Attach(entity);
            DBContext.LeaveType.Remove(entity);
            await DBContext.SaveChangesAsync();
            return HttpStatusCode.OK;
        }
    }
}