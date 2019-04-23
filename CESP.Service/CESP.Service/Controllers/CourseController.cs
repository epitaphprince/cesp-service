using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CESP.Core.Managers.Courses;
using CESP.Service.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CESP.Service.Controllers
{
    [Route("courses")]
    public class CourseController: Controller
    {
        private readonly ICourseManager _courseManager;
        private readonly IMapper _mapper;  
        
        public CourseController(
            ICourseManager courseManager,
            IMapper mapper)
        {
            _courseManager = courseManager;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("")]
        public async Task<CourseResponse[]> GetList(int? count)
        {
            if (count < 0)
                count = 0;
            
            var courses = await _courseManager.GetList(count);
            
            return courses.Select(t => _mapper.Map<CourseResponse>(t)).ToArray();
        }
        
        [HttpGet]
        [Route("method")]
        public IActionResult Method()
        {
            return new OkResult();
        }
    }
}