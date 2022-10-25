using Microsoft.Extensions.DependencyInjection;
using Transformador.Data;
using Transformador.Data.Repository;
using Transformador.Domain.Interfaces.Data.Repository;
using Transformador.Domain.Interfaces.Services;
using Transformador.Domain.NotificadorDeErros;
using Transformador.Services;

namespace Transformador.CrossCutting
{
    public static class DependencyContainer
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            services.AddScoped<IUserRepository, UserRepository>();

            services.AddScoped<INotificador, Notificador>();
            services.AddScoped<IUserService, UserService>();

            return services;
        }
    }
}