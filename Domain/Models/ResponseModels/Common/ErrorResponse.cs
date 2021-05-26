using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Zedcrest.DocumentManager.Domain.Models.ResponseModels
{
    public class ErrorResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
    }
}
