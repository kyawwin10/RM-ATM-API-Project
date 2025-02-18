using Asp.Versioning;
using AutoMapper;
using BAL.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MODEL.ApplicationConfig;
using MODEL.DTO;
using MODEL.Entitied;
using REPOSITORY.UnitOfWork;

namespace RetailManagementApi.Controllers
{
    //[Authorize]
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    //[Route("api/[controller]")]

    public class UserController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserService _userService;
        public UserController(IUnitOfWork unitOfWork, IUserService userService)
        {
            _unitOfWork = unitOfWork;
            _userService = userService;
        }

        //[Authorize]
        [HttpGet("GetAllUser")]
        public async Task<IActionResult> GetAllUser()
        {
            try
            {
                var userdata = await _unitOfWork.user.GetByCondition(x => x.ActiveFlag == true);
                return Ok(new ResponseModel {Message = "Get All User Success", Status = APIStatus.Success, Data = userdata});
            }
            catch (Exception ex)
            {
                return Ok(new ResponseModel {Message = ex.Message, Status = APIStatus.Error});
            }
        }


        //[Authorize]
        [HttpPost("AddUser")]
        public async Task<IActionResult> AddUser(AddUserDTO inputModel)
        {
            try
            {
                await _userService.AddUser(inputModel);
                return Ok(new ResponseModel { Message = "User Create Successfully", Status = APIStatus.Success});
            }
            catch (Exception ex) 
            {
                return Ok(new ResponseModel { Message = ex.Message, Status = APIStatus.Error});
            }
        }

        //[Authorize]
        [HttpPatch("UserEdit")]
        public async Task<IActionResult> UserEdit(Guid id)
        {
            try
            {
                if (id != null)
                {
                    var user_data = await _userService.UserEdit(id);
                    return Ok(new ResponseModel { Message = "User Get By ID Successfully", Status = APIStatus.Success, Data = user_data });
                }
                else
                {
                    return Ok(new ResponseModel { Message = "User ID Not Found", Status = APIStatus.Error });
                }
            }
            catch (Exception ex)
            {
                return Ok(new ResponseModel { Message = ex.Message, Status = APIStatus.Error });
            }
        }

        //[Authorize]
        [HttpPut("UpdateUser")]
        public async Task<IActionResult> UpdateUser(UpdateUserDTO id)
        {
            try
            {
                if (id != null)
                {
                    await _userService.UpdateUser(id);
                    return Ok(new ResponseModel { Message = "User Updated Successfully", Status = APIStatus.Success });
                }
                else 
                { 
                    return Ok(new ResponseModel { Message = "User Updated Fail", Status = APIStatus.Error });
                }
            }
            catch (Exception ex)
            {
                return Ok(new ResponseModel { Message = ex.Message, Status = APIStatus.Error });
            }
        }

        //[Authorize]
        [HttpDelete("DeleteUser")]
        public async Task<IActionResult> DeleteUser(DeleteUserDto inputModel)
        {
            try
            {
                await _userService.DeleteUser(inputModel);
                return Ok(new ResponseModel { Message = "User Delete Successfully", Status = APIStatus.Success });
            }
            catch (Exception ex) 
            {
                return Ok(new ResponseModel { Message = ex.Message, Status = APIStatus.Error});
            }
        }

        [HttpPost("UserLogin")]
        public async Task<IActionResult> UserLogin(UserLoginDTO loginModel)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(loginModel.UserName) || string.IsNullOrWhiteSpace(loginModel.Password))
                {
                    return Ok(new ResponseModel { Message = "Username or Password cannot be empty", Status = APIStatus.Error });
                }

                var token = await _userService.UserLogin(loginModel);
                return Ok(new ResponseModel { Message = "User Login Successfully", Status = APIStatus.Success, Data = token });
            }
            catch (Exception ex)
            {
                return Ok(new ResponseModel { Message = ex.Message, Status = APIStatus.Error });
            }
        }

    }
}
