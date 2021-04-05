using AutoMapper;
using week1.DTOs;
using week1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace week1
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, UserDto>();
            CreateMap<Role, RoleDto>()
                .ForMember(x => x.RoleName, x => x.MapFrom(x => x.Name));
            CreateMap<RoleDtoAdd, Role>()
                .ForMember(x => x.Name, x => x.MapFrom(x => x.RoleName)); ;
            CreateMap<UserRole, UserRoleDto>();
        }
    }
}