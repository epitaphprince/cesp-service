using CESP.Core.Managers.Courses;
using CESP.Core.Managers.Events;
using CESP.Core.Managers.Feedbacks;
using CESP.Core.Managers.LanguageLevels;
using CESP.Core.Managers.Partners;
using CESP.Core.Managers.Schedulers;
using CESP.Core.Managers.SpeakingClub;
using CESP.Core.Managers.Teachers;
using CESP.Core.Managers.Users;
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
            services.AddScoped<IUserManager, UserManager>();
            services.AddScoped<ICourseManager, CourseManager>();
            services.AddScoped<IFeedbackManager, FeedbackManager>();
            services.AddScoped<IScheduleManager, ScheduleManager>();
            services.AddScoped<IEventManager, EventManager>();
            services.AddScoped<ISpeakingClubManager, SpeakingClubManager>();

            services.AddScoped<IPartnerManager, PartnerManager>();
            services.AddScoped<ILevelManager, LevelManager>();

            return services;
        }
    }
}