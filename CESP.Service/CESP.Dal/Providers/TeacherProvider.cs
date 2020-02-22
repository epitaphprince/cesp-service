using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CESP.Core.Contracts;
using CESP.Core.Models;
using CESP.Database.Context.Education.Models;

namespace CESP.Dal.Providers
{
    public class TeacherProvider : ITeacherProvider
    {
        private ICespRepository _cespRepository;
        private readonly IMapper _mapper;

        public TeacherProvider(ICespRepository cespRepository, IMapper mapper)
        {
            _cespRepository = cespRepository;
            _mapper = mapper;
        }

        public async Task<List<Teacher>> GetList(int? count)
        {
            var teachers = await _cespRepository.GetTeachers(count);
            return teachers.Select(t => _mapper.Map<Teacher>(t)).ToList();
        }

        public async Task Update(Teacher teacher)
        {
            var teacherDto = _mapper.Map<TeacherDto>(teacher);

            if (!string.IsNullOrEmpty(teacher.Photo))
                teacherDto.Photo = await _cespRepository.GetFile(teacher.Photo);
            if (!string.IsNullOrEmpty(teacher.SmallPhoto))
                teacherDto.SmallPhoto = await _cespRepository.GetFile(teacher.SmallPhoto);
            if (!string.IsNullOrEmpty(teacher.LargePhoto))
                teacherDto.LargePhoto = await _cespRepository.GetFile(teacher.LargePhoto);

           await _cespRepository.UpdateTeacher(teacherDto);
        }
    }
}