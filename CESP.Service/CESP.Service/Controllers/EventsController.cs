using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CESP.Core.Contracts;
using CESP.Core.Managers.Events;
using CESP.Core.Managers.File;
using CESP.Dal.Infrastructure;
using CESP.Database.Context.Activities.Models;
using CESP.Database.Context.Files.Models;
using CESP.Service.Helpers;
using CESP.Service.ViewModels.Requests;
using CESP.Service.ViewModels.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace CESP.Service.Controllers
{
    [Route("events")]
    public class EventsController : Controller
    {
        private readonly IEventManager _eventManager;
        private readonly IEventProvider _eventProvider;
        private readonly IFileManager _fileManager;
        private readonly Credentials _credentials;
        private readonly IMapper _mapper;

        public EventsController(IEventManager eventManager,
            IEventProvider eventProvider,
            IFileManager fileManager,
            IOptions<Credentials> credentials,
            IMapper mapper)
        {
            _eventManager = eventManager;
            _eventProvider = eventProvider;
            _fileManager = fileManager;
            _credentials = credentials.Value;
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

        [HttpPost]
        [Route("")]
        public async Task Add([FromForm] IFormFile file, AddEventRequest request)
        {
            if (Request.CheckPassword(_credentials.Password))
            {
                await _fileManager.SaveContent(file, "activities");
                var eventDto = _mapper.Map<ActivityDto>(request);
                eventDto.Photo = new FileDto
                {
                    Info = request.Name,
                    Name = $"activities/{file.FileName}"
                };

                await _eventProvider.AddEvent(eventDto);
            }
        }
    }
}