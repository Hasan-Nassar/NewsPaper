using AutoMapper;
using User.Core.Dto;

namespace User.Mapping
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<Core.Entity.User, UserDto>().ReverseMap();
        }
    }
}