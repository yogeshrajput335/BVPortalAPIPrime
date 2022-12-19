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
    public class TimesheetController : ControllerBase
    {
        private readonly BVContext DBContext;

        public TimesheetController(BVContext DBContext)
        {
            this.DBContext = DBContext;
        }

        [HttpGet("GetTimesheet")]
        public async Task<ActionResult<List<TimesheetDTO>>> Get()
        {
            var List = await DBContext.Timesheet.Select(
                s => new TimesheetDTO
                {
                    Id = s.Id,
                    EmployeeId = s.EmployeeId,
                    EmployeeName = s.Employee.FirstName+" "+s.Employee.LastName,
                    ProjectName=s.Project.ProjectName,
                    ProjectId=s.ProjectId,
                    WeekEndingDate = s.WeekEndingDate,
                    Month = s.Month,
                    Year = s.Year,
                    CreatedDate = s.CreatedDate,
                    CreatedBy = s.CreatedBy,
                    Duration = s.Duration,
                    Status = s.Status,
                    ClientName =s.Project.Client.ClientName
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
        
        [HttpGet("GetTimesheetById/{id}")]
        public async Task<ActionResult<TimesheetDTO>> GetTimesheetById(int id)
        {
            var List = await DBContext.Timesheet.Where(x=>x.Id==id).Select(
                s => new TimesheetDTO
                {
                    Id = s.Id,
                    EmployeeId = s.EmployeeId,
                    EmployeeName = s.Employee.FirstName+" "+s.Employee.LastName,
                    ProjectName=s.Project.ProjectName,
                    ProjectId=s.ProjectId,
                    WeekEndingDate = s.WeekEndingDate,
                    Month = s.Month,
                    Year = s.Year,
                    CreatedDate = s.CreatedDate,
                    CreatedBy = s.CreatedBy,
                    Duration = s.Duration,
                    Status = s.Status,
                    Detail =s.TimesheetDetail.ToList()
                }
            ).FirstOrDefaultAsync();
            
            if (List==null)
            {
                return NotFound();
            }
            else
            {
                return List;
            }
        }

        [HttpPost("InsertTimesheet")]
        public async Task < HttpStatusCode > InsertTimesheet(TimesheetDataDTO s) {
            List<TimesheetDetail> details = new List<TimesheetDetail>();
            for (var i = 0; i < s.date.Count; i++)
            {
                var detail = new TimesheetDetail() {
                    EmployeeId = s.timesheet.EmployeeId,
                    ProjectId=s.timesheet.ProjectId,
                    WorkDay=s.date[i],
                    Hours = Convert.ToInt32(s.data[i])
                };
                details.Add(detail);
            }
            var entity = new Timesheet() {
                    EmployeeId = s.timesheet.EmployeeId,
                    ProjectId=s.timesheet.ProjectId,
                    WeekEndingDate = s.timesheet.WeekEndingDate,
                    Month = s.timesheet.Month,
                    Year = s.timesheet.Year,
                    CreatedDate = s.timesheet.CreatedDate,
                    CreatedBy = s.timesheet.CreatedBy,
                    Duration = s.timesheet.Duration,
                    Status = s.timesheet.Status,
                    TimesheetDetail = details
            };
            DBContext.Timesheet.Add(entity);
            
            //DBContext.TimesheetDetail.AddRange(details);
            await DBContext.SaveChangesAsync();
            return HttpStatusCode.Created;
        }

        [HttpPut("UpdateTimesheet")]
        public async Task<HttpStatusCode> UpdateTimesheet(TimesheetDTO Timesheet) {
            var entity = await DBContext.Timesheet.FirstOrDefaultAsync(s => s.Id == Timesheet.Id);
            entity.EmployeeId = Timesheet.EmployeeId;
            entity.ProjectId=Timesheet.ProjectId;
            entity.WeekEndingDate = Timesheet.WeekEndingDate;
            entity.Month = Timesheet.Month;
            entity.Year = Timesheet.Year;
            entity.CreatedDate = Timesheet.CreatedDate;
            entity.CreatedBy = Timesheet.CreatedBy;
            entity.Duration = Timesheet.Duration;
            entity.Status = Timesheet.Status;
            await DBContext.SaveChangesAsync();
            return HttpStatusCode.OK;
        }
        
        [HttpDelete("DeleteTimesheet/{Id}")]
        public async Task < HttpStatusCode > DeleteTimesheet(int Id) {
            var entity = new Timesheet() {
                Id = Id
            };
            DBContext.Timesheet.Attach(entity);
            DBContext.Timesheet.Remove(entity);
            await DBContext.SaveChangesAsync();
            return HttpStatusCode.OK;
        }
    }
}