using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Zedcrest.DocumentManager.Domain.Constants
{
    public class ResponseMessages
    {
        public const string ReferenceNotFound = "Profile with the refrence supplied not found";
        public const string ItemRetrieved = "Items retrieved successfully";
        public const string InternalError = "An internal error occurred with the API";
        public const string ItemCreatedSuccessfully = "Item created successfully";
    }
}
