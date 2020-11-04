using Author.Core.Dto;
using AutoMapper;
using Author.Core.Entity;
using Common.Events;

namespace Author.Mapping
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<Core.Entity.Author, AuthorDto>().ReverseMap();
            CreateMap<UserCreatedEvent, AuthorDto>().ReverseMap();
        }
    }
}