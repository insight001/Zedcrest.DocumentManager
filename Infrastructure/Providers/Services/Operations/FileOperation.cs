using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Zedcrest.DocumentManager.Domain.Exceptions;
using Zedcrest.DocumentManager.Domain.Models.DTO;
using Zedcrest.DocumentManager.Infrastructure.Providers.Interface;

namespace Zedcrest.DocumentManager.Infrastructure.Providers.Services
{
    public class FileOperation : IFileOperation
    {

        public async Task<List<DocumentDTO>> UploadFiles(List<IFormFile> files, IConfiguration _configuration)
        {

           

            List<DocumentDTO> documents = new List<DocumentDTO>();

            var bag = new ConcurrentBag<DocumentDTO>();

           var tasks = files.Select(async file =>
            {
                var blobClient = new Azure.Storage.Blobs.BlobClient(
                          connectionString: _configuration["AZURE_STORAGE_CONNECTION"],
                          blobContainerName: _configuration["AZURE_CONTAINER_NAME"],
                          blobName: $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}");

                var contentFileName = Path.GetFileNameWithoutExtension(file.FileName);
                var document = new DocumentDTO
                {
                    DocumentTitle = contentFileName,
                    DocumentURL = blobClient.Uri.AbsoluteUri,
                    FilSizeInByte = file.Length
                };

                bag.Add(document);

                await blobClient.UploadAsync(file.OpenReadStream());
            });

            await Task.WhenAll(tasks);

            return bag.ToList();
        }



    }
}
