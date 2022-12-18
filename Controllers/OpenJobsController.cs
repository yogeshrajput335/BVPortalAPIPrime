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
    [Route("api/[controller]")]
    public class OpenjobsController : ControllerBase
    {
        private readonly BVContext DBContext;

        public OpenjobsController(BVContext DBContext)
        {
            this.DBContext = DBContext;
        }

        [HttpGet("GetOpenjobs"), Authorize(Roles = "EMPLOYEE,ADMIN")]
        public async Task<ActionResult<List<OpenjobsDTO>>> Get()
        {
            var List = await DBContext.Openjobs.Where(x=>x.Status=="ACTIVE").Select(
                s => new OpenjobsDTO
                {
                    Id = s.Id,
                    JobName = s.JobName,
                    Profile = s.Profile,
                    Description = s.Description,
                    StartDate = s.StartDate,
                    Country = s.Country,
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

        [HttpGet("GetDeletedjobs"), Authorize(Roles = "ADMIN")]
        public async Task<ActionResult<List<OpenjobsDTO>>> GetDeletedjobs()
        {
            var List = await DBContext.Openjobs.Where(x=>x.Status=="" || x.Status=="INACTIVE").Select(
                s => new OpenjobsDTO
                {
                    Id = s.Id,
                    JobName = s.JobName,
                    Profile = s.Profile,
                    Description = s.Description,
                    StartDate = s.StartDate,
                    Country = s.Country,
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

        [HttpPut("UpdateStatusActiveOpenjobs/{Id}"), Authorize(Roles = "ADMIN")]
        public async Task<HttpStatusCode> UpdateStatusActiveOpenjobs(int Id ) {
            var entity = await DBContext.Openjobs.FirstOrDefaultAsync(s => s.Id == Id);
            
            entity.Status = "ACTIVE";
            await DBContext.SaveChangesAsync();
            return HttpStatusCode.OK;
        }

        [HttpPost("InsertOpenjobs"), Authorize(Roles = "ADMIN")]
        public async Task < HttpStatusCode > InsertOpenjobs(OpenjobsDTO s) {
            var entity = new Openjobs() {
                JobName = s.JobName,
                Description = s.Description,
                Profile = s.Profile,
                StartDate = s.StartDate,
                Country = s.Country,
                Status = s.Status
            };
            DBContext.Openjobs.Add(entity);
            await DBContext.SaveChangesAsync();
            return HttpStatusCode.Created;
        }

        [HttpPut("UpdateOpenjobs"), Authorize(Roles = "ADMIN")]
        public async Task<HttpStatusCode> UpdateOpenjobs(OpenjobsDTO Openjobs) {
            var entity = await DBContext.Openjobs.FirstOrDefaultAsync(s => s.Id == Openjobs.Id);
            entity.JobName = Openjobs.JobName;
            entity.Profile = Openjobs.Profile;
            entity.Description = Openjobs.Description;
            entity.StartDate = Openjobs.StartDate;
            entity.Country = Openjobs.Country;
            entity.Status = Openjobs.Status;
            await DBContext.SaveChangesAsync();
            return HttpStatusCode.OK;
        }

        [HttpPut("UpdateStatusInactiveOpenjobs/{Id}"), Authorize(Roles = "ADMIN")]
        public async Task<HttpStatusCode> UpdateStatusInactiveOpenjobs(int Id ) {
            var entity = await DBContext.Openjobs.FirstOrDefaultAsync(s => s.Id == Id);
            
            entity.Status = "INACTIVE";
            await DBContext.SaveChangesAsync();
            return HttpStatusCode.OK;
        }

        [HttpDelete("DeleteOpenjobs/{Id}"), Authorize(Roles = "ADMIN")]
        public async Task < HttpStatusCode > DeleteOpenjobs(int Id) {
            var entity = new Openjobs() {
                Id = Id
            };
            DBContext.Openjobs.Attach(entity);
            DBContext.Openjobs.Remove(entity);
            await DBContext.SaveChangesAsync();
            return HttpStatusCode.OK;
        }
        
        [HttpPost("ApplyJob"), Authorize(Roles = "EMPLOYEE")]
        public async Task < HttpStatusCode > ApplyJob(ApplyJobDTO s) {
            var emp = DBContext.Employee.Where(x=>x.Id==s.EmployeeId).FirstOrDefault();
            //  : Add jobid in candidate  
                
            
           var entity = new Candidate() {
                    JobId = s.JobId,
                    FirstName = emp.FirstName,
                    LastName = emp.LastName,
                    PhoneNo = emp.PhoneNumber,
                    Email=emp.Email,
                    Status = "SELF-REFER",
                    ReferBy = emp.Id
            };
            DBContext.Candidates.Add(entity);
            await DBContext.SaveChangesAsync();
            return HttpStatusCode.Created;
        }
    }
    
}