using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Zedcrest.DocumentManager.Domain.Models.ResponseModels
{
    public class HandlerResponse<T>
    {
        public bool Success { get; set; }
        public bool Message { get; set; }
        public T Data { get; set; }
    }
}
