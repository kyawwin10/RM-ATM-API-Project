using AutoMapper;
using MODEL.DTO;
using MODEL.Entitied;
using System.Web.Providers.Entities;

namespace RetailManagementApi
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<AddUserDTO, MODEL.Entitied.User>();
            CreateMap<UpdateUserDTO, MODEL.Entitied.User>();
            CreateMap<DeleteUserDto, MODEL.Entitied.User>();
        }
    }
}
