using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Zedcrest.DocumentManager.Infrastructure.Utilities;
using Zedcrest.DocumentManager.Domain.Models.RequestModels.CommandRequestModels;
using Zedcrest.DocumentManager.Domain.Models.ResponseModels.CommandResponseModels;
using Zedcrest.DocumentManager.Infrastructure.Persistence;
using Zedcrest.DocumentManager.Domain.Entities;
using Zedcrest.DocumentManager.Domain.Models.ResponseModels;
using Zedcrest.DocumentManager.Domain.Constants;

namespace Zedcrest.DocumentManager.Application.Features.Documents.Commands
{
    public class UploadUserCommandHandler : IRequestHandler<UploadUserRequestModel,APIResponse<UploadUserResponseModel>>
    {
        private readonly AppDbContext _context;
        public UploadUserCommandHandler(AppDbContext context)
        {
            _context = context;
        }
        public async Task<APIResponse<UploadUserResponseModel>> Handle(UploadUserRequestModel request, CancellationToken cancellationToken)
        {
            string reference = ReferenceGenerator.Generate();

            var user = new User
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                UserId = Guid.NewGuid(),
                Refrence = reference,
                Email =  request.Email
            };

            _context.Users.Add(user);

            //Call upload file service ; returns file meta data



            _context.SaveChanges();

            return new APIResponse<UploadUserResponseModel>
            {
                Success =  true,
                Message = ResponseMessages.ItemCreatedSuccessfully,
                 Data =  new UploadUserResponseModel { Reference = user.Refrence}
            };
        }
    }
}
