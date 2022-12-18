using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BVPortalApi.Models;
using BVPortalAPIPrime.Models;

namespace BVPortalAPIPrime.Services
{
    public interface IMailService
    {
        Task SendEmailAsync(MailRequest mailRequest);
        Task SendWelcomeEmailAsync(WelcomeRequest request);
    }
}