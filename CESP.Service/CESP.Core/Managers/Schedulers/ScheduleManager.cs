using System.Collections.Generic;
using System.Threading.Tasks;
using CESP.Core.Contracts;
using CESP.Core.Models;

namespace CESP.Core.Managers.Schedulers
{
    public class ScheduleManager: IScheduleManager
    {
        private readonly IScheduleProvider _scheduleProvider;

        public ScheduleManager(IScheduleProvider scheduleProvider)
        {
            _scheduleProvider = scheduleProvider;
        }

        public async Task<List<Schedule>> GetList(int bunchId)
        {
            return await _scheduleProvider.GetSchedulesByBunchId(bunchId);
        }
        
        public async Task<List<Schedule>> GetList(string bunch)
        {
            return await _scheduleProvider.GetSchedulesByBunch(bunch);
        }

        public async Task<List<GroupBunch>> GetBunches()
        {
            return await _scheduleProvider.GetBunches();
        }
    }
}