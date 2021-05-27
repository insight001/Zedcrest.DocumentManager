using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Zedcrest.DocumentManager.Infrastructure.Providers.Services.HostedService
{
    public class AppSettings
    {
        public QueueSettings QueueSettings { get; set; }
    }

    public class QueueSettings
    {
        public string VirtualHost { get; set; }
        public string HostName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string QueueName { get; set; }

    }
}
