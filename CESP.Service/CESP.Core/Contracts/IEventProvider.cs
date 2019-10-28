using System.Collections.Generic;
using System.Threading.Tasks;
using CESP.Core.Models;
using CESP.Database.Context.Activities.Models;

namespace CESP.Core.Contracts
{
    public interface IEventProvider
    {
        Task<List<EventShort>> GetEvents(int? count);

        Task<Event> GetEvent(string sysName);

        Task AddEvent(ActivityDto eventDto);
        
    }
}