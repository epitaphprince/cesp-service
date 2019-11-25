using System.Collections.Generic;
using System.Threading.Tasks;
using CESP.Core.Models;

namespace CESP.Core.Contracts
{
    public interface IScheduleProvider
    {
        Task<List<ScheduleSection>> GetSchedules();

        Task<List<GroupBunch>> GetBunches();
    }
}