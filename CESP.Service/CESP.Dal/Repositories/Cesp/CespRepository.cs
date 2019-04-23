using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CESP.Core.Contracts;
using CESP.Database.Context;
using CESP.Database.Context.Education.Models;
using CESP.Database.Context.Payments.Models;
using Microsoft.EntityFrameworkCore;

namespace CESP.Dal.Repositories.Cesp
{
    public class CespRepository: ICespRepository
    {
        private readonly CespContext _context;

        public CespRepository(CespContext cespContext)
        {
            _context = cespContext;
        }

        public async Task<List<TeacherDto>> GetTeachers(int? count)
        {
            
            var teachers = count == null
                ? _context.Teachers
                : _context.Teachers.Take((int)count);
            
            return await teachers.Include(t => t.Photo).ToListAsync();
        }

        public async Task<List<CourseDto>> GetCourses(int? count)
        {
            var courses = count == null
                ? _context.Courses
                : _context.Courses.Take((int)count);
            
            return await courses.Include(c => c.Photo).ToListAsync();
        }

        public async Task<List<StudentGroupDto>> GetStudentGroupsByCourseId(int courseId)
        {
            return await _context.StudentGroups.Where(gr => gr.CourseId == courseId).ToListAsync();
        }

        public async Task<List<PriceDto>> GetPricesByGroupId(int groupId)
        {
            return await _context.Prices.Where(pr => pr.StudentGroupId == groupId).ToListAsync();
        }
    }
}