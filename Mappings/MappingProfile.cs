using AutoMapper;
using Stream.Models;
using Stream.ViewModels;
using Stream.ViewModels.Dto;

namespace Stream.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();
            
            CreateMap<Game, GameDto>();
            CreateMap<GameDto, Game>();
        }
    }
}