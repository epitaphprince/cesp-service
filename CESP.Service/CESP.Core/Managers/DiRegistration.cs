using CESP.Core.Managers.Courses;
using CESP.Core.Managers.Events;
using CESP.Core.Managers.Feedbacks;
using CESP.Core.Managers.Schedulers;
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
            services.AddScoped<ICourseManager, CourseManager>();
            services.AddScoped<IFeedbackManager, FeedbackManager>();
            services.AddScoped<IScheduleManager, ScheduleManager>();
            services.AddScoped<IEventManager, EventManager>();
            
            return services;
        }
    }
}