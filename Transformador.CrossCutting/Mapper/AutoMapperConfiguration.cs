using AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace Transformador.CrossCutting.Mapper
{
    public static class AutoMapperConfiguration
    {
        public static IServiceCollection AddAutoMapperConfiguration(this IServiceCollection services)
        {
            MapperConfiguration config = new(config =>
            {
                config.AddProfile(new DtoToEntityProfile());
            });
            IMapper mapper = config.CreateMapper();
            services.AddSingleton(mapper);

            return services;
        }
    }
}