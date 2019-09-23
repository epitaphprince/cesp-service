using System.Threading.Tasks;
using AutoMapper;
using CESP.Core.Managers.LanguageLevels;
using CESP.Service.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace CESP.Service.Controllers
{
    [Route("levels")]
    public class LanguageLevelController : Controller
    {
        private readonly ILevelManager _levelManager;
        private readonly IMapper _mapper;

        public LanguageLevelController(ILevelManager levelManager, IMapper mapper)
        {
            _levelManager = levelManager;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetList()
        {
            var levels = await _levelManager.GetList();

            return Ok(levels.Select(t => _mapper.Map<LevelShortResponse>(t)));
        }

        [HttpGet]
        [Route("{name}")]
        public async Task<IActionResult> Get([FromRoute] string name)
        {
            var level = await _levelManager.Get(name);

            return Ok(_mapper.Map<LevelResponse>(level));
        }
    }
}