using System.Collections.Generic;
using System.Threading.Tasks;
using CESP.Core.Models;

namespace CESP.Core.Contracts
{
    public interface ICourseRepository
    {
         Task<List<Course>> GetListCourse(int? count);
    }
}