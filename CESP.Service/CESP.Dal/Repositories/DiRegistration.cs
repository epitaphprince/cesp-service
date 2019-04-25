using AutoMapper;
using CESP.Core.Contracts;
using CESP.Dal.Infrastructure;
using CESP.Dal.Mapping;
using CESP.Dal.Providers;
using CESP.Dal.Repositories.Cesp;
using CESP.Dal.Repositories.Files;
using CESP.Database.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace CESP.Dal.Repositories
{
    public static class DiRegistration
    {
        public static IServiceCollection RegisterRepositories(
            this IServiceCollection services, IConfiguration configuration)
        {
            var mappingConfig = new MapperConfiguration(
                mc =>
                {
                    mc.AddProfile(new TeachetMappingProfile());
                    mc.AddProfile(new CourseMappingProfile());
                });
            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);

            var cespConnectionString = configuration
                .GetSection("ConnectionStrings")
                .GetValue<string>("CespDb");
            services.AddDbContext<CespContext>(options =>
                options.UseNpgsql(cespConnectionString));
            
            services.Configure<FileStorage>(configuration.GetSection("FileStorage"));
            services.Configure<ResourceStorage>(configuration.GetSection("ResourceStorage"));

            services.AddScoped<ICespResourceProvider, CespResourceProvider>();
            services.AddScoped<ICespRepository, CespRepository>();
            services.AddScoped<ICourseRepository, CourseProvider>();
            services.AddScoped<ITeacherProvider, TeacherProvider>();
            
            services.AddScoped<IFolderProvider, FolderProvider>();
            services.AddScoped<IFileProvider, FileProvider>();
            services.AddScoped<IFileRepository, FileRepository>();
            
            return services;
        }
    }
}