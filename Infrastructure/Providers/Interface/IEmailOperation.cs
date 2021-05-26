using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Zedcrest.DocumentManager.Infrastructure.Providers.Interface
{
    public interface IEmailOperation
    {
        void SendEmailWithAttachments(string receiverEmail, string name, List<IFormFile> attacments);
    }
}
