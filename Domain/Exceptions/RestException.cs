using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Zedcrest.DocumentManager.Domain.Exceptions
{
    public class RestException : Exception
    {
        public string ErrorMessage { get; set; }
        public HttpStatusCode Code { get; }
        public object Errors { get; }

        public RestException(HttpStatusCode code, string message, object errors = null) : base(message)
        {
            Code = code;
            Errors = errors;
        }
    }
}
