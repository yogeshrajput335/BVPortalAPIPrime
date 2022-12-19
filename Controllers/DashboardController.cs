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
    public class DashboardController : ControllerBase
    {
        private readonly BVContext DBContext;

        public DashboardController(BVContext DBContext)
        {
            this.DBContext = DBContext;
        }

        [HttpGet("GetPieData")]
        public async Task<ActionResult<List<List<string>>>> GetPieData()
        {
            List<List<string>> myList = new List<List<string>>();
            myList.Add(new List<string> { "Task", "Hours per Day" });
            myList.Add(new List<string> { "Work", "11" });
            myList.Add(new List<string> { "Eat", "3" });
            return myList;
        }
    }
}