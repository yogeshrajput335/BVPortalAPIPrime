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
    public class CandidateController : ControllerBase
    {
        private readonly BVContext DBContext;

        public CandidateController(BVContext DBContext)
        {
            this.DBContext = DBContext;
        }

        [HttpGet("GetCandidates")]
        public async Task<ActionResult<List<CandidateDTO>>> Get()
        {
            var List = await DBContext.Candidates.Select(
                s => new CandidateDTO
                {
                    Id = s.Id,
                    FirstName = s.FirstName,
                    LastName = s.LastName,
                    PhoneNo = s.PhoneNo,
                    Email=s.Email,
                    Status = s.Status,
                    ReferBy = s.ReferBy,
                    ReferByName = s.Employee.FirstName + " "+s.Employee.LastName,
                    JobId = s.JobId,
                    JobName = s.Openjobs.JobName,
                    Technology = s.Technology,
                    Visa = s.Visa,
                    Rate = s.Rate,
                    Client = s.Client,
                    ClientContact = s.ClientContact,
                    ClientMail = s.ClientMail,
                    Vendor = s.Vendor,
                    VendorContact = s.VendorContact,
                    VendorMail = s.VendorMail,
                    CreatedDate = s.CreatedDate
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

        [HttpPost("InsertCandidate")]
        public async Task < HttpStatusCode > InsertCandidate(CandidateDTO s) {
            var entity = new Candidate() {
                    Id = s.Id,
                    FirstName = s.FirstName,
                    LastName = s.LastName,
                    PhoneNo = s.PhoneNo,
                    Email=s.Email,
                    Status = s.Status,
                    ReferBy = s.ReferBy,
                    Technology = s.Technology,
                    Visa = s.Visa,
                    Rate = s.Rate,
                    Client = s.Client,
                    ClientContact = s.ClientContact,
                    ClientMail = s.ClientMail,
                    Vendor = s.Vendor,
                    VendorContact = s.VendorContact,
                    VendorMail = s.VendorMail,
                    CreatedDate = s.CreatedDate
            };
            DBContext.Candidates.Add(entity);
            await DBContext.SaveChangesAsync();
            return HttpStatusCode.Created;
        }

        [HttpPut("UpdateCandidate")]
        public async Task<HttpStatusCode> UpdateCandidaet(CandidateDTO Candidate) {
            var entity = await DBContext.Candidates.FirstOrDefaultAsync(s => s.Id == Candidate.Id);
            entity.FirstName = Candidate.FirstName;
            entity.LastName = Candidate.LastName;
            entity.PhoneNo = Candidate.PhoneNo;
            entity.Email = Candidate.Email;
            entity.Status = Candidate.Status;
            entity.ReferBy = Candidate.ReferBy;
            entity.Technology = Candidate.Technology;
            entity.Visa = Candidate.Visa;
            entity.Rate = Candidate.Rate;
            entity.Client = Candidate.Client;
            entity.ClientContact = Candidate.ClientContact;
            entity.ClientMail = Candidate.ClientMail;
            entity.Vendor = Candidate.Vendor;
            entity.VendorContact = Candidate.VendorContact;
            entity.VendorMail = Candidate.VendorMail;
            entity.CreatedDate = Candidate.CreatedDate;
            await DBContext.SaveChangesAsync();
            return HttpStatusCode.OK;
        }
        
        [HttpDelete("DeleteCandidate/{Id}")]
        public async Task < HttpStatusCode > DeleteCandidate(int Id) {
            var entity = new Candidate() {
                Id = Id
            };
            DBContext.Candidates.Attach(entity);
            DBContext.Candidates.Remove(entity);
            await DBContext.SaveChangesAsync();
            return HttpStatusCode.OK;
        }
    }
}