using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CESP.Core.Contracts;
using CESP.Core.Models;
using CESP.Database.Context.Education.Models;

namespace CESP.Core.Managers.Courses
{
    public class CourseManager : ICourseManager
    {
        private readonly ICourseProvider _courseProvider;
        private readonly ICespResourceProvider _cespResourceProvider;

        public CourseManager(
            ICourseProvider courseProvider,
            ICespResourceProvider resourceProvider)
        {
            _courseProvider = courseProvider;
            _cespResourceProvider = resourceProvider;
        }

        public async Task<List<Course>> GetList(int? count)
        {
            var courses = await _courseProvider.GetCourses(count);

            courses.ForEach(
                c =>
                {
                    c.Photo = GetUrl(c.Photo);
                    c.Icons = c.Icons.Select(GetUrl).ToArray();
                });

            return courses;
        }
        
        public async Task SaveCourseFile(string fileName, int courseId, CourseFileTypeEnum fileType)
        {
            await _courseProvider.SaveCourseFile(fileName, courseId, fileType);
        }

        private string GetUrl(string urlPart)
        {
            return _cespResourceProvider.GetFullUrl(urlPart);
        }
    }
}