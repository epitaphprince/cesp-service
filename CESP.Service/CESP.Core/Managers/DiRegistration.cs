using CESP.Core.Managers.Teachers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CESP.Core.Managers
{
    public static class DiRegistration
    {
        public static IServiceCollection RegisterManagers(
            this IServiceCollection services, IConfiguration configuration)
        {

            services.AddScoped<ITeacherManager, TeacherManager>();
            
            return services;
        }
    }
}