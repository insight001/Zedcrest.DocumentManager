using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zedcrest.DocumentManager.Domain.Models.ResponseModels.QueryResponseModels;

namespace Zedcrest.DocumentManager.Domain.Models.RequestModels.CommandRequestModels
{
    public class GetUserByReferenceRequestModel : IRequest<GetUserByReferenceResponseModel>
    {
        public string Reference { get; set; }
    }
}
