using Microsoft.Extensions.DependencyInjection;
using Transformador.Data;
using Transformador.Domain.Interfaces.Data.Repository;

namespace Transformador.CrossCutting
{
    public static class DependencyContainer
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            return services;
        }
    }
}