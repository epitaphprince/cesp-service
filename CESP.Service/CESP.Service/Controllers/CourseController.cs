using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CESP.Core.Managers.Courses;
using CESP.Core.Managers.File;
using CESP.Dal.Infrastructure;
using CESP.Database.Context.Education.Models;
using CESP.Service.Helpers;
using CESP.Service.ViewModels.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace CESP.Service.Controllers
{
    [ApiController]
    [Route("courses")]
    public class CourseController : Controller
    {
        private readonly string FileFolder = "courses";
        
        private readonly ICourseManager _courseManager;
        private readonly IMapper _mapper;
        private readonly Credentials _credentials;
        private readonly IFileManager _fileManager;

        public CourseController(
            ICourseManager courseManager,
            IMapper mapper,
            IOptions<Credentials> credentials,
            IFileManager fileManager)
        {
            _courseManager = courseManager;
            _mapper = mapper;
            _credentials = credentials.Value;
            _fileManager = fileManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetList(int? count)
        {
            if (count < 0)
                count = 0;

            var courses = await _courseManager.GetList(count);

            return Ok(courses.Select(t => _mapper.Map<CourseResponse>(t)));
        }
        
        [HttpPost]
        [Route("addfile")]
        public async Task<IActionResult> AddFile([FromForm] IFormFile file, int courseId)
        {
            if (Request.CheckPassword(_credentials.Password))
            {
                if (file != null)
                {
                    await _fileManager.SaveContent(file, FileFolder);

                    await _courseManager.SaveCourseFile(AddFileFolder(file.FileName), courseId, CourseFileTypeEnum.Icon);
                }

                return Ok();
            }
            return BadRequest();
        }
        
        private string AddFileFolder(string source)
        {
            return _fileManager.GetFilePath(FileFolder, source);
        }
    }
}