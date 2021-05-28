using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zedcrest.DocumentManager.Domain.Models.DTO;

namespace Zedcrest.DocumentManager.Infrastructure.Providers.Interface
{
    public interface IFileOperation
    {
        Task<List<DocumentDTO>> UploadFiles(List<IFormFile> files, IConfiguration configuration);
    }
}
