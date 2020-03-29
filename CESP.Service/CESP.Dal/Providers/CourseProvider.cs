using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CESP.Core.Contracts;
using CESP.Core.Models;
using CESP.Database.Context.Education.Models;
using CESP.Database.Context.Files.Models;
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

        public async Task<List<Course>> GetCourses(int? count)
        {
            var coursesDto = await _cespRepository.GetCourses(count);
            
            var coursesFulled = new List<Course>();
            
            foreach (var courseDto in coursesDto)
            {
                var course = _mapper.Map<CourseDto, Course>(courseDto);
                
                var groups = await _cespRepository.GetStudentGroupsByCourseId(courseDto.Id);

                List<List<PriceDto>> pricesList = new List<List<PriceDto>>();

                var groupPricesTasks = groups.Select(gr => _cespRepository.GetPricesByGroupId(gr.Id));

                foreach (var groupPricesTask in groupPricesTasks)
                {
                    pricesList.Add(await groupPricesTask);
                }
                
                if (pricesList.Any())
                {
                    var prices = pricesList.SelectMany(pr => pr).Where(pr => pr.PaymentPeriod == null);
                    if (prices.Any())
                    {
                        var maxDiscount = prices.Max(pr => pr.DiscountPer);
                        course.Prices = prices.Select(pr => pr.Cost).ToArray();
                        course.DiscountPercent = maxDiscount;
                        course.CurrencyName = prices.First().Currency?.Name;
                    }
                }

                coursesFulled.Add(course);
            }

            return coursesFulled;
        }

        public async Task SaveCourseFile(string fileName, int courseId, CourseFileTypeEnum fileType)
        {
            var courseFile = new CourseFileDto
            {
                CourseId =  courseId,
                File = new FileDto
                {
                    Name = fileName,
                    FileType = (int)fileType,
                }
            };

            await _cespRepository.SaveCourseFile(courseFile);
        }
    }
}