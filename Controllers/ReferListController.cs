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
    public class ReferListController : ControllerBase
    {
        private readonly BVContext DBContext;

        public ReferListController(BVContext DBContext)
        {
            this.DBContext = DBContext;
        }

        [HttpGet("GetReferList")]
        public async Task<ActionResult<List<ReferListDTO>>> Get()
        {
            var List = await DBContext.ReferList.Select(
                s => new ReferListDTO
                {
                    Id = s.Id,
                    FirstName = s.FirstName,
                    LastName = s.LastName,
                    PhoneNo = s.PhoneNo,
                    Email = s.Email,
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

        [HttpPost("InsertReferList")]
        public async Task < HttpStatusCode > InsertReferList(ReferListDTO s) {
            var entity = new ReferList() {
                    FirstName = s.FirstName,
                    LastName = s.LastName,
                    PhoneNo = s.PhoneNo,
                    Email = s.Email,
                    Status = s.Status
            };
            DBContext.ReferList.Add(entity);
            await DBContext.SaveChangesAsync();
            return HttpStatusCode.Created;
        }

        [HttpPut("UpdateReferList")]
        public async Task<HttpStatusCode> UpdateReferList(ReferListDTO ReferList) {
            var entity = await DBContext.ReferList.FirstOrDefaultAsync(s => s.Id == ReferList.Id);
            entity.FirstName = ReferList.FirstName;
            entity.LastName = ReferList.LastName;
            entity.PhoneNo = ReferList.PhoneNo;
            entity.Email = ReferList.Email;
            entity.Status = ReferList.Status;
            await DBContext.SaveChangesAsync();
            return HttpStatusCode.OK;
        }

        [HttpDelete("DeleteReferList/{Id}")]
        public async Task < HttpStatusCode > DeleteReferList(int Id) {
            var entity = new ReferList() {
                Id = Id
            };
            DBContext.ReferList.Attach(entity);
            DBContext.ReferList.Remove(entity);
            await DBContext.SaveChangesAsync();
            return HttpStatusCode.OK;
        }
        
        [HttpDelete("MoveToCandidate/{Id}/{EmployeeId}")]
        public async Task < HttpStatusCode > MoveToCandidate(int Id,int EmployeeId) {
            var entity = await DBContext.ReferList.FirstOrDefaultAsync(s => s.Id == Id);
            entity.Status = "Moved to candidate";

             var candidate = new Candidate() {
                    FirstName = entity.FirstName,
                    LastName = entity.LastName,
                    PhoneNo = entity.PhoneNo,
                    Email = entity.Email,
                    Status = "REFERRED",
                    ReferBy = EmployeeId
            };
            DBContext.Candidates.Add(candidate);

            await DBContext.SaveChangesAsync();
            return HttpStatusCode.OK;
        }
    }
}