using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CESP.Core.Contracts;
using CESP.Core.Managers.File;
using CESP.Core.Managers.SpeakingClub;
using CESP.Dal.Infrastructure;
using CESP.Database.Context.Education.Models;
using CESP.Database.Context.Files.Models;
using CESP.Service.ViewModels.Requests;
using CESP.Service.ViewModels.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace CESP.Service.Controllers
{
    [Route("club")]
    public class SpeakingClubController : Controller
    {
        private readonly ISpeakingClubManager _speakingClubManager;
        private readonly ICespRepository _cespRepository;
        private readonly IFileManager _fileManager;
        private readonly Credentials _credentials;
        private readonly IMapper _mapper;

        public SpeakingClubController(
            ISpeakingClubManager speakingClubManager,
            IFileManager fileManager,
            ICespRepository cespRepository,
            IMapper mapper, IOptions<Credentials> credentials)
        {
            _speakingClubManager = speakingClubManager;
            _fileManager = fileManager;
            _cespRepository = cespRepository;
            _mapper = mapper;
            _credentials = credentials.Value;
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

        [HttpPost]
        [Route("")]
        public async Task<IActionResult> Add([FromForm] IFormFile file, AddSpeakingClubRequest request)
        {
            if (Request.Headers.Any(header => header.Key.Equals("Password")
                                                          && header.Value.Equals(_credentials.Password)))
            {
                await _fileManager.SaveImage(file, "club");
                var speakingClubDto = _mapper.Map<SpeakingClubMeetingDto>(request);
                speakingClubDto.Photo = new FileDto
                {
                    Info = request.Name,
                    Name = $"club/{file.FileName}"
                };
                await _cespRepository.AddSpeakingClubMeeting(speakingClubDto);
                return Ok();
            }

            return BadRequest();
        }
    }
}