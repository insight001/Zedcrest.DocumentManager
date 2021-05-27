using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Zedcrest.DocumentManager.Infrastructure.Providers.Interface;
using Microsoft.Extensions.Configuration;

namespace Zedcrest.DocumentManager.Infrastructure.Providers.Services.HostedService
{
    public class SendEmailConsumer : IConsumer<ISendEmailMessage>
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IConfiguration _configuration;
        
        public SendEmailConsumer(IServiceProvider serviceProvider, IConfiguration configuration)
        {
            _serviceProvider = serviceProvider;
            _configuration = configuration;
        }


        public async Task Consume(ConsumeContext<ISendEmailMessage> context)
        {
            var emailService = _serviceProvider.GetService<IEnumerable<IEmailOperation>>();

            emailService.FirstOrDefault(x=>x.CurrentName == _configuration["Current_Email_Provider"])?.SendEmailWithAttachments(context.Message.Email.ReceiverEmail, context.Message.Email.Name, context.Message.Email.Attachments);

            await context.RespondAsync<EmailSent>(new
            {
                Value = $"Received: {context.Message.MessageId}"
            });
        }
    }
}
