using AutoMapper;
using Demo.PL.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace Demo.PL.MappingProfiles
{
    public class RoleProfile:Profile
    {
        public RoleProfile() 
        { 
            CreateMap<IdentityRole,RoleViewModel>().ForMember(r=>r.RoleName,o=>o.MapFrom(d=>d.Name))
                .ReverseMap();
        }
    }
}
