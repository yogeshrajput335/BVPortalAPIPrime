using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BVPortalApi.Models;
using BVPortalAPIPrime.Models;
using BVPortalAPIPrime.Services;
using Microsoft.AspNetCore.Mvc;

namespace BVPortalAPIPrime.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MailController : ControllerBase
    {
        private readonly IMailService mailService;
        public MailController(IMailService mailService)
        {
            this.mailService = mailService;
        }
        [HttpPost("send")]
        public async Task<IActionResult> SendMail([FromForm]MailRequest request)
        {
            try
            {
                await mailService.SendEmailAsync(request);
                return Ok();
            }
            catch (Exception ex)
            {
                throw;
            }
                
        }
        [HttpPost("welcome")]
        public async Task<IActionResult> SendWelcomeMail([FromForm]WelcomeRequest request)
        {
            try
            {
                await mailService.SendWelcomeEmailAsync(request);
                return Ok();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        
    }
}