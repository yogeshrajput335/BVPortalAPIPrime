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
    public class ClientController : ControllerBase
    {
        private readonly BVContext DBContext;

        public ClientController(BVContext DBContext)
        {
            this.DBContext = DBContext;
        }

        [HttpGet("GetClient")]
        public async Task<ActionResult<List<ClientDTO>>> Get()
        {
            var List =
                await (from c in DBContext.Client
                       join t in DBContext.ClientTerm on c.Id equals t.ClientId into inner
                       from tt in inner.DefaultIfEmpty()
                       select new ClientDTO
                       {
                           Id = c.Id,
                           ClientName = c.ClientName,
                           ContactPerson = c.ContactPerson,
                           Email = c.Email,
                           PhoneNumber = c.PhoneNumber,
                           Address = c.Address,
                           Status = c.Status,
                           Term = tt.Term,
                           TermText = tt.TermText ?? string.Empty
                       }).ToListAsync();
            if (List.Count < 0)
            {
                return NotFound();
            }
            else
            {
                return List;
            }
        }

        [HttpPost("InsertClient")]
        public async Task<HttpStatusCode> InsertClient(ClientDTO s)
        {
            var entity = new Client()
            {
                ClientName = s.ClientName,
                ContactPerson = s.ContactPerson,
                Email = s.Email,
                PhoneNumber = s.PhoneNumber,
                Address = s.Address,
                Status = s.Status
            };
            DBContext.Client.Add(entity);
            await DBContext.SaveChangesAsync();
            return HttpStatusCode.Created;
        }

        [HttpPut("UpdateClient")]
        public async Task<HttpStatusCode> UpdateClient(ClientDTO Client)
        {
            var entity = await DBContext.Client.FirstOrDefaultAsync(s => s.Id == Client.Id);
            entity.ClientName = Client.ClientName;
            entity.ContactPerson = Client.ContactPerson;
            entity.Email = Client.Email;
            entity.PhoneNumber = Client.PhoneNumber;
            entity.Address = Client.Address;
            entity.Status = Client.Status;
            await DBContext.SaveChangesAsync();
            return HttpStatusCode.OK;
        }

        [HttpDelete("DeleteClient/{Id}")]
        public async Task<HttpStatusCode> DeleteClient(int Id)
        {
            var entity = new Client()
            {
                Id = Id
            };
            DBContext.Client.Attach(entity);
            DBContext.Client.Remove(entity);
            await DBContext.SaveChangesAsync();
            return HttpStatusCode.OK;
        }
        
        [HttpPost("SetTerm/{Id}/{Term}")]
        public async Task<HttpStatusCode> SetTerm(int Id, int Term,[FromBody] SetTermDTO setTerm)
        {
            int oldTerm = 0;
            var entity = await DBContext.ClientTerm.FirstOrDefaultAsync(s => s.ClientId == Id);
            if (entity == null)
            {
                ClientTerm ct = new ClientTerm();
                ct.ClientId = Id;
                ct.TermText = Term + "d";
                ct.Term = Term;
                DBContext.ClientTerm.Add(ct);
            }
            else if (entity != null && entity.Term != Term)
            {
                oldTerm = entity.Term;
                entity.TermText = Term + "d";
                entity.Term = Term;
            }
            var emp = await DBContext.Employee.FirstOrDefaultAsync(s => s.Id == setTerm.ChangeBy);
            ClientTermHistory cth = new ClientTermHistory();
            cth.ClientId = Id;
            cth.OldTermText = oldTerm + "d";
            cth.OldTerm = oldTerm;
            cth.NewTermText = Term + "d";
            cth.NewTerm = Term;
            cth.ReasonForChange = setTerm.ReasonForChange; 
            cth.ChangeDate = DateTime.Now;
            cth.ChangeBy = emp.FirstName+" "+emp.LastName; 
            DBContext.ClientTermHistory.Add(cth);
            await DBContext.SaveChangesAsync();
            return HttpStatusCode.OK;
        }

        [HttpGet("GetClientTermHistory/{id}")]
        public async Task<ActionResult<List<ClientTermHistory>>> GetClientTermHistory(int id)
        {
            var List = await DBContext.ClientTermHistory.Where(x => x.ClientId == id).ToListAsync();

            if (List.Count < 0)
            {
                return NotFound();
            }
            else
            {
                return List;
            }
        }
    }
}