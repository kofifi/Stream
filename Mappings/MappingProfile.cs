using AutoMapper;
using Stream.Models;
using Stream.ViewModels.Dto;

namespace Stream.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<Game, GameDto>().ReverseMap();
            CreateMap<Library, LibraryDto>().ReverseMap();
        }
    }
}