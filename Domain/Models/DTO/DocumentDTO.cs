using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Zedcrest.DocumentManager.Domain.Models.DTO
{
    public class DocumentDTO
    {
        public string DocumentTitle { get; set; }
        public string DocumentURL { get; set; }
        public long FilSizeInByte { get; set; }
    }
}
