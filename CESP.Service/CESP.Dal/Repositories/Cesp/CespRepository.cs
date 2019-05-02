using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CESP.Core.Contracts;
using CESP.Core.Models;
using CESP.Database.Context;
using CESP.Database.Context.Education.Models;
using CESP.Database.Context.Payments.Models;
using CESP.Database.Context.Schedules.Models;
using CESP.Database.Context.StudentGroups.Models;
using CESP.Database.Context.Users.Models;
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

        public async Task<List<FeedbackDto>> GetFeedbacks(int? count)
        {
            var feedbacks = count == null
                ? _context.Feedbacks.OrderByDescending(f => f.Date)
                : _context.Feedbacks.OrderByDescending(f => f.Date).Take((int)count);

            return await feedbacks
                .Include(f => f.Photo)
                .Include(f => f.Source)
                .ToListAsync();
        }

        public async Task<List<StudentGroupDto>> GetStudentGroupsByBunchId(int bunchId)
        {
            return await _context
                .StudentGroups
                .Where(sg => sg.GroupBunchId == bunchId)
                .Include(sg => sg.LanguageLevel)
                .ToListAsync();
        }

        public async Task<List<ScheduleDto>> GetSchedulesByGroupId(int groupId)
        {
            return await _context
                .Schedules
                .Where(sch => sch.StudentGroupId == groupId)
                .ToListAsync();
        }

        public async Task<List<GroupDurationDto>> GetDurationsByGroupId(int groupId)
        {
            return await _context
                .GroupDurations
                .Where(sch => sch.StudentGroupId == groupId)
                .Include(sch => sch.TimeUnit)
                .ToListAsync();
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
            return await _context
                .Prices
                .Where(pr => pr.StudentGroupId == groupId)
                .Include(pr => pr.Currency)
                .ToListAsync();
        }
    }
}