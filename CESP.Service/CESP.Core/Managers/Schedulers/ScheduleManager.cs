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
            var schedules = await _scheduleProvider.GetSchedulesByBunchId(bunchId);

            return schedules;
        }
    }
}