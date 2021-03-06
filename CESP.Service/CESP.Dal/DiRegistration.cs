using CESP.Core.Contracts;
using CESP.Dal.Infrastructure;
using CESP.Dal.Providers;
using CESP.Dal.Repositories.Cesp;
using CESP.Dal.Repositories.Files;
using CESP.Database.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CESP.Dal
{
    public static class DiRegistration
    {
        public static IServiceCollection RegisterRepositories(
            this IServiceCollection services, IConfiguration configuration)
        {
            var cespConnectionString = configuration
                .GetSection("ConnectionStrings")
                .GetValue<string>("CespDb");
            services.AddDbContext<CespContext>(options =>
                options.UseNpgsql(cespConnectionString));

            services.Configure<FileStorage>(configuration.GetSection("FileStorage"));
            services.Configure<ResourceStorage>(configuration.GetSection("ResourceStorage"));

            services.AddScoped<ICespResourceProvider, CespResourceProvider>();
            services.AddScoped<ICespRepository, CespRepository>();
            services.AddScoped<ICourseProvider, CourseProvider>();
            services.AddScoped<ITeacherProvider, TeacherProvider>();
            services.AddScoped<IUserProvider, UserProvider>();
            services.AddScoped<IFeedbackProvider, FeedbackProvider>();
            services.AddScoped<IScheduleProvider, ScheduleProvider>();
            services.AddScoped<IEventProvider, EventProvider>();
            services.AddScoped<ISpeakingClubProvider, SpeakingClubProvider>();

            services.AddScoped<IPartnerProvider, PartnerProvider>();
            services.AddScoped<ILevelProvider, LevelProvider>();
            services.AddScoped<IFileInfoProvider, FileInfoProvider>();

            services.AddScoped<IFolderProvider, FolderProvider>();
            services.AddScoped<IFileProvider, FileProvider>();
            services.AddScoped<IFileRepository, FileRepository>();

            return services;
        }
    }
}