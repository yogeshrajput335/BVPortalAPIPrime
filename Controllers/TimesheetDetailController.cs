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
    public class TimesheetDetailController : ControllerBase
    {
        private readonly BVContext DBContext;

        public TimesheetDetailController(BVContext DBContext)
        {
            this.DBContext = DBContext;
        }

        [HttpGet("GetTimesheetDetail")]
        public async Task<ActionResult<List<TimesheetDetailDTO>>> Get()
        {
            var List = await DBContext.TimesheetDetail.Select(
                s => new TimesheetDetailDTO
                {
                    Id = s.Id,
                    TimesheetId = s.TimesheetId,
                    ProjectId=s.ProjectId,
                    EmployeeId=s.EmployeeId,
                    WorkDay = s.WorkDay,
                    Hours = s.Hours
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

        [HttpPost("InsertTimesheetDetail")]
        public async Task < HttpStatusCode > InsertTimesheetDetail(TimesheetDetailDTO s) {
            var entity = new TimesheetDetail() {
                    TimesheetId = s.TimesheetId,
                    ProjectId=s.ProjectId,
                    EmployeeId=s.EmployeeId,
                    WorkDay = s.WorkDay,
                    Hours = s.Hours
            };
            DBContext.TimesheetDetail.Add(entity);
            await DBContext.SaveChangesAsync();
            return HttpStatusCode.Created;
        }

        [HttpPut("UpdateTimesheetDetail")]
        public async Task<HttpStatusCode> UpdateTimesheetDetail(TimesheetDetailDTO TimesheetDetail) {
            var entity = await DBContext.TimesheetDetail.FirstOrDefaultAsync(s => s.Id == TimesheetDetail.Id);
            entity.TimesheetId = TimesheetDetail.TimesheetId;
            entity.ProjectId=TimesheetDetail.ProjectId;
            entity.EmployeeId=TimesheetDetail.EmployeeId;
            entity.WorkDay = TimesheetDetail.WorkDay;
            entity.Hours = TimesheetDetail.Hours;
            await DBContext.SaveChangesAsync();
            return HttpStatusCode.OK;
        }
        
        [HttpDelete("DeleteTimesheetDetail/{Id}")]
        public async Task < HttpStatusCode > DeleteTimesheetDetail(int Id) {
            var entity = new TimesheetDetail() {
                Id = Id
            };
            DBContext.TimesheetDetail.Attach(entity);
            DBContext.TimesheetDetail.Remove(entity);
            await DBContext.SaveChangesAsync();
            return HttpStatusCode.OK;
        }
    }
}