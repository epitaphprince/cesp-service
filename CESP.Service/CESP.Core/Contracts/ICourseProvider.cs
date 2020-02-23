using System.Collections.Generic;
using System.Threading.Tasks;
using CESP.Core.Models;
using CESP.Database.Context.Education.Models;

namespace CESP.Core.Contracts
{
    public interface ICourseProvider
    {
        Task<List<Course>> GetCourses(int? count);

        Task SaveCourseFile(string fileName, int courseId, CourseFileTypeEnum fileType);
    }
}