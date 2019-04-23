using System.Collections.Generic;
using System.Threading.Tasks;
using CESP.Core.Models;

namespace CESP.Core.Managers.Teachers
{
    public interface ITeacherManager
    {
        Task<List<Teacher>> GetList(int? count);
    }
}