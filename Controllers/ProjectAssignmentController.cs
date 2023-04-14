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
    public class ProjectAssignmentController : ControllerBase
    {
        private const string cacheKey = "assetList";
        private readonly BVContext DBContext;
        private IMemoryCache _cache;
        private ILogger<ProjectAssignmentController> _logger;
        private readonly IMapper _mapper;

        public ProjectAssignmentController(BVContext DBContext, IMemoryCache cache, ILogger<ProjectAssignmentController> logger, IMapper mapper)
        {
            this.DBContext = DBContext;
            _cache = cache ?? throw new ArgumentNullException(nameof(cache));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper;
        }

        [HttpGet("GetProjectAssignment")]
        public async Task<ActionResult<List<ProjectAssignmentDTO>>> Get()
        {
            // var List = await DBContext.ProjectAssignment.Select(
            //     s => new ProjectAssignmentDTO
            //     {
            //         Id = s.Id,
            //         ProjectId = s.ProjectId,
            //         EmployeeId  = s.EmployeeId,
            //         ProjectName = s.Project.ProjectName,
            //         EmployeeName  = s.Employee.FirstName + " " + s.Employee.LastName,
            //         Notes = s.Notes,
            //         StartDate = s.StartDate,
            //         EndDate = s.EndDate
            //        }
            // ).ToListAsync();
             _logger.Log(LogLevel.Information, "Trying to fetch the list of Project Assignments from cache.");
            if (_cache.TryGetValue(cacheKey, out List<ProjectAssignmentDTO> List))
            {
                _logger.Log(LogLevel.Information, "Project Assignment list found in cache.");
                
            }
            else
            {
                _logger.Log(LogLevel.Information, "Project Assignment list not found in cache. Fetching from database.");
                List = _mapper.Map<List<ProjectAssignmentDTO>>(await DBContext.ProjectAssignment.Include(x=>x.Project).ToListAsync());
                List = _mapper.Map<List<ProjectAssignmentDTO>>(await DBContext.ProjectAssignment.Include(x=>x.Employee).ToListAsync());
                
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

        [HttpPost("InsertProjectAssignment")]
        public async Task < HttpStatusCode > InsertProjectAssignment(ProjectAssignmentDTO s) {
            // var entity = new ProjectAssignment() {
            //         ProjectId = s.ProjectId,
            //         EmployeeId  = s.EmployeeId,
            //         Notes = s.Notes,
            //         StartDate = s.StartDate,
            //         EndDate = s.EndDate
            // };
            var entity = _mapper.Map<ProjectAssignment>(s);
            DBContext.ProjectAssignment.Add(entity);
            await DBContext.SaveChangesAsync();
            _cache.Remove(cacheKey);
            return HttpStatusCode.Created;
        }

        [HttpPut("UpdateProjectAssignment")]
        public async Task<HttpStatusCode> UpdateProjectAssignment(ProjectAssignmentDTO ProjectAssignment) {
            var entity = await DBContext.ProjectAssignment.FirstOrDefaultAsync(s => s.Id == ProjectAssignment.Id);
            entity.ProjectId = ProjectAssignment.ProjectId;
            entity.EmployeeId = ProjectAssignment.EmployeeId;
            entity.Notes = ProjectAssignment.Notes;
            entity.StartDate = ProjectAssignment.StartDate;
            entity.EndDate = ProjectAssignment.EndDate;
            await DBContext.SaveChangesAsync();
            _cache.Remove(cacheKey);
            return HttpStatusCode.OK;
        }

        [HttpDelete("DeleteProjectAssignment/{Id}")]
        public async Task < HttpStatusCode > DeleteProjectAssignment(int Id) {
            var entity = new ProjectAssignment() {
                Id = Id
            };
            DBContext.ProjectAssignment.Attach(entity);
            DBContext.ProjectAssignment.Remove(entity);
            await DBContext.SaveChangesAsync();
            _cache.Remove(cacheKey);
            return HttpStatusCode.OK;
        }
        
        [HttpGet("GetProjectEmpTree")]
        public async Task<ActionResult<List<ProjectEmpTreeDTO>>> GetProjectEmpTree()
        {
            
            var ClientList = await DBContext.Client.Where(x=>x.Status=="ACTIVE").Select(x=>x).OrderBy(x=>x.ClientName).ToListAsync();
            var ProjectList = await DBContext.Project.Where(x=>x.Status=="APPROVED").Select(x=>x).OrderBy(x=>x.ProjectName).ToListAsync();
             
            var List = await DBContext.ProjectAssignment.Select(
                s => new ProjectAssignmentDTO
                {
                    Id = s.Id,
                    ProjectId = s.ProjectId,
                    EmployeeId  = s.EmployeeId,
                    ProjectName = s.Project.ProjectName,
                    EmployeeName  = s.Employee.FirstName + " " + s.Employee.LastName,
                    Notes = s.Notes,
                    StartDate = s.StartDate,
                    EndDate = s.EndDate
                   }
            ).ToListAsync();
            List<ProjectEmpTreeDTO> tree = new List<ProjectEmpTreeDTO>();
            foreach (var c in ClientList)
            {
                List<ProjectEmpTreeDTO> ch = new List<ProjectEmpTreeDTO>();
                var ProAssigned = ProjectList.Where(x=>x.ClientId == c.Id);
                foreach (var item in ProAssigned)
                {
                    List<ProjectEmpTreeDTO> children = new List<ProjectEmpTreeDTO>();
                    var EmpAssigned = List.Where(x=>x.ProjectId == item.Id);
                    if(EmpAssigned.Any()){
                        foreach (var emp in EmpAssigned)
                        {
                            children.Add(new ProjectEmpTreeDTO {Name = emp.EmployeeName});
                        }
                    }
                    ch.Add(new ProjectEmpTreeDTO {Name = item.ProjectName,Children=children});
                }
                tree.Add(new ProjectEmpTreeDTO {Name = c.ClientName,Children=ch});
            }
            
            if (List.Count < 0)
            {
                return NotFound();
            }
            else
            {
                return tree;
            }
        }

        [HttpGet("GetProjectEmpTreeSummary")]
        public async Task<ActionResult<ProjectEmpTreeSummaryDTO>> GetProjectEmpTreeSummary()
        {
            ProjectEmpTreeSummaryDTO sum = new  ProjectEmpTreeSummaryDTO();
            var ClientList = await DBContext.Client.Select(x=>x).ToListAsync();
            sum.ActiveClientCount = ClientList.Where(x=>x.Status=="ACTIVE").Count();
            sum.InactiveClientCount = ClientList.Where(x=>x.Status=="INACTIVE").Count();
            sum.TotalClientCount = ClientList.Count();
           
            
            sum.ProjectEmpCount = DBContext.ProjectAssignment.GroupBy(
                p => p.Project.ProjectName, 
                p => p.EmployeeId,
                (key, g) => new ProjectEmpCount{ ProjectName = key, EmployeeCount = g.Count() }).ToList();
            
            var ProjectList = await DBContext.Project.Select(x=>x).ToListAsync();
            sum.NewProjectsCount = ProjectList.Where(x=>x.Status=="NEW").Count();
            sum.ApprovedProjectsCount = ProjectList.Where(x=>x.Status=="APPROVED").Count();
            sum.RejectedProjectsCount = ProjectList.Where(x=>x.Status=="REJECTED").Count();

            var EmployeeList = await DBContext.Employee.Select(x=>x).ToListAsync();
            sum.ActiveEmployeeCount = EmployeeList.Where(x=>x.Status=="ACTIVE").Count();
            sum.InactiveEmployeeCount = EmployeeList.Where(x=>x.Status=="INACTIVE").Count();

            sum.ClientEmpCount = DBContext.ProjectAssignment.GroupBy(
                p => p.Project.Client.ClientName, 
                p => p.EmployeeId,
                (key, g) => new ClientEmpCount{ ClientName = key, EmployeeCount = g.Count() }).ToList();

            sum.ClientProjectCount = DBContext.Project.GroupBy(
                p => p.Client.ClientName, 
                p => p.Id,
                (key, g) => new ClientProjectCount{ ClientName = key, ProjectCount = g.Count() }).ToList();

            return sum;
        }
}
}
