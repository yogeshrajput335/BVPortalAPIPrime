using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using BVPortalApi.DTO;
using BVPortalApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BVPortalApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientTermHistoryController : ControllerBase
    {
        private readonly BVContext DBContext;

        public ClientTermHistoryController(BVContext DBContext)
        {
            this.DBContext = DBContext;
        }

        [HttpGet("GetClientClientTermHistory")]
        public async Task<ActionResult<List<ClientTermHistoryDTO>>> Get()
        {
            var List = await DBContext.ClientTermHistory.Select(
                s => new ClientTermHistoryDTO
                {
                    Id = s.Id,
                    ClientId = s.ClientId,
                    OldTermText = s.OldTermText,
                    OldTerm = s.OldTerm,
                    NewTermText = s.NewTermText,
                    NewTerm =s.NewTerm,
                    ReasonForChange = s.ReasonForChange,
                    ChangeDate = s.ChangeDate,
                    ChangeBy = s.ChangeBy
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

        [HttpPost("InsertClientTermHistory")]
        public async Task < HttpStatusCode > InsertClientTermHistory(ClientTermHistoryDTO s) {
            var entity = new ClientTermHistory() {
                    ClientId = s.ClientId,
                    OldTermText = s.OldTermText,
                    OldTerm = s.OldTerm,
                    NewTermText = s.NewTermText,
                    NewTerm =s.NewTerm,
                    ReasonForChange = s.ReasonForChange,
                    ChangeDate = s.ChangeDate,
                    ChangeBy = s.ChangeBy
            };
            DBContext.ClientTermHistory.Add(entity);
            await DBContext.SaveChangesAsync();
            return HttpStatusCode.Created;
        }

        [HttpPut("UpdateClientTermHistory")]
        public async Task<HttpStatusCode> UpdateClientTermHistory(ClientTermHistoryDTO ClientTermHistory) {
            var entity = await DBContext.ClientTermHistory.FirstOrDefaultAsync(s => s.Id == ClientTermHistory.Id);
            entity.ClientId = ClientTermHistory.ClientId;
            entity.OldTermText = ClientTermHistory.OldTermText;
            entity.OldTerm = ClientTermHistory.OldTerm;
            entity.NewTermText = ClientTermHistory.NewTermText;
            entity.NewTerm = ClientTermHistory.NewTerm;
            entity.ReasonForChange =ClientTermHistory.ReasonForChange;
            entity.ChangeDate = ClientTermHistory.ChangeDate;
            entity.ChangeBy = ClientTermHistory.ChangeBy;
            await DBContext.SaveChangesAsync();
            return HttpStatusCode.OK;
        }
        
        [HttpDelete("DeleteClientTermHistory/{Id}")]
        public async Task < HttpStatusCode > DeleteClientTermHistory(int Id) {
            var entity = new ClientTermHistory() {
                Id = Id
            };
            DBContext.ClientTermHistory.Attach(entity);
            DBContext.ClientTermHistory.Remove(entity);
            await DBContext.SaveChangesAsync();
            return HttpStatusCode.OK;
        }
    }
}