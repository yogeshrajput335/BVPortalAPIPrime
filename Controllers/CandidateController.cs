using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using BVPortalApi.DTO;
using BVPortalApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace BVPortalApi.Controllers
{
    [ApiController]
    [Route("api/[controller]"), Authorize(Roles = "ADMIN")]
    public class CandidateController : ControllerBase
    {
        private const string cacheKey = "candidateList";
        private readonly BVContext DBContext; 
        private IMemoryCache _cache;
        private ILogger<CandidateController> _logger;
        private readonly IMapper _mapper;

        public CandidateController(BVContext DBContext, IMemoryCache cache, ILogger<CandidateController> logger, IMapper mapper)
        {
            this.DBContext = DBContext;
             _cache = cache ?? throw new ArgumentNullException(nameof(cache));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper; 
        }

        [HttpGet("GetCandidates")]
        public async Task<ActionResult<List<CandidateDTO>>> Get()
        {
            // var List = await DBContext.Candidates.Select(
            //     s => new CandidateDTO
            //     {
            //         Id = s.Id,
            //         FirstName = s.FirstName,
            //         LastName = s.LastName,
            //         Name = s.LastName+", "+s.FirstName,
            //         PhoneNo = s.PhoneNo,
            //         Email=s.Email,
            //         Status = s.Status,
            //         ReferBy = s.ReferBy,
            //         ReferByName = s.Employee.FirstName + " "+s.Employee.LastName,
            //         JobId = s.JobId,
            //         JobName = s.Openjobs.JobName,
            //         Technology = s.Technology,
            //         Visa = s.Visa,
            //         Rate = s.Rate,
            //         Client = s.Client,
            //         ClientContact = s.ClientContact,
            //         ClientMail = s.ClientMail,
            //         Vendor = s.Vendor,
            //         VendorContact = s.VendorContact,
            //         VendorMail = s.VendorMail,
            //         CreatedDate = s.CreatedDate
            //     }
            // ).ToListAsync();
             _logger.Log(LogLevel.Information, "Trying to fetch the list of candidates from cache.");
            if (_cache.TryGetValue(cacheKey, out List<CandidateDTO> List))
            {
                _logger.Log(LogLevel.Information, "Candidate list found in cache.");
                
            }
            else
            {
                _logger.Log(LogLevel.Information, "Candidate list not found in cache. Fetching from database.");
                List = _mapper.Map<List<CandidateDTO>>(await DBContext.Candidates.Include(x=>x.Employee).ToListAsync());
                List = _mapper.Map<List<CandidateDTO>>(await DBContext.Candidates.Include(x=>x.Openjobs).ToListAsync());
                var cacheEntryOptions = new MemoryCacheEntryOptions()
                        .SetSlidingExpiration(TimeSpan.FromSeconds(60))
                        .SetAbsoluteExpiration(TimeSpan.FromSeconds(3600))
                        .SetPriority(CacheItemPriority.Normal)
                        .SetSize(1024);
                _cache.Set(cacheKey, List, cacheEntryOptions);
            }
            
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
            // var entity = new Candidate() {
            //         Id = s.Id,
            //         FirstName = s.FirstName,
            //         LastName = s.LastName,
            //         PhoneNo = s.PhoneNo,
            //         Email=s.Email,
            //         Status = s.Status,
            //         ReferBy = s.ReferBy,
            //         Technology = s.Technology,
            //         Visa = s.Visa,
            //         Rate = s.Rate,
            //         Client = s.Client,
            //         ClientContact = s.ClientContact,
            //         ClientMail = s.ClientMail,
            //         Vendor = s.Vendor,
            //         VendorContact = s.VendorContact,
            //         VendorMail = s.VendorMail,
            //         CreatedDate = s.CreatedDate
            // };
            var entity = _mapper.Map<Candidate>(s);
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
            _cache.Remove(cacheKey);
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
            _cache.Remove(cacheKey);
            return HttpStatusCode.OK;
        }
        [HttpPost("DeleteCandidates")]
        public  async Task<HttpStatusCode> DeleteCandidates(List<CandidateDTO> candidates) {
            List<Candidate> entities = candidates.Select(i => new Candidate(){
                Id = i.Id
            }).ToList();
            DBContext.Candidates.AttachRange(entities);
            DBContext.Candidates.RemoveRange(entities);
            await DBContext.SaveChangesAsync();
            _cache.Remove(cacheKey);
            return HttpStatusCode.OK;
        }
    }
}