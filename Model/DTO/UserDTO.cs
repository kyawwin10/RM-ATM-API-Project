using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL.DTO
{
    public class AddUserDTO
    {
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public decimal? Balance { get; set; }
        public string? CreatedBy { get; set; }
    }

    public class UserGetByIdDTO
    {
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public decimal? Balance { get; set; }
        public string? CreatedBy { get; set; }
    }
    public class UpdateUserDTO
    {
        public Guid UserID { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public decimal? Balance { get; set; }
        public string? UpdatedBy { get;set; }
    }

    public class DeleteUserDto
    {
        public Guid UserID { get; set; }
        public string? UpdatedBy { get; set; }
    }

    public class UserLoginDTO
    {
        public string? UserName { get; set; }
        public string? Password { get; set; }
    }
}
