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
    public class CourseProvider: ICourseRepository
    {
        private ICespRepository _cespRepository;
        private readonly IMapper _mapper;
        
        public CourseProvider(ICespRepository cespRepository, IMapper mapper)
        {
            _cespRepository = cespRepository;
            _mapper = mapper;
        }

        public async Task<List<Course>> GetListCourse(int? count)
        {
            var courses = await _cespRepository.GetCourses(count);
            
            var coursesWithPrice = new List<(CourseDto, PriceDto)>();
            foreach (var course in courses)
            {
                PriceDto price = null;
                var groups = await _cespRepository.GetStudentGroupsByCourseId(course.Id);
                foreach (var group in groups)
                {
                    var pr = (await _cespRepository.GetPricesByGroupId(group.Id)).FirstOrDefault();
                    if (pr != null)
                    {
                        price = pr;
                        continue;
                    }
                }
                coursesWithPrice.Add((course, price));
            }
            
            return coursesWithPrice.Select(c => _mapper.Map<(CourseDto, PriceDto),Course>(c)).ToList();
        }
    }
}