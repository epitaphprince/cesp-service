using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CESP.Core.Contracts;
using CESP.Core.Managers.File;
using CESP.Core.Managers.Teachers;
using CESP.Dal.Infrastructure;
using CESP.Database.Context.Education.Models;
using CESP.Database.Context.Files.Models;
using CESP.Service.Helpers;
using CESP.Service.ViewModels.Requests;
using CESP.Service.ViewModels.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace CESP.Service.Controllers
{
    [Route("teachers")]
    public class TeacherController : Controller
    {
        private readonly ITeacherManager _teacherManager;
        private readonly IMapper _mapper;
        private readonly Credentials _credentials;
        private readonly IFileManager _fileManager;
        private readonly ICespRepository _cespRepository;

        public TeacherController(
            ITeacherManager teacherManager,
            IMapper mapper, 
            IOptions<Credentials> credentials,
            IFileManager fileManager,
            ICespRepository cespRepository)
        {
            _teacherManager = teacherManager;
            _mapper = mapper;
            _credentials = credentials.Value;
            _fileManager = fileManager;
            _cespRepository = cespRepository;
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

        [HttpPost]
        [Route("")]
        public async Task<IActionResult> Add([FromForm] IFormFile file, [FromForm] IFormFile fileSmall,
            [FromForm] IFormFile fileLarge, AddTeacherRequest request)
        {
            if (Request.CheckPassword(_credentials.Password))
            {
                await _fileManager.SaveImage(file, "teachers");
                await _fileManager.SaveImage(fileSmall, "teachers");
                await _fileManager.SaveImage(fileLarge, "teachers");
                var teacherDto = _mapper.Map<TeacherDto>(request);
                teacherDto.Photo = new FileDto
                {
                    Info = request.Name,
                    Name = $"teachers/{file.FileName}"
                };
                teacherDto.SmallPhoto = new FileDto
                {
                    Info = request.Name,
                    Name = $"teachers/{fileSmall.FileName}"
                };
                teacherDto.LargePhoto = new FileDto
                {
                    Info = request.Name,
                    Name = $"teachers/{fileLarge.FileName}"
                };
                await _cespRepository.AddTeacher(teacherDto);
                return Ok();
            }

            return BadRequest();
        }
    }
}