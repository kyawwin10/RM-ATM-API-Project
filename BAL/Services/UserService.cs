using AutoMapper;
using BAL.Common;
using BAL.IServices;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using MODEL.ApplicationConfig;
using MODEL.DTO;
using MODEL.Entitied;
using REPOSITORY.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web.Providers.Entities;

namespace BAL.Services
{
    public class UserService : IUserService
    {
        private readonly IConfiguration _configuration;
        private readonly IUnitOfWork _unitOfWork;
        private readonly TokenProvider _tokenProvider;
        private readonly IMapper _mapper;
        public UserService(IConfiguration configuration, IUnitOfWork unitOfWork, TokenProvider tokenProvider, IMapper mapper)
        {
            _configuration = configuration;
            _unitOfWork = unitOfWork;
            _tokenProvider = tokenProvider;
            _mapper = mapper;
        }

        public async Task AddUser(AddUserDTO inputModel)
        {
            try
            {
                var adduser = _mapper.Map<MODEL.Entitied.User>(inputModel); // Use AutoMapper to map DTO to Entity
                await _unitOfWork.user.Add(adduser);
                await _unitOfWork.SaveChangeAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("User Create Failed", ex);
            }
        }

        //public async Task AddUser(AddUserDTO inputModel)
        //{
        //    try
        //    {
        //        var adduser = new MODEL.Entitied.User()
        //        {
        //            UserName = inputModel.UserName,
        //            Password = inputModel.Password,
        //            Balance = inputModel.Balance ?? 0,
        //            CreatedBy = inputModel.CreatedBy,
        //        };
        //        await _unitOfWork.user.Add(adduser);
        //        await _unitOfWork.SaveChangeAsync();
        //    }
        //    catch (Exception ex) 
        //    {
        //        throw new Exception("User Create Failed", ex);
        //    }
        //}

        public async Task<UserGetByIdDTO> UserEdit(Guid userId)
        {
            try
            {
                var user_data = (await _unitOfWork.user.GetByCondition(x => x.UserID == userId && x.ActiveFlag)).FirstOrDefault();
                if (user_data is null)
                {
                    throw new KeyNotFoundException($"User with ID {userId} was not found.");
                }
                return new UserGetByIdDTO
                {
                    UserName = user_data.UserName,
                    Password = user_data.Password,
                    Balance = user_data.Balance,
                    CreatedBy = user_data.CreatedBy
                };
            }
            catch (Exception ex)
            {
                throw new Exception("An Error, Does not Exit User ID", ex);
            }
        }
        public async Task UpdateUser(UpdateUserDTO inputModel)
        {
            try
            {
                var updateuser = (await _unitOfWork.user.GetByCondition(x => x.UserID == inputModel.UserID && x.ActiveFlag)).FirstOrDefault();
                if (updateuser != null)
                {
                    _mapper.Map(inputModel, updateuser);
                    updateuser.UpdatedDate = DateTime.UtcNow;
                    _unitOfWork.user.Update(updateuser);
                }
                await _unitOfWork.SaveChangeAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}", ex);
            }
        }


        //public async Task UpdateUser(UpdateUserDTO id)
        //{
        //    try
        //    {
        //        var updateuser = (await _unitOfWork.user.GetByCondition(x => x.UserID == id.UserID && x.ActiveFlag)).FirstOrDefault();
        //        if (updateuser != null)
        //        {
        //            updateuser.UserName = id.UserName;
        //            updateuser.Password = id.Password;
        //            updateuser.Balance = id.Balance;
        //            updateuser.UpdatedBy = id.UpdatedBy;
        //            updateuser.UpdatedDate = DateTime.UtcNow;
        //           _unitOfWork.user.Update(updateuser);
        //        }
        //        await _unitOfWork.SaveChangeAsync();
        //    }
        //    catch (Exception ex) 
        //    {
        //        throw new Exception($"{ex.Message}", ex);
        //    }
        //}

        public async Task DeleteUser(DeleteUserDto inputModel)
        {
            try
            {
                var userdelete = (await _unitOfWork.user.GetByCondition(x => x.UserID == inputModel.UserID)).FirstOrDefault();
                if (userdelete != null) 
                {
                    userdelete.ActiveFlag = false;
                    _mapper.Map(inputModel, userdelete);
                    userdelete.UpdatedDate = DateTime.UtcNow;
                    _unitOfWork.user.Update(userdelete);
                }
                    await _unitOfWork.SaveChangeAsync();
            }
            catch (Exception ex) 
            {
                throw new Exception( "User Delete Fail", ex);
            }
        }

        public async Task<string> UserLogin(UserLoginDTO loginModel)
        {
            try
            {
                // Validate input
                if (string.IsNullOrWhiteSpace(loginModel.UserName) || string.IsNullOrWhiteSpace(loginModel.Password))
                {
                    throw new ArgumentException("Username and Password are required");
                }

                // Retrieve user from the repository
                var user = (await _unitOfWork.user.GetByCondition(x => x.UserName == loginModel.UserName && x.Password == loginModel.Password && x.ActiveFlag)).FirstOrDefault();

                if (user == null)
                {
                    throw new Exception("Invalid username or password");
                }

                var token = _tokenProvider.Create(user);
                return token;
            }
            catch (Exception ex)
            {
                throw new Exception("Login failed: " + ex.Message, ex);
            }
        }


    }
}
