using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using BVPortalApi.DTO;
using BVPortalApi.Models;
using BVPortalAPIPrime.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BVPortalApi.Controllers
{
    [ApiController]
    [Route("api/[controller]"), Authorize(Roles = "ADMIN")]
    public class ServiceController : ControllerBase
    {
        
        private readonly BVContext DBContext;

        public ServiceController(BVContext DBContext)
        {
            this.DBContext = DBContext;
        }

        [HttpGet("GetService")]
        public async Task<ActionResult<List<ServiceDTO>>> Get()
        {
            var List = await DBContext.Service.Select(
                s => new ServiceDTO
                {
                    Id = s.Id,
                    ServiceName = s.ServiceName,
                    Unit = s.Unit,
                    Rate = s.Rate,
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

        [HttpPost("InsertService")]
        public async Task < HttpStatusCode > InsertService(ServiceDTO s) {
            var entity = new Service() {
                 ServiceName = s.ServiceName,
                    Unit = s.Unit,
                    Rate = s.Rate,
                    Status = s.Status
            };
            DBContext.Service.Add(entity);
            await DBContext.SaveChangesAsync();
            return HttpStatusCode.Created;
        }

        [HttpPut("UpdateService")]
        public async Task<HttpStatusCode> UpdateService(ServiceDTO Service) {
            var entity = await DBContext.Service.FirstOrDefaultAsync(s => s.Id == Service.Id);
            entity.ServiceName = Service.ServiceName;
            entity.Unit = Service.Unit;
            entity.Rate = Service.Rate;
            entity.Status = Service.Status;
            await DBContext.SaveChangesAsync();
            return HttpStatusCode.OK;
        }
        
        [HttpDelete("DeleteService/{Id}")]
        public async Task < HttpStatusCode > DeleteService(int Id) {
            var entity = new Service() {
                Id = Id
            };
            DBContext.Service.Attach(entity);
            DBContext.Service.Remove(entity);
            await DBContext.SaveChangesAsync();
            return HttpStatusCode.OK;
        }
         [HttpPost("DeleteServices")]
        public  async Task<HttpStatusCode> DeleteServices(List<ServiceDTO> services) {
            List<Service> entities = services.Select(i => new Service(){
                Id = i.Id
            }).ToList();
            DBContext.Service.AttachRange(entities);
            DBContext.Service.RemoveRange(entities);
            await DBContext.SaveChangesAsync();
            // _cache.Remove(cacheKey);
            return HttpStatusCode.OK;
        }
    }
}