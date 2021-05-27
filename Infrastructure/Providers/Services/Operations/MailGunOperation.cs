using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zedcrest.DocumentManager.Infrastructure.Providers.Interface;

namespace Zedcrest.DocumentManager.Infrastructure.Providers.Services.Operations
{
    public class MailGunOperation : IEmailOperation
    {
        public string CurrentName => throw new NotImplementedException();

        public void SendEmailWithAttachments(string receiverEmail, string name, List<IFormFile> attacments)
        {
            throw new NotImplementedException();
        }
    }
}
