using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CESP.Core.Managers.Schedulers;
using CESP.Service.ViewModels;
using CESP.Service.ViewModels.Responses;
using Microsoft.AspNetCore.Mvc;

namespace CESP.Service.Controllers
{
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
        [Route("")]
        public async Task<IActionResult> GetList()
        {
            var schedules = await _scheduleManager.GetList();

            return Ok(schedules.Select(sh => _mapper.Map<ScheduleResponse>(sh)));
        }

        [HttpGet]
        [Route("bunches")]
        public async Task<IActionResult> GetBunches()
        {
            var banches = await _scheduleManager.GetBunches();

            return Ok(banches.Select(b => _mapper.Map<GroupBunchResponse>(b)));
        }

        [HttpGet]
        [Route("levels")]
        public async Task<IActionResult> GetListByLevels(string[] levelNames)
        {
            var schedules = await _scheduleManager.GetListByLevels(levelNames);

            return Ok(schedules.Select(sh => _mapper.Map<ScheduleSegmentResponse>(sh)));
        }
        
//        [HttpGet]
//        [Route("{bunch}")]
//        public async Task<IActionResult> GetList([FromRoute] string bunch)
//        {
//            var schedules = await _scheduleManager.GetList(bunch);
//
//            return Ok(schedules.Select(t => _mapper.Map<ScheduleResponse>(t)));
//        }
    }
}