using System.Collections.Generic;
using System.Threading.Tasks;
using CESP.Core.Models;

namespace CESP.Core.Managers.Courses
{
    public interface ICourseManager
    {
        Task<List<Course>> GetList(int? count);
    }
}