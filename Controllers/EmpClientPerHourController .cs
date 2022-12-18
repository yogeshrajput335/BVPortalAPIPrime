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
    public class EmpClientPerHourController : ControllerBase
    {
        private readonly BVContext DBContext;

        public EmpClientPerHourController(BVContext DBContext)
        {
            this.DBContext = DBContext;
        }

        [HttpGet("GetEmpClientPerHour")]
        public async Task<ActionResult<List<EmpClientPerHourDTO>>> Get()
        {
            var List = await DBContext.EmpClientPerHour.Select(
                s => new EmpClientPerHourDTO
                {
                    Id = s.Id,
                    EmployeeId = s.EmployeeId,
                    ClientId = s.ClientId,
                    PerHour = s.PerHour,
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

        [HttpPost("InsertEmpClientPerHour")]
        public async Task < HttpStatusCode > InsertEmpClientPerHour(EmpClientPerHourDTO s) {
            var entity = new EmpClientPerHour() {
                    EmployeeId = s.EmployeeId,
                    ClientId = s.ClientId,
                    PerHour = s.PerHour,
                };
            DBContext.EmpClientPerHour.Add(entity);
            await DBContext.SaveChangesAsync();
            return HttpStatusCode.Created;
        }

        [HttpPut("UpdateEmpClientPerHour")]
        public async Task<HttpStatusCode> UpdateEmpClientPerHour(EmpClientPerHourDTO EmpClientPerHour) {
            var entity = await DBContext.EmpClientPerHour.FirstOrDefaultAsync(s => s.Id == EmpClientPerHour.Id);
            entity.EmployeeId = EmpClientPerHour.EmployeeId;
            entity.ClientId = EmpClientPerHour.ClientId;
            entity.PerHour = EmpClientPerHour.PerHour;
            await DBContext.SaveChangesAsync();
            return HttpStatusCode.OK;
        }
        
        [HttpDelete("DeleteEmpClientPerHour/{Id}")]
        public async Task < HttpStatusCode > DeleteEmpClientPerHour(int Id) {
            var entity = new EmpClientPerHour() {
                Id = Id
            };
            DBContext.EmpClientPerHour.Attach(entity);
            DBContext.EmpClientPerHour.Remove(entity);
            await DBContext.SaveChangesAsync();
            return HttpStatusCode.OK;
        }
    }
}