using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CESP.Core.Contracts;
using CESP.Core.Models;

namespace CESP.Core.Managers.Events
{
    public class EventManager: IEventManager
    {
        private readonly IEventProvider _eventProvider;
        private readonly ICespResourceProvider _cespResourceProvider;

        public EventManager(IEventProvider eventProvider, 
            ICespResourceProvider cespResourceProvider)
        {
            _eventProvider = eventProvider;
            _cespResourceProvider = cespResourceProvider;
        }

        public async Task<List<EventShort>> GetList(int? count)
        {
            var events = await _eventProvider.GetEvents(count);
            
            events.ForEach(
                c =>
                {
                    c.Photo = _cespResourceProvider.GetFullUrl(c.Photo);
                });

            return events;
        }

        public async Task<Event> Get(string sysName)
        {
            var ev = await _eventProvider.GetEvent(sysName);

            ev.Photos = ev.Photos.Select(
                c => _cespResourceProvider.GetFullUrl(c)).ToList();
            
            return ev;
        }
    }
}