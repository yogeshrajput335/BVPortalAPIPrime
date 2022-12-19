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
    public class TimesheetMasterController : ControllerBase
    {
        private readonly BVContext DBContext;

        public TimesheetMasterController(BVContext DBContext)
        {
            this.DBContext = DBContext;
        }

        [HttpGet("GetTimesheetMaster")]
        public async Task<ActionResult<List<TimesheetMasterDTO>>> Get()
        {
            var List = await DBContext.TimesheetMaster.Select(
                s => new TimesheetMasterDTO
                {
                    Id = s.Id,
                    Year = s.Year,
                    Month = s.Month,
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

        [HttpPost("InsertTimesheetMaster")]
        public async Task < HttpStatusCode > InsertTimesheetMaster(TimesheetMasterDTO s) {
            var entity = new TimesheetMaster() {
                Year = s.Year,
                Month = s.Month,
                Status = s.Status
            };
            DBContext.TimesheetMaster.Add(entity);
            await DBContext.SaveChangesAsync();
            return HttpStatusCode.Created;
        }

        [HttpPut("UpdateTimesheetMaster")]
        public async Task<HttpStatusCode> UpdateTimesheetMaster(TimesheetMasterDTO TimesheetMaster) {
            var entity = await DBContext.TimesheetMaster.FirstOrDefaultAsync(s => s.Id == TimesheetMaster.Id);
            entity.Year = TimesheetMaster.Year;
            entity.Month = TimesheetMaster.Month;
            entity.Status = TimesheetMaster.Status;
            await DBContext.SaveChangesAsync();
            return HttpStatusCode.OK;
        }
        
        [HttpDelete("DeleteTimesheetMaster/{Id}")]
        public async Task < HttpStatusCode > DeleteTimesheetMaster(int Id) {
            var entity = new TimesheetMaster() {
                Id = Id
            };
            DBContext.TimesheetMaster.Attach(entity);
            DBContext.TimesheetMaster.Remove(entity);
            await DBContext.SaveChangesAsync();
            return HttpStatusCode.OK;
        }
    }
}