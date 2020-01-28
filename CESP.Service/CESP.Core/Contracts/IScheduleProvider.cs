using System.Collections.Generic;
using System.Threading.Tasks;
using CESP.Core.Models;

namespace CESP.Core.Contracts
{
    public interface IScheduleProvider
    {
        Task<List<ScheduleItem>> GetScheduleItemsByLevels(string[] levelNames);

        Task<List<ScheduleItem>> GetScheduleItems();
    }
}