using System.Collections.Generic;
using System.Threading.Tasks;
using CESP.Core.Models;

namespace CESP.Core.Managers.Events
{
    public interface IEventManager
    {
        Task<List<EventShort>> GetList(int? count);

        Task<Event> Get(string sysName);
    }
}