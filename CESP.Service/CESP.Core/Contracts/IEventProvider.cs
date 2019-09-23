using System.Collections.Generic;
using System.Threading.Tasks;
using CESP.Core.Models;

namespace CESP.Core.Contracts
{
    public interface IEventProvider
    {
        Task<List<EventShort>> GetEvents(int? count);

        Task<Event> GetEvent(string sysName);
    }
}