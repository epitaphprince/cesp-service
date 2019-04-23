using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CESP.Core.Contracts;
using CESP.Core.Models;

namespace CESP.Core.Managers.Courses
{
    public class CourseManager: ICourseManager
    {
        private readonly ICourseRepository _courseRepository;
        private readonly ICespResourceProvider _cespResourceProvider;
        
        public CourseManager(
            ICourseRepository courseRepository,
            ICespResourceProvider resourceProvider )
        {
            _courseRepository = courseRepository;
            _cespResourceProvider = resourceProvider;

        }

        public async Task<List<Course>> GetList(int? count)
        {
            var courses = await _courseRepository.GetListCourse(count);

            courses.ForEach(
                c =>
                {
                    c.Photo = GetUrl(c.Photo);
                });
            
            return courses;
        }

        private string GetUrl(string urlPart)
        {
            return _cespResourceProvider.GetFullUrl(urlPart);
        }
    }
}