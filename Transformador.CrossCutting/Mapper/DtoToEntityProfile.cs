using AutoMapper;
using Transformador.Domain.Dtos;
using Transformador.Domain.Dtos.ViewModels;
using Transformador.Domain.Entities;

namespace Transformador.CrossCutting.Mapper
{
    internal class DtoToEntityProfile : Profile
    {
        public DtoToEntityProfile()
        {
            CreateMap<UserDto, User>();
            CreateMap<User, UserVM>().ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));
        }
    }
}