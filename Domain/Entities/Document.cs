using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Zedcrest.DocumentManager.Domain.Entities
{
    public class Document
    {
        public Guid DocumentId { get; set; }
        public Guid UserId { get; set; }
        public string DocumentTitle { get; set; }
        public string DocumentURL { get; set; }
        public long FilSizeInByte { get; set; }

    }
}
