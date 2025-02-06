using Asp.Versioning;
using BAL.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MODEL.ApplicationConfig;
using MODEL.DTO;

namespace RetailManagementAPI.Controllers
{
    //[Authorize]
    [ApiVersion("3.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class FilesController : ControllerBase
    {
        private readonly IFilesService _filesService;

        public FilesController(IFilesService filesService)
        {
            _filesService = filesService;
        }

        [HttpGet("GetAllFiles")]
        public async Task<IActionResult> GetAllFiles()
        {
            try
            {
                var returnData = await _filesService.GetAllFiles();
                if (returnData != null)
                {
                    return Ok(new ResponseModel { Message = "Get All File Success", Status = APIStatus.Success, Data = returnData });
                }
                return Ok(new ResponseModel { Message = "Get All File Failed", Status = APIStatus.Error });

            }
            catch (Exception ex)
            {
                return Ok(new ResponseModel { Message = ex.Message, Status = APIStatus.Error });
            }
        }

        [HttpPost("UploadFile")]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            try
            {
                var returnData = await _filesService.UploadFile(file);
                if (returnData != null)
                {
                    return Ok(new ResponseModel { Message = "UploadFile Successfully", Status = APIStatus.Success, Data = returnData });
                }
                return Ok(new ResponseModel { Message = "UploadFile Fail", Status = APIStatus.Error });
            }
            catch (Exception ex)
            {
                return Ok(new ResponseModel { Message = ex.Message, Status = APIStatus.Error });
            }


        }

        [HttpGet("DownloadFile")]
        public async Task<IActionResult> DownloadFile(string filename)
        {
            try
            {
                var result = await _filesService.DownloadAsync(filename);

                if (result != null)
                {
                    return File(result.Content, result.ContentType, result.FileName);
                }
                return Ok(new ResponseModel { Message = "DownLoadFile Failed", Status = APIStatus.Error });
            }
            catch (Exception ex)
            {
                return Ok(new ResponseModel { Message = ex.Message, Status = APIStatus.Error });
            }

        }

        [HttpPost("DeleteFile")]
        public async Task<IActionResult> DeleteFile(string filename)
        {
            try
            {
                var returnData = await _filesService.DeleteFile(filename);

                if (returnData)
                {
                    return Ok(new ResponseModel { Message = "DeleteFile Successfully", Status = APIStatus.Success, Data = returnData });
                }
                return Ok(new ResponseModel { Message = "DeleteFile Failed", Status = APIStatus.Error });
            }
            catch (Exception ex)
            {
                return Ok(new ResponseModel { Message = ex.Message, Status = APIStatus.Error });
            }
        }
    }
}
