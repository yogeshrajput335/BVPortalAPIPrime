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
    [Route("api/[controller]"), Authorize(Roles = "ADMIN")]
    public class AssetAllocationController : ControllerBase
    {
        private readonly BVContext DBContext;

        public AssetAllocationController(BVContext DBContext)
        {
            this.DBContext = DBContext;
        }
        
        [HttpGet("GetAssetAllocation")]
        public async Task<ActionResult<List<AssetAllocationDTO>>> Get()
        {
            var List = await DBContext.AssetAllocation.Select(
                s => new AssetAllocationDTO
                {
                    Id = s.Id,
                    AssetId = s.AssetId,
                    AllocatedById = s.AllocatedById,
                    AllocatedToId = s.AllocatedToId,
                    AssetName = s.Asset.Name,
                    AllocatedBy = s.AllocatedBy.FirstName+" "+s.AllocatedBy.LastName,
                    AllocatedTo = s.AllocatedTo.FirstName+" "+s.AllocatedTo.LastName,
                    AllocatedDate = s.AllocatedDate,
                    ReturnDate=s.ReturnDate,
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

        [HttpPost("InsertAssetAllocation")]
        public async Task < HttpStatusCode > InsertAssetAllocation(AssetAllocationDTO s) {
            var entity = new AssetAllocation() {
                    AssetId = s.AssetId,
                    AllocatedById = s.AllocatedById,
                    AllocatedToId = s.AllocatedToId,
                    AllocatedDate = s.AllocatedDate,
                    ReturnDate=s.ReturnDate,
                    Status = s.Status
            };
            DBContext.AssetAllocation.Add(entity);
            await DBContext.SaveChangesAsync();
            return HttpStatusCode.Created;
        }

        [HttpPut("UpdateAssetAllocation")]
        public async Task<HttpStatusCode> UpdateAssetAllocation(AssetAllocationDTO AssetAllocation) {
            var entity = await DBContext.AssetAllocation.FirstOrDefaultAsync(s => s.Id == AssetAllocation.Id);
            entity.AssetId  = AssetAllocation.AssetId ;
            entity.AllocatedById = AssetAllocation.AllocatedById;
            entity.AllocatedToId = AssetAllocation.AllocatedToId;
            entity.AllocatedDate = AssetAllocation.AllocatedDate;
            entity.ReturnDate = AssetAllocation.ReturnDate;
            entity.Status = AssetAllocation.Status;
            await DBContext.SaveChangesAsync();
            return HttpStatusCode.OK;
        }
        
        [HttpDelete("DeleteAssetAllocation/{Id}")]
        public async Task < HttpStatusCode > DeleteAssetAllocation(int Id) {
            var entity = new AssetAllocation() {
                Id = Id
            };
            DBContext.AssetAllocation.Attach(entity);
            DBContext.AssetAllocation.Remove(entity);
            await DBContext.SaveChangesAsync();
            return HttpStatusCode.OK;
        }
    }
}