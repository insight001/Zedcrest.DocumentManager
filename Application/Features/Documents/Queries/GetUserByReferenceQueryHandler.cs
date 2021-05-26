using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Zedcrest.DocumentManager.Domain.Constants;
using Zedcrest.DocumentManager.Domain.Exceptions;
using Zedcrest.DocumentManager.Domain.Models.DTO;
using Zedcrest.DocumentManager.Domain.Models.RequestModels.CommandRequestModels;
using Zedcrest.DocumentManager.Domain.Models.RequestModels.QueryRequestModels;
using Zedcrest.DocumentManager.Domain.Models.ResponseModels;
using Zedcrest.DocumentManager.Domain.Models.ResponseModels.QueryResponseModels;
using Zedcrest.DocumentManager.Infrastructure.Persistence;

namespace Zedcrest.DocumentManager.Application.Features.Documents.Queries
{
    public class GetUserByReferenceQueryHandler : IRequestHandler<GetUserByReferenceRequestModel, APIResponse<GetUserByReferenceResponseModel>>
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public GetUserByReferenceQueryHandler(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<APIResponse<GetUserByReferenceResponseModel>> Handle(GetUserByReferenceRequestModel request, CancellationToken cancellationToken)
        {
            var user = _context.Users.Where(x => x.Refrence == request.Reference).Include(x=>x.Documents).FirstOrDefault();

            if (user == null)
                throw new RestException(System.Net.HttpStatusCode.NotFound, ResponseMessages.ReferenceNotFound);


            var response = new GetUserByReferenceResponseModel
            {
                Documents = _mapper.Map<List<DocumentDTO>>(user.Documents),
                Profile = _mapper.Map<UserDTO>(user)
            };

            return new APIResponse<GetUserByReferenceResponseModel>
            {
                Data = response,
                Message = ResponseMessages.ItemRetrieved,
                Success = true
            };
            
        }
    }
}
