using MODEL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.IServices
{
    public interface IUserService
    {
        Task AddUser(AddUserDTO inputModel);
        Task<UserGetByIdDTO> UserEdit(Guid userId);
        Task UpdateUser(UpdateUserDTO id);
        Task DeleteUser(DeleteUserDto inputModel);
        Task<string> UserLogin(UserLoginDTO loginModel);
        //Task<string> UserLogin(string username, string password);
    }
}
