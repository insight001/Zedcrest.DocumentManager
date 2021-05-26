using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zedcrest.DocumentManager.Domain.Models.ResponseModels.CommandResponseModels;

namespace Zedcrest.DocumentManager.Domain.Models.RequestModels.CommandRequestModels
{
    public class UploadUserRequestModel : IRequest<UploadUserResponseModel>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public List<IFormFile> Files { get; set; }
    }
}
