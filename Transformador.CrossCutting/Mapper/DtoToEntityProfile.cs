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
            CreateMap<User, UserVMComplete>().ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));

            CreateMap<TransformerDto, Transformer>();
            CreateMap<Transformer, TransformerVM>().ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));
            CreateMap<Transformer, TransformerVMComplete>().ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));

            CreateMap<TestDto, Test>();
            CreateMap<Test, TestVM>().ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));
            // CreateMap<Test, TestVMComplete>().ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));
        }
    }
}