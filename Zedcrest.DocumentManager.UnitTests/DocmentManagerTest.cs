using AutoMapper;
using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using MockQueryable.Moq;
using Moq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Zedcrest.DocumentManager.Application.Features.Documents.Commands;
using Zedcrest.DocumentManager.Application.Features.Documents.Queries;
using Zedcrest.DocumentManager.Domain.Constants;
using Zedcrest.DocumentManager.Domain.Entities;
using Zedcrest.DocumentManager.Domain.Exceptions;
using Zedcrest.DocumentManager.Domain.Models.RequestModels.CommandRequestModels;
using Zedcrest.DocumentManager.Domain.Models.RequestModels.QueryRequestModels;
using Zedcrest.DocumentManager.Infrastructure.Persistence;
using Zedcrest.DocumentManager.Infrastructure.Providers.Interface;

namespace Zedcrest.DocumentManager.Test
{
    public class DocumentManagerTests
    {

        private readonly Mock<AppDbContext> _context;
        private readonly Mock<IFileOperation> _fileOperation;
        private Mock<IConfiguration> _configuration;
        private readonly Mock<IMapper> _mapper;
        private readonly Mock<IPublishEndpoint> _endpoint;
        public DocumentManagerTests()
        {

            _context = new Mock<AppDbContext>();
            _fileOperation = new Mock<IFileOperation>();
            _configuration = new Mock<IConfiguration>();
            _mapper = new Mock<IMapper>();
            _endpoint = new Mock<IPublishEndpoint>();

        }

        [Fact]
        public async Task Post_Document_Should_Throws_Exception_When_File_With_Size_More_Than_2MB_Is_Included_In_File_Upload_List()
        {
            //Arrange
            var request = new UploadUserRequestModel()
            {
                Email = "taiwo.alabi@gmail.com",
                FirstName = "Taiwo",
                LastName = "Alabi"

            };
            var fileMock = new Mock<IFormFile>();
            var physicalFile = new FileInfo("a.pdf");
            var ms = new MemoryStream();
            var writer = new StreamWriter(ms);
            writer.Write(physicalFile.OpenRead());
            writer.Flush();
            ms.Position = 0;
            var fileName = physicalFile.Name;
            //Setup mock file using info from physical file
            fileMock.Setup(_ => _.FileName).Returns(fileName);
            fileMock.Setup(_ => _.Length).Returns(physicalFile.Length);
            fileMock.Setup(_ => _.OpenReadStream()).Returns(ms);
            fileMock.Setup(_ => _.ContentDisposition).Returns(string.Format("inline; filename={0}", fileName));
            List<IFormFile> uploadFileList = new List<IFormFile>();
            uploadFileList.Add(fileMock.Object);
            request.Files = uploadFileList;

            List<User> users = new List<User>();

            var mockUsers = users.AsQueryable().BuildMockDbSet();
            // var mockCustomerCards = customerCards.AsQueryable().BuildMockDbSet();
            //var mockCardTokenizationLogs = tokenizationLogs.AsQueryable().BuildMockDbSet();

            _context.Setup(c => c.Users).Returns(mockUsers.Object);

            //Act
            UploadUserCommandHandler commandHandler = new UploadUserCommandHandler(_context.Object, _fileOperation.Object, _configuration.Object, _mapper.Object, _endpoint.Object);


            //Assert
            RestException exception = await Assert.ThrowsAsync<RestException>(async () => await commandHandler.Handle(request, new CancellationToken()));
            Assert.Contains("is greater than 2MB", exception.Message);

        }

        [Fact]
        public async Task Post_Document_Add_Document_To_DB_When_All_Validation_Passess()
        {
            //Arrange
            var request = new UploadUserRequestModel()
            {
                Email = "taiwo.alabi@gmail.com",
                FirstName = "Taiwo",
                LastName = "Alabi"

            };
            var fileMock = new Mock<IFormFile>();
            var physicalFile = new FileInfo("b.JPEG");
            var ms = new MemoryStream();
            var writer = new StreamWriter(ms);
            writer.Write(physicalFile.OpenRead());
            writer.Flush();
            ms.Position = 0;
            var fileName = physicalFile.Name;
            //Setup mock file using info from physical file
            fileMock.Setup(_ => _.FileName).Returns(fileName);
            fileMock.Setup(_ => _.Length).Returns(physicalFile.Length);
            fileMock.Setup(_ => _.OpenReadStream()).Returns(ms);
            fileMock.Setup(_ => _.ContentDisposition).Returns(string.Format("inline; filename={0}", fileName));
            List<IFormFile> uploadFileList = new List<IFormFile>();
            uploadFileList.Add(fileMock.Object);
            request.Files = uploadFileList;

            List<User> users = new List<User>();

            var mockUsers = users.AsQueryable().BuildMockDbSet();

            _context.Setup(c => c.Users).Returns(mockUsers.Object);

            //Act
            UploadUserCommandHandler commandHandler = new UploadUserCommandHandler(_context.Object, _fileOperation.Object, _configuration.Object, _mapper.Object, _endpoint.Object);
            var response = await commandHandler.Handle(request, new CancellationToken());

            //Assert
            Assert.True(response.Success);

        }

        [Fact]
        public async Task Get_User_By_Reference_Returns_Data()
        {
            //Arrange
            var request = new GetUserByReferenceRequestModel()
            {
                Reference = "09ADSFRTUYIOPPF789"

            };

            List<User> users = new List<User>() {
                new User
                {
                     Email = "taiwo.alabi@gmail.com",
                     FirstName = "Taiwo",
                     LastName = "Alabi",
                     Refrence = "09ADSFRTUYIOPPF789",
                     UserId = Guid.NewGuid()
                },
                 new User
                {
                     Email = "taiwo.insight@gmail.com",
                     FirstName = "Taiwo",
                     LastName ="Insight",
                     Refrence = "0xDFR345RTMKGJGPDSJD467MDM",
                     UserId = Guid.NewGuid()
                }

            };

            var mockUsers = users.AsQueryable().BuildMockDbSet();

            _context.Setup(c => c.Users).Returns(mockUsers.Object);

            //Act
            GetUserByReferenceQueryHandler queryHandler = new GetUserByReferenceQueryHandler(_context.Object, _mapper.Object);
            var response = await queryHandler.Handle(request, new CancellationToken());

            //Assert
            Assert.Equal(ResponseMessages.ItemRetrieved, response.Message);

        }

    }
}
