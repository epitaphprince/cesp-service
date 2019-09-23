using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CESP.Core.Managers.SpeakingClub;
using CESP.Service.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CESP.Service.Controllers
{
    [Route("club")]
    public class SpeakingClubController : Controller
    {
        private readonly ISpeakingClubManager _speakingClubManager;
        private readonly IMapper _mapper;

        public SpeakingClubController(
            ISpeakingClubManager speakingClubManager,
            IMapper mapper)
        {
            _speakingClubManager = speakingClubManager;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetList(int? count)
        {
            if (count < 0)
                count = 0;

            var meetings = await _speakingClubManager.GetList(count);

            return Ok(meetings.Select(t => _mapper.Map<SpeakingClubMeetingShortResponse>(t)));
        }

        [HttpGet]
        [Route("{sysname}")]
        public async Task<IActionResult> Get([FromRoute] string sysname)
        {
            var ev = await _speakingClubManager.Get(sysname);

            return Ok(_mapper.Map<SpeakingClubMeetingResponse>(ev));
        }
    }
}