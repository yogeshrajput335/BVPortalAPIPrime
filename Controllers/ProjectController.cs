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
    public class ProjectController : ControllerBase
    {
        private readonly BVContext DBContext;

        public ProjectController(BVContext DBContext)
        {
            this.DBContext = DBContext;
        }

        [HttpGet("GetProjects")]
        public async Task<ActionResult<List<ProjectDTO>>> Get()
        {
            var List = await DBContext.Project.Select(
                s => new ProjectDTO
                {
                    Id = s.Id,
                    ProjectName = s.ProjectName,
                    ClientId = s.ClientId,
                    ClientName = s.Client.ClientName,
                    Description = s.Description,
                    StartDate =s.StartDate,
                    EndDate = s.EndDate,
                    ProjectType = s.ProjectType,
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

        [HttpPost("InsertProject")]
        public async Task < HttpStatusCode > InsertProject(ProjectDTO s) {
            var entity = new Project() {
                     Id = s.Id,
                    ProjectName = s.ProjectName,
                    ClientId = s.ClientId,
                    Description = s.Description,
                    StartDate =s.StartDate,
                    EndDate = s.EndDate,
                    ProjectType = s.ProjectType,
                    Status = s.Status
            };
            DBContext.Project.Add(entity);
            await DBContext.SaveChangesAsync();
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
            return HttpStatusCode.OK;
        }
    }
}