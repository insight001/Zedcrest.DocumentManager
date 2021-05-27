using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zedcrest.DocumentManager.Domain.Models.DTO;

namespace Zedcrest.DocumentManager.Infrastructure.Providers.Services.HostedService
{
    public class SendEmailMessage : ISendEmailMessage
    {
        public Guid MessageId { get; set; }
        public EmailDTO Email { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
