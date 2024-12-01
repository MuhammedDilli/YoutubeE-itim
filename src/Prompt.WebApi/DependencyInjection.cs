using Prompt.Persistence.Services;
using Prompt.WebApi.Services;

namespace Prompt.WebApi
{
    public static class DependencyInjection
    {

        public static IServiceCollection AddWebApi(this IServiceCollection services)
        {
            services.AddHttpContextAccessor();

            services.AddScoped<ICurrentUserService, CurrentUserManager>();

            return services;
        
        
        
        }

    }
}
