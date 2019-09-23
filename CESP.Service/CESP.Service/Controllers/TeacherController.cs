using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CESP.Core.Managers.Teachers;
using CESP.Service.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CESP.Service.Controllers
{
    [Route("teachers")]
    public class TeacherController : Controller
    {
        private readonly ITeacherManager _teacherManager;
        private readonly IMapper _mapper;

        public TeacherController(
            ITeacherManager teacherManager,
            IMapper mapper)
        {
            _teacherManager = teacherManager;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetList(int? count)
        {
            if (count < 0)
                count = 0;

            var teachers = await _teacherManager.GetList(count);

            return Ok(teachers.Select(t => _mapper.Map<TeacherResponse>(t)));
        }
    }
}