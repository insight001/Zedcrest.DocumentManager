using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Zedcrest.DocumentManager.Infrastructure.Providers.Services.HostedService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IBusControl _busControl;
        private readonly IServiceProvider _serviceProvider;
        private readonly IConfiguration _configuration;

        public Worker(ILogger<Worker> logger, IBusControl busControl, IServiceProvider serviceProvider, IConfiguration configuration)
        {
            _logger = logger;
            _busControl = busControl;
            _serviceProvider = serviceProvider;
            _configuration = configuration;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var SendEmailHandler = _busControl.ConnectReceiveEndpoint(_configuration["Q.QueueName"],x =>
           {
               x.Consumer<SendEmailConsumer>(_serviceProvider);
           });

            await SendEmailHandler.Ready;
        }
    }
}
