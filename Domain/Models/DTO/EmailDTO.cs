using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Zedcrest.DocumentManager.Domain.Models.DTO
{
    public class EmailDTO
    {
        public string Name { get; set; }
        public string ReceiverEmail { get; set; }
        public List<IFormFile> Attachments { get; set; }
    }
}
