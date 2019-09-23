using System.Collections.Generic;
using System.Threading.Tasks;
using CESP.Core.Models;

namespace CESP.Core.Contracts
{
    public interface IScheduleProvider
    {
        Task<List<Schedule>> GetSchedulesByBunchId(int bunchId);

        Task<List<Schedule>> GetSchedulesByBunch(string bunch);

        Task<List<GroupBunch>> GetBunches();
    }
}