using Microsoft.AspNetCore.Http;
using MODEL.DTO;
using MODEL.Entitied;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.IServices
{
    public interface IFilesService
    {
        Task<IEnumerable<Files>> GetAllFiles();
        Task<string> UploadFile(IFormFile formFile);
        Task<FileResponseDTO?> DownloadAsync(string blobFilename);
        Task<bool> DeleteFile(string name);
    }
}

//Task<IEnumerable<Files>> GetAllFiles();
//Task<string> UploadFile(IFormFile formFile);
//Task<FileResponseDTO?> DownloadAsync(string blobFilename);
//Task<bool> DeleteFile(string name);
