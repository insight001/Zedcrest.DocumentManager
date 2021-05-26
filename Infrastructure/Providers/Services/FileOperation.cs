using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
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

            ValidateSize(files);

            List<DocumentDTO> documents = new List<DocumentDTO>();

            var blobClient = new Azure.Storage.Blobs.BlobClient(
                            connectionString: _configuration["AZURE_STORAGE_CONNECTION"],
                            blobContainerName: _configuration["AZURE_CONTAINER_NAME"],
                            blobName: "documents");

            
            Parallel.ForEach(files, file =>
            {
               
                blobClient.UploadAsync(file.OpenReadStream());
                var contentFileName = Path.GetFileNameWithoutExtension(file.FileName);
                

                var document = new DocumentDTO
                {
                    DocumentTitle = contentFileName,
                    DocumentURL = blobClient.Uri.ToString(),
                    FilSizeInByte = file.Length
                };

                documents.Add(document);
            });

            return documents;
        }


        private bool ValidateSize(List<IFormFile> files)
        {
            var extensions = new List<string>() { ".pdf", ".xls", ".xlsx", ".doc", ".docx", ".csv", ".png", ".jpg", ".jpeg", ".gif", ".txt" };
            
            foreach(var file in files)
            {
                if (!extensions.Contains(Path.GetExtension(file.FileName)))
                    throw new RestException(System.Net.HttpStatusCode.BadRequest, $"{file.FileName} extension not recognized");

                if (file.Length > 200000)
                    throw new RestException(System.Net.HttpStatusCode.BadRequest, $"{file.FileName} is greater than 2MB");
            }

            return true;

        }

    }
}
