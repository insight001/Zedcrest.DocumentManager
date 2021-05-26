using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zedcrest.DocumentManager.Domain.Models.ResponseModels;
using Zedcrest.DocumentManager.Domain.Models.ResponseModels.QueryResponseModels;

namespace Zedcrest.DocumentManager.Domain.Models.RequestModels.QueryRequestModels
{
    public class GetUserByReferenceRequestModel : IRequest<APIResponse<GetUserByReferenceResponseModel>>
    {
        public string Reference { get; set; }
    }
}
