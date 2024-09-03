using Business.Services;
using Business.Services.Interfaces;
using DAL;
using DAL.Repository;
using DAL.Repository.Intefaces;

namespace API.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {
            services.AddScoped<BancoAPIContext>();
            services.AddScoped<IOcorrenciaRepository, OcorrenciaRepository>();
            services.AddScoped<IOcorrenciaService, OcorrenciaService>();

            return services;
        }
    }
}
