using System.Collections.Generic;
using System.Threading.Tasks;
using CESP.Core.Models;

namespace CESP.Core.Managers.Schedulers
{
    public interface IScheduleManager
    {
        Task<List<ScheduleSection>> GetList();

        Task<List<ScheduleSection>> GetListByLevels(string[] levelNames);

        Task<List<GroupBunch>> GetBunches();
    }
}