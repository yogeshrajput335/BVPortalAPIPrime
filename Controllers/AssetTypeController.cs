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
    public class AssetTypeController : ControllerBase
    {
        private readonly BVContext DBContext;

        public AssetTypeController(BVContext DBContext)
        {
            this.DBContext = DBContext;
        }

        [HttpGet("GetAssetType")]
        public async Task<ActionResult<List<AssetTypeDTO>>> Get()
        {
            var List = await DBContext.AssetType.Select(
                s => new AssetTypeDTO
                {
                    Id = s.Id,
                    Name = s.Name,
                    Description = s.Description,
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

        [HttpPost("InsertAssetType")]
        public async Task < HttpStatusCode > InsertAssetType(AssetTypeDTO s) {
            var entity = new AssetType() {
                Name = s.Name,
                Description = s.Description,
                Status = s.Status
            };
            DBContext.AssetType.Add(entity);
            await DBContext.SaveChangesAsync();
            return HttpStatusCode.Created;
        }

        [HttpPut("UpdateAssetType")]
        public async Task<HttpStatusCode> UpdateAssetType(AssetTypeDTO AssetType) {
            var entity = await DBContext.AssetType.FirstOrDefaultAsync(s => s.Id == AssetType.Id);
            entity.Name = AssetType.Name;
            entity.Description = AssetType.Description;
            entity.Status = AssetType.Status;
            await DBContext.SaveChangesAsync();
            return HttpStatusCode.OK;
        }
        
        [HttpDelete("DeleteAssetType/{Id}")]
        public async Task < HttpStatusCode > DeleteAssetType(int Id) {
            var entity = new AssetType() {
                Id = Id
            };
            DBContext.AssetType.Attach(entity);
            DBContext.AssetType.Remove(entity);
            await DBContext.SaveChangesAsync();
            return HttpStatusCode.OK;
        }
    }
}