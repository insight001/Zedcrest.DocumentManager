using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zedcrest.DocumentManager.Domain.Models.DTO;

namespace Zedcrest.DocumentManager.Infrastructure.Providers.Services.HostedService
{
    public interface ISendEmailMessage
    {
        Guid MessageId { get; set; }
        EmailDTO Email { get; set; }
        DateTime DateCreated { get; set; }
    }
}
