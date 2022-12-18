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
    public class AssetController : ControllerBase
    {
        
        private readonly BVContext DBContext;

        public AssetController(BVContext DBContext)
        {
            this.DBContext = DBContext;
        }

        [HttpGet("GetAsset")]
        public async Task<ActionResult<List<AssetDTO>>> Get()
        {
            var List = await DBContext.Assets.Select(
                s => new AssetDTO
                {
                    Id = s.Id,
                    Name = s.Name,
                    TypeId = s.TypeId,
                    Type = s.AssetType.Name,
                    ModelNumber = s.ModelNumber,
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

        [HttpPost("InsertAsset")]
        public async Task < HttpStatusCode > InsertAsset(AssetDTO s) {
            var entity = new Asset() {
                Name = s.Name,
                TypeId = s.TypeId,
                ModelNumber = s.ModelNumber,
                Status = s.Status
            };
            DBContext.Assets.Add(entity);
            await DBContext.SaveChangesAsync();
            return HttpStatusCode.Created;
        }

        [HttpPut("UpdateAsset")]
        public async Task<HttpStatusCode> UpdateAsset(AssetDTO Asset) {
            var entity = await DBContext.Assets.FirstOrDefaultAsync(s => s.Id == Asset.Id);
            entity.Name = Asset.Name;
            entity.TypeId = Asset.TypeId;
            entity.ModelNumber = Asset.ModelNumber;
            entity.Status = Asset.Status;
            await DBContext.SaveChangesAsync();
            return HttpStatusCode.OK;
        }
        
        [HttpDelete("DeleteAsset/{Id}")]
        public async Task < HttpStatusCode > DeleteAsset(int Id) {
            var entity = new Asset() {
                Id = Id
            };
            DBContext.Assets.Attach(entity);
            DBContext.Assets.Remove(entity);
            await DBContext.SaveChangesAsync();
            return HttpStatusCode.OK;
        }
        [HttpPost("DeleteAssets")]
        public  async Task<HttpStatusCode> DeleteAsset(List<AssetDTO> Assets) {
            List<Asset> entities = Assets.Select(i => new Asset(){
                Id = i.Id
            }).ToList();
            DBContext.Assets.AttachRange(entities);
            DBContext.Assets.RemoveRange(entities);
            await DBContext.SaveChangesAsync();
            return HttpStatusCode.OK;
        }
    }
}