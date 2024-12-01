using Prompt.Persistence.Services;
using Prompt.WebApi.Configuration;
using Prompt.WebApi.Services;

namespace Prompt.WebApi
{
    public static class DependencyInjection
    {

        public static IServiceCollection AddWebApi(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerWithVersion();
            services.AddMemoryCache();
            services.AddApiVersioning(
           options =>
           {
               options.ReportApiVersions = true;
           });
            services.AddHttpContextAccessor();

            services.AddScoped<ICurrentUserService, CurrentUserManager>();

            return services;
        
        
        
        }

    }
}


