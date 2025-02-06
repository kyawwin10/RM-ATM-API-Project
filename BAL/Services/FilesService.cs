using Azure.Storage;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using BAL.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using MODEL.DTO;
using MODEL.Entitied;
using REPOSITORY.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Services
{
    public class FilesService : IFilesService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly BlobContainerClient _fileContainer;

        public FilesService(IUnitOfWork unitOfWork, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;

            var credential = new StorageSharedKeyCredential(configuration["AppSettings:AzureStorageAccountName"], configuration["AppSettings:AzureAccessKey"]);
            var blobUri = $"https://{configuration["AppSettings:AzureStorageAccountName"]}.blob.core.windows.net";
            var blobServiceClient = new BlobServiceClient(new Uri(blobUri), credential);
            _fileContainer = blobServiceClient.GetBlobContainerClient(configuration["AppSettings:AzureContainer"]);
        }

        public async Task<IEnumerable<Files>> GetAllFiles()
        {
            var files = await _unitOfWork.files.GetByCondition(x => x.ActiveFlag == true);
            return files;
        }

        public async Task<string> UploadFile(IFormFile formFile)
        {
            try
            {
                // Validate if the file is not null, and check its content type and extension
                if (formFile == null ||
                    !(formFile.ContentType.Equals("image/jpeg", StringComparison.OrdinalIgnoreCase) ||
                      formFile.ContentType.Equals("image/jpg", StringComparison.OrdinalIgnoreCase)) ||
                    !(Path.GetExtension(formFile.FileName).Equals(".jpg", StringComparison.OrdinalIgnoreCase) ||
                      Path.GetExtension(formFile.FileName).Equals(".jpeg", StringComparison.OrdinalIgnoreCase)))
                {
                    return ""; // Invalid file
                }

                // Get the current date and time
                string dateTime = DateTime.UtcNow.ToString("yyyyMMdd_HHmmss");

                // Extract the file extension
                string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(formFile.FileName);
                string fileExtension = Path.GetExtension(formFile.FileName);

                // Create a new file name by appending the date and time
                string newFileName = $"{fileNameWithoutExtension}_{dateTime}{fileExtension}";

                // Use the new file name in the BlobClient
                BlobClient client = _fileContainer.GetBlobClient(newFileName);


                await using (Stream? data = formFile.OpenReadStream())
                {
                    await client.UploadAsync(data, new BlobHttpHeaders { ContentType = formFile.ContentType });
                }

                var file = new Files()
                {
                    FileName = newFileName,
                    URL = client.Uri.AbsoluteUri,
                    ContentType = formFile.ContentType,
                };

                await _unitOfWork.files.Create(file);
                await _unitOfWork.SaveChangeAsync();

                return client.Uri.AbsoluteUri;

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<bool> DeleteFile(string name)
        {
            try
            {
                var file = await _unitOfWork.files.GetFirstOrDefaultAsync(x => x.FileName == name);
                if (file != null)
                {
                    file.ActiveFlag = false;
                    _unitOfWork.files.Update(file);
                    await _unitOfWork.SaveChangeAsync();

                    return true;
                }
                return false;
            }

            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<FileResponseDTO?> DownloadAsync(string blobFilename)
        {
            BlobClient file = _fileContainer.GetBlobClient(blobFilename);
            if (await file.ExistsAsync())
            {
                var data = await file.OpenReadAsync();
                Stream blobConent = data;

                var content = await file.DownloadContentAsync();

                string name = blobFilename;
                string contentType = content.Value.Details.ContentType;

                return new FileResponseDTO { Content = blobConent, FileName = name, ContentType = contentType };
            }

            return null;
        }
    }
}
