using System.Collections.Generic;
using System.Threading.Tasks;
using CESP.Core.Models;

namespace CESP.Core.Managers.Schedulers
{
    public interface IScheduleManager
    {
        Task<List<Schedule>> GetList(int bunchId);
        
        Task<List<Schedule>> GetList(string bunch);

        Task<List<GroupBunch>> GetBunches();
    }
}