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
            return services.ResolveRepositorys().ResolveServices();
        }

        public static IServiceCollection ResolveRepositorys(this IServiceCollection services)
        {
            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ITransformerRepository, TransformerRepository>();
            services.AddScoped<ITestRepository, TestRepository>();
            services.AddScoped<IReportRepository, ReportRepository>();

            return services;
        }

        public static IServiceCollection ResolveServices(this IServiceCollection services)
        {
            services.AddScoped<INotificador, Notificador>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ITransformerService, TransformerService>();
            services.AddScoped<ITestService, TestService>();
            services.AddScoped<IReportService, ReportService>();

            return services;
        }
    }
}