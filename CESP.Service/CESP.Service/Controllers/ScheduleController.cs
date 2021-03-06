using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CESP.Core.Managers.Schedulers;
using CESP.Service.ViewModels.Responses;
using Microsoft.AspNetCore.Mvc;

namespace CESP.Service.Controllers
{ 
    [ApiController]
    [Route("schedule")]
    public class ScheduleController : Controller
    {
        private readonly IScheduleManager _scheduleManager;
        private readonly IMapper _mapper;

        public ScheduleController(
            IScheduleManager scheduleManager,
            IMapper mapper)
        {
            _scheduleManager = scheduleManager;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetList()
        {
            var schedules = await _scheduleManager.GetList();

            return Ok(schedules.Select(sh => _mapper.Map<ScheduleResponse>(sh)));
        }

        [HttpGet]
        [Route("levels")]
        public async Task<IActionResult> GetListByLevels([FromQuery]string[] levelNames)
        {
            var schedules = await _scheduleManager.GetListByLevels(levelNames);

            return Ok(schedules.Select(sh => _mapper.Map<ScheduleSegmentResponse>(sh)));
        }
    }
}