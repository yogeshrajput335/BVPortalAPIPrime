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
    public class TimesheetApprovalController : ControllerBase
    {
        private readonly BVContext DBContext;

        public TimesheetApprovalController(BVContext DBContext)
        {
            this.DBContext = DBContext;
        }

        [HttpGet("GetTimesheetApproval")]
        public async Task<ActionResult<List<TimesheetApprovalDTO>>> Get()
        {
            var List = await DBContext.TimesheetApproval.Select(
                s => new TimesheetApprovalDTO
                {
                    Id = s.Id,
                    TimesheetId = s.TimesheetId,
                    ApproverId=s.ApproverId,
                    Notes = s.Notes,
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

        [HttpPost("InsertTimesheetApproval")]
        public async Task < HttpStatusCode > InsertTimesheetApproval(TimesheetApprovalDTO s) {
            var entity = new TimesheetApproval() {
                    TimesheetId = s.TimesheetId,
                    ApproverId=s.ApproverId,
                    Notes = s.Notes,
                    Status = s.Status
            };
            DBContext.TimesheetApproval.Add(entity);
            await DBContext.SaveChangesAsync();
            return HttpStatusCode.Created;
        }

        [HttpPut("UpdateTimesheetApproval")]
        public async Task<HttpStatusCode> UpdateTimesheetApproval(TimesheetApprovalDTO TimesheetApproval) {
            var entity = await DBContext.TimesheetApproval.FirstOrDefaultAsync(s => s.Id == TimesheetApproval.Id);
            entity.TimesheetId = TimesheetApproval.TimesheetId;
            entity.ApproverId=TimesheetApproval.ApproverId;
            entity.Notes = TimesheetApproval.Notes;
            entity.Status = TimesheetApproval.Status;
            await DBContext.SaveChangesAsync();
            return HttpStatusCode.OK;
        }
        
        [HttpDelete("DeleteTimesheetApproval/{Id}")]
        public async Task < HttpStatusCode > DeleteTimesheetApproval(int Id) {
            var entity = new TimesheetApproval() {
                Id = Id
            };
            DBContext.TimesheetApproval.Attach(entity);
            DBContext.TimesheetApproval.Remove(entity);
            await DBContext.SaveChangesAsync();
            return HttpStatusCode.OK;
        }
    }
}