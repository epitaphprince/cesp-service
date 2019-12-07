using System.Collections.Generic;
using System.Threading.Tasks;
using CESP.Core.Contracts;
using CESP.Core.Models;

namespace CESP.Core.Managers.Schedulers
{
    public class ScheduleManager : IScheduleManager
    {
        private readonly IScheduleProvider _scheduleProvider;

        public ScheduleManager(IScheduleProvider scheduleProvider)
        {
            _scheduleProvider = scheduleProvider;
        }

        public async Task<List<ScheduleSection>> GetList()
        {
            return await _scheduleProvider.GetSchedules();
        }
        
        public async Task<List<ScheduleSegment>> GetListByLevels(string[] levelNames)
        {
            return await _scheduleProvider.GetSchedulesByLevels(levelNames);
        }

        public async Task<List<GroupBunch>> GetBunches()
        {
            return await _scheduleProvider.GetBunches();
        }
    }
}