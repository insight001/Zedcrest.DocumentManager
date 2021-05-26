using Microsoft.AspNetCore.Http;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Zedcrest.DocumentManager.Infrastructure.Providers.Interface;

namespace Zedcrest.DocumentManager.Infrastructure.Providers.Services
{
    public class SendGridOperation : IEmailOperation
    {
        public void SendEmailWithAttachments(string receiverEmail, string name, List<IFormFile> attachments)
        {
           
            
            var client = new SendGridClient(Environment.GetEnvironmentVariable("SENDGRID_KEY"));

            List<Attachment> attachmentBox = new List<Attachment>();
            foreach(var item in attachments)
            {
                var attachment = new Attachment
                {
                    Content = ConvertAttachmentToBase64(item),
                    ContentId = Guid.NewGuid().ToString(),
                    Disposition = "inline",
                    Filename = item.FileName,
                };

                attachmentBox.Add(attachment);
            }


            var from = new EmailAddress("noreply@zedcrest.com");
            var subject = "Your Attachments are ready";
            var to = new EmailAddress(receiverEmail);
            var plainText = "";
            var htmlContent = MessageGenerator(name);
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainText, htmlContent);
            msg.AddAttachments(attachmentBox);

            client.SendEmailAsync(msg);
        }





        private string ConvertAttachmentToBase64(IFormFile file)
        {
            string filestring;
            
                
                    using (var ms = new MemoryStream())
                    {
                        file.CopyTo(ms);

                        var filesBytes = ms.ToArray();

                        filestring = Convert.ToBase64String(filesBytes);
                        
                    }
                
            
           
            return filestring;
        }

        private string MessageGenerator(string name)
        {
            return $@" Hello {name}, <p><br/>
                    We kept a copy of the files you uploaded,  We've attached some to this email
                ";
        }
    }
}
