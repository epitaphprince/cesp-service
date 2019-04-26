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
    public class CourseProvider : ICourseProvider
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
                var groups = await _cespRepository.GetStudentGroupsByCourseId(course.Id);

                var groupPricesTasks = groups.Select(gr => _cespRepository.GetPricesByGroupId(gr.Id));

                var pricesList = await Task.WhenAll(groupPricesTasks);

                var prices = pricesList.SelectMany(pr => pr);

                var maxDiscount = prices.Max(pr => pr.DiscountPer);

                var price =
                    maxDiscount > 0 ?
                    prices.FirstOrDefault(pr => pr.DiscountPer == maxDiscount)
                    : prices.FirstOrDefault();

                coursesWithPrice.Add((course, price));
            }

            return coursesWithPrice.Select(c => _mapper.Map<(CourseDto, PriceDto), Course>(c)).ToList();
        }
    }
}