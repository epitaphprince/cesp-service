using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CESP.Core.Contracts;
using CESP.Core.Models;
using CESP.Database.Context.Education.Models;
using CESP.Database.Context.Payments.Models;

namespace CESP.Dal.Providers
{
    public class TeacherProvider: ITeacherProvider
    {
        private ICespRepository _cespRepository;
        private readonly IMapper _mapper;
        
        public TeacherProvider(ICespRepository cespRepository, IMapper mapper)
        {
            _cespRepository = cespRepository;
            _mapper = mapper;
        }

        public async Task<List<Teacher>> GetTeachers(int? count)
        {
            var teachers = await _cespRepository.GetTeachers(count);
            return teachers.Select(t => _mapper.Map<Teacher>(t)).ToList();
        }
    }
}