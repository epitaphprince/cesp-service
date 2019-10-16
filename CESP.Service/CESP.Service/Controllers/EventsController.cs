using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CESP.Core.Managers.Events;
using CESP.Service.ViewModels.Responses;
using Microsoft.AspNetCore.Mvc;

namespace CESP.Service.Controllers
{
    [Route("events")]
    public class EventsController : Controller
    {
        private readonly IEventManager _eventManager;
        private readonly IMapper _mapper;

        public EventsController(IEventManager eventManager,
            IMapper mapper)
        {
            _eventManager = eventManager;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetList(int? count)
        {
            if (count < 0)
                count = 0;

            var events = await _eventManager.GetList(count);

            return Ok(events.Select(t => _mapper.Map<EventShortResponse>(t)));
        }

        [HttpGet]
        [Route("{sysname}")]
        public async Task<IActionResult> Get([FromRoute] string sysname)
        {
            var ev = await _eventManager.Get(sysname);

            return Ok(_mapper.Map<EventResponse>(ev));
        }
    }
}