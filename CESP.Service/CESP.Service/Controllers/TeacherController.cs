using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CESP.Core.Contracts;
using CESP.Core.Managers.File;
using CESP.Core.Managers.Teachers;
using CESP.Core.Models;
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
    [ApiController]
    [Route("teachers")]
    public class TeacherController : Controller
    {
        private readonly string FileFolder = "teachers";
        
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
        public async Task<IActionResult> GetList(int? count)
        {
            if (count < 0)
                count = 0;

            var teachers = await _teacherManager.GetList(count);

            return Ok(teachers.Select(t => _mapper.Map<TeacherResponse>(t)));
        }

        [HttpPost]
        [Route("update")]
        public async Task<IActionResult> Update([FromForm] IFormFile file,
            [FromForm] IFormFile fileSmall,
            [FromForm] IFormFile fileLarge,
            UpdateTeacherRequest request)
        {
            if (Request.CheckPassword(_credentials.Password))
            {
                if (file != null)
                {
                    await _fileManager.UpdateFile(file, request.Photo, FileFolder, request.Name);
                    request.Photo = AddFileFolder(file.Name);
                }

                if (fileSmall != null)
                {
                    await _fileManager.UpdateFile(fileSmall, request.SmallPhoto, FileFolder, request.Name);
                    request.SmallPhoto = AddFileFolder(fileSmall.Name);
                }

                if (fileLarge != null)
                {
                    await _fileManager.UpdateFile(fileLarge, request.LargePhoto, FileFolder, request.Name);
                    request.LargePhoto = AddFileFolder(fileLarge.Name);
                }
                
                var teacher = _mapper.Map<Teacher>(request);
                await _teacherManager.Update(teacher);
                
                return Ok();
            }
            return BadRequest();
        }

        private string AddFileFolder(string source)
        {
            return _fileManager.GetFilePath(FileFolder, source);
        }
        
        [HttpPost]
        public async Task<IActionResult> Add([FromForm] IFormFile file, [FromForm] IFormFile fileSmall,
            [FromForm] IFormFile fileLarge, AddTeacherRequest request)
        {
            if (Request.CheckPassword(_credentials.Password))
            {
                await _fileManager.SaveContent(file, FileFolder);
                await _fileManager.SaveContent(fileSmall, FileFolder);
                await _fileManager.SaveContent(fileLarge, FileFolder);
                var teacherDto = _mapper.Map<TeacherDto>(request);
                teacherDto.Photo = new FileDto
                {
                    Info = request.Name,
                    Name = AddFileFolder(file.FileName)
                };
                teacherDto.SmallPhoto = new FileDto
                {
                    Info = request.Name,
                    Name = AddFileFolder(fileSmall.FileName)
                };
                teacherDto.LargePhoto = new FileDto
                {
                    Info = request.Name,
                    Name = AddFileFolder(fileLarge.FileName)
                };
                await _cespRepository.AddTeacher(teacherDto);
                return Ok();
            }

            return BadRequest();
        }
    }
}