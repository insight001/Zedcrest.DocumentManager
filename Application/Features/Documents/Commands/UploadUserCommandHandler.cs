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
using Zedcrest.DocumentManager.Infrastructure.Providers.Interface;
using Microsoft.Extensions.Configuration;
using AutoMapper;
using MassTransit;
using Zedcrest.DocumentManager.Infrastructure.Providers.Services.HostedService;
using System.IO;
using Zedcrest.DocumentManager.Domain.Exceptions;
using Microsoft.AspNetCore.Http;

namespace Zedcrest.DocumentManager.Application.Features.Documents.Commands
{
    public class UploadUserCommandHandler : IRequestHandler<UploadUserRequestModel,APIResponse<UploadUserResponseModel>>
    {
        private readonly AppDbContext _context;
        private readonly IFileOperation _fileOperation;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly IPublishEndpoint _endpoint;


        public UploadUserCommandHandler(AppDbContext context, IFileOperation fileOperation, IConfiguration configuration, IMapper mapper, IPublishEndpoint endpoint)
        {
            _context = context;
            _configuration = configuration;
            _fileOperation = fileOperation;
            _mapper = mapper;
            _endpoint = endpoint;
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


            bool validator = Validator(request.Files);
         

            var response = await _fileOperation.UploadFiles(request.Files, _configuration);

            var documents = _mapper.Map<List<Document>>(response);

            documents?.ForEach(x =>
            x.UserId = user.UserId
            );

            _context.AddRange(documents);

            await _endpoint.Publish<ISendEmailMessage>(new SendEmailMessage()
            {
                DateCreated = DateTime.UtcNow,
                Email = new Domain.Models.DTO.EmailDTO
                {
                    Attachments = request.Files,
                    Name = $"{user.FirstName} {user.LastName}",
                    ReceiverEmail = user.Email
                }

            });
          
            _context.SaveChanges();

            return new APIResponse<UploadUserResponseModel>
            {
                Success =  true,
                Message = ResponseMessages.ItemCreatedSuccessfully,
                 Data =  new UploadUserResponseModel { Reference = user.Refrence}
            };
        }


        private bool Validator(List<IFormFile> files)
        {
            var extensions = new List<string>() { ".pdf", ".xls", ".xlsx", ".doc", ".docx", ".csv", ".png", ".jpg", ".jpeg", ".gif", ".txt" };

            foreach (var file in files)
            {
                if (!extensions.Contains(Path.GetExtension(file.FileName).ToLower()))
                    throw new RestException(System.Net.HttpStatusCode.BadRequest, $"{file.FileName} extension not recognized");

                if (file.Length > 2000000)
                    throw new RestException(System.Net.HttpStatusCode.BadRequest, $"{file.FileName} is greater than 2MB");
            }

            return true;
        }
    }
}
