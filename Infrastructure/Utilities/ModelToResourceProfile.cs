using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zedcrest.DocumentManager.Domain.Entities;
using Zedcrest.DocumentManager.Domain.Models.DTO;

namespace Zedcrest.DocumentManager.Infrastructure.Utilities
{
    public class ModelToResourceProfile : Profile
    {
        public ModelToResourceProfile()
        {
            CreateMap<Document, DocumentDTO>().AfterMap((src, dest) =>
            {
                dest.DocumentTitle = src.DocumentTitle;
                dest.DocumentURL = src.DocumentURL;
                dest.FilSizeInByte = src.FilSizeInByte;
            });

            CreateMap<DocumentDTO, Document>().AfterMap((src, dest) =>
            {
                dest.DocumentId = Guid.NewGuid();
            });

            CreateMap<User, UserDTO>().AfterMap((src, dest) =>
            {
                dest.FirstName = src.FirstName;
                dest.LastName = src.LastName;
                dest.Email = src.Email;
                dest.Refrence = src.Refrence;
            });
        }
    }
}
