using System.Collections.Generic;
using System.Threading.Tasks;
using CESP.Core.Models;
using CESP.Database.Context.Education.Models;

namespace CESP.Core.Managers.Courses
{
    public interface ICourseManager
    {
        Task<List<Course>> GetList(int? count);
        Task SaveCourseFile(string fileName, int courseId, CourseFileTypeEnum fileType);
    }
}