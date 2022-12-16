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
    public class EmpClientPerHourHistoryController : ControllerBase
    {
        private readonly BVContext DBContext;

        public EmpClientPerHourHistoryController(BVContext DBContext)
        {
            this.DBContext = DBContext;
        }

        [HttpGet("GetEmpClientPerHourHistory")]
        public async Task<ActionResult<List<EmpClientPerHourHistoryDTO>>> Get()
        {
            var List = await DBContext.EmpClientPerHourHistory.Select(
                s => new EmpClientPerHourHistoryDTO
                {
                    Id = s.Id,
                    EmployeeId = s.EmployeeId,
                    ClientId = s.ClientId,
                    OldPerHour = s.OldPerHour,
                    NewPerHour = s.NewPerHour,
                    ReasonForChange =s.ReasonForChange,
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

        [HttpPost("InsertEmpClientPerHourHistory")]
        public async Task < HttpStatusCode > InsertEmpClientPerHourHistory(EmpClientPerHourHistoryDTO s) {
            var entity = new EmpClientPerHourHistory() {
                    EmployeeId = s.EmployeeId,
                    ClientId = s.ClientId,
                    OldPerHour = s.OldPerHour,
                    NewPerHour = s.NewPerHour,
                    ReasonForChange =s.ReasonForChange,
                    ChangeDate = s.ChangeDate,
                    ChangeBy = s.ChangeBy,
                };
            DBContext.EmpClientPerHourHistory.Add(entity);
            await DBContext.SaveChangesAsync();
            return HttpStatusCode.Created;
        }

        [HttpPut("UpdateEmpClientPerHourHistory")]
        public async Task<HttpStatusCode> UpdateEmpClientPerHourHistory(EmpClientPerHourHistoryDTO EmpClientPerHourHistory) {
            var entity = await DBContext.EmpClientPerHourHistory.FirstOrDefaultAsync(s => s.Id == EmpClientPerHourHistory.Id);
            entity.EmployeeId = EmpClientPerHourHistory.EmployeeId;
            entity.ClientId = EmpClientPerHourHistory.ClientId;
            entity.OldPerHour = EmpClientPerHourHistory.OldPerHour;
            entity.NewPerHour = EmpClientPerHourHistory.NewPerHour;
            entity.ReasonForChange = EmpClientPerHourHistory.ReasonForChange;
            entity.ChangeDate = EmpClientPerHourHistory.ChangeDate;
            entity.ChangeBy = EmpClientPerHourHistory.ChangeBy;
            await DBContext.SaveChangesAsync();
            return HttpStatusCode.OK;
        }
        
        [HttpDelete("DeleteEmpClientPerHourHistory/{Id}")]
        public async Task < HttpStatusCode > DeleteEmpClientPerHourHistory(int Id) {
            var entity = new EmpClientPerHourHistory() {
                Id = Id
            };
            DBContext.EmpClientPerHourHistory.Attach(entity);
            DBContext.EmpClientPerHourHistory.Remove(entity);
            await DBContext.SaveChangesAsync();
            return HttpStatusCode.OK;
        }
    }
}