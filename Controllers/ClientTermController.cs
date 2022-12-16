using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using BVPortalApi.CommonFeatures;
using BVPortalApi.CommonFeatures.Contracts;
using BVPortalApi.DTO;
using BVPortalApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BVPortalApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientTermController : ControllerBase
    {
        private readonly BVContext DBContext;

        public ClientTermController(BVContext DBContext)
        {
            this.DBContext = DBContext;
        }

         [HttpGet("GetClientTerm")]
        public async Task<ActionResult<List<ClientTermDTO>>> Get()
        {
            var List = await DBContext.ClientTerm.Select(
                s => new ClientTermDTO
                {
                    Id = s.Id,
                    ClientId = s.ClientId,
                    TermText = s.TermText,
                    Term = s.Term,
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

        [HttpPost("InsertClientTerm")]
        public async Task < HttpStatusCode > InsertClientTerm(ClientTermDTO s) {
            var entity = new ClientTerm() {
                    ClientId = s.ClientId,
                    TermText = s.TermText,
                    Term = s.Term,
            };
            DBContext.ClientTerm.Add(entity);
            await DBContext.SaveChangesAsync();
            return HttpStatusCode.Created;
        }

        [HttpPut("UpdateClientTerm")]
        public async Task<HttpStatusCode> UpdateClientTerm(ClientTermDTO ClientTerm) {
            var entity = await DBContext.ClientTerm.FirstOrDefaultAsync(s => s.Id == ClientTerm.Id);
            entity.ClientId= ClientTerm.ClientId;
            entity.TermText = ClientTerm.TermText;
            entity.Term = ClientTerm.Term;
            await DBContext.SaveChangesAsync();
            return HttpStatusCode.OK;
        }
        
        [HttpDelete("DeleteClientTerm/{Id}")]
        public async Task < HttpStatusCode > DeleteClientTerm(int Id) {
            var entity = new ClientTerm() {
                Id = Id
            };
            DBContext.ClientTerm.Attach(entity);
            DBContext.ClientTerm.Remove(entity);
            await DBContext.SaveChangesAsync();
            return HttpStatusCode.OK;
        }
    }
}