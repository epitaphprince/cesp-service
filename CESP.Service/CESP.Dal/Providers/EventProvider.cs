using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CESP.Core.Contracts;
using CESP.Core.Models;
using AutoMapper;
using CESP.Database.Context.Activities.Models;
using CESP.Database.Context.Files.Models;

namespace CESP.Dal.Providers
{
    public class EventProvider: IEventProvider
    {
        private ICespRepository _cespRepository;
        private readonly IMapper _mapper;

        public EventProvider(ICespRepository cespRepository,
            IMapper mapper)
        {
            _cespRepository = cespRepository;
            _mapper = mapper;
        }

        public async Task<List<EventShort>> GetEvents(int? count)
        {
            var events = await _cespRepository.GetEvents(count);
            return events
                .Select(t => _mapper.Map<EventShort>(t))
                .OrderByDescending(ev => ev.Start)
                .ToList();
        }

        public async Task<Event> GetEvent(string sysName)
        {
            var ev = await _cespRepository.GetEvent(sysName);

            var files = await _cespRepository.GetEventFiles(ev.Id);
            
            return _mapper.Map<(ActivityDto,List<FileDto>),Event>((ev, files));
        }
    }
}