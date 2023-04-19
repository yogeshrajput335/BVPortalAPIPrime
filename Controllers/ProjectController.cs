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
    public class ProjectController : ControllerBase
    {
        private const string cacheKey = "projectList";
        private readonly BVContext DBContext;
        private IMemoryCache _cache;
        private ILogger<ProjectController> _logger;
        private readonly IMapper _mapper;

        public ProjectController(BVContext DBContext, IMemoryCache cache, ILogger<ProjectController> logger, IMapper mapper)
        {  this.DBContext = DBContext;
            _cache = cache ?? throw new ArgumentNullException(nameof(cache));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper;
        }

        [HttpGet("GetProjects")]
        public async Task<ActionResult<List<ProjectDTO>>> Get()
        {
             _logger.Log(LogLevel.Information, "Trying to fetch the list of Projects from cache.");
            if (_cache.TryGetValue(cacheKey, out List<ProjectDTO> List))
            {
                _logger.Log(LogLevel.Information, "Project list found in cache.");
                
            }
            else
            {
                _logger.Log(LogLevel.Information, "Project list not found in cache. Fetching from database.");
                List = _mapper.Map<List<ProjectDTO>>(await DBContext.Project.Include(x=>x.Client).ToListAsync());
                
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

        [HttpPost("InsertProject")]
        public async Task < HttpStatusCode > InsertProject(ProjectDTO s) {
            var entity = _mapper.Map<Project>(s);
            DBContext.Project.Add(entity);
            await DBContext.SaveChangesAsync();
            _cache.Remove(cacheKey);
            return HttpStatusCode.Created;
        }

        [HttpPut("UpdateProject")]
        public async Task<HttpStatusCode> UpdateProject(ProjectDTO Project) {
            var entity = await DBContext.Project.FirstOrDefaultAsync(s => s.Id == Project.Id);
            entity.ProjectName = Project.ProjectName;
            entity.ClientId = Project.ClientId;
            entity.Description = Project.Description;
            entity.StartDate = Project.StartDate;
            entity.EndDate = Project.EndDate;
            entity.ProjectType = Project.ProjectType;
            entity.Status = Project.Status;
            await DBContext.SaveChangesAsync();
            _cache.Remove(cacheKey);
            return HttpStatusCode.OK;
        }
        
        [HttpDelete("DeleteProject/{Id}")]
        public async Task < HttpStatusCode > DeleteProject(int Id) {
            var entity = new Project() {
                Id = Id
            };
            DBContext.Project.Attach(entity);
            DBContext.Project.Remove(entity);
            await DBContext.SaveChangesAsync();
            _cache.Remove(cacheKey);
            return HttpStatusCode.OK;
        }
         [HttpGet("GetProjectCount")]
        public ActionResult<int> GetProjectCount()
        {
            return  DBContext.Project.Where(x=>x.Status.ToLower() == "active").Count();
        }
    }
}