using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CESP.Core.Contracts;
using CESP.Database.Context;
using CESP.Database.Context.Activities.Models;
using CESP.Database.Context.Education.Models;
using CESP.Database.Context.Files.Models;
using CESP.Database.Context.Partners.Models;
using CESP.Database.Context.Payments.Models;
using CESP.Database.Context.Schedules.Models;
using CESP.Database.Context.StudentGroups.Models;
using CESP.Database.Context.Users.Models;
using Microsoft.EntityFrameworkCore;

namespace CESP.Dal.Repositories.Cesp
{
    public class CespRepository : ICespRepository
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
                : _context.Teachers.Take((int) count);

            return await teachers.Include(t => t.Photo).ToListAsync();
        }

        public async Task<List<FeedbackDto>> GetFeedbacks(int? count)
        {
            var feedbacks = count == null
                ? _context.Feedbacks.OrderByDescending(f => f.Date)
                : _context.Feedbacks.OrderByDescending(f => f.Date).Take((int) count);

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

        public async Task<List<GroupBunchDto>> GetGroupBunches()
        {
            return await _context
                .GroupBunches
                .ToListAsync();
        }

        public async Task<int?> GetGroupBunchIdBySysNameOrNull(string sysName)
        {
            var bunch = await _context
                .GroupBunches
                .FirstOrDefaultAsync(gb => gb.SysName == sysName);
            
            return bunch?.Id;
        }

        public async Task<List<CourseDto>> GetCourses(int? count)
        {
            var courses = count == null
                ? _context.Courses
                : _context.Courses.Take((int) count);

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

        public async Task<List<ActivityDto>> GetEvents(int? count)
        {
            var events = count == null
                ? _context.Activities.OrderByDescending(f => f.Start)
                : _context.Activities.OrderByDescending(f => f.Start).Take((int)count);
            
            return await events
                .Include(e => e.Photo)
                .ToListAsync();
        }
        
        public async Task<ActivityDto> GetEvent(string sysName)
        {
            return await _context
                .Activities
                .Include(e => e.Photo)
                .FirstOrDefaultAsync(e => e.SysName == sysName);
        }
        
        public async Task<List<FileDto>> GetEventFiles(int eventId)
        {
            var files = from af in _context.ActivityFiles
                join f in _context.Files on af.FileId equals f.Id
                where af.ActivityId == eventId
                select f;

            return await files.ToListAsync();
        }

        public async Task<List<PartnerDto>> GetPartners(int? count)
        {
            var partners = count == null
                ? _context.Partners
                : _context.Partners.Take((int)count);
            
            return await partners
                .Include(e => e.Photo)
                .ToListAsync();
        }
        
        public async Task<PartnerDto> GetPartner(string sysName)
        {
            return await _context
                .Partners
                .Include(e => e.Photo)
                .FirstOrDefaultAsync(e => e.SysName == sysName);
        }

        public async Task<List<FileDto>> GetPartnerFiles(int partnerId)
        {
            var files = from af in _context.PartnerFiles
                join f in _context.Files on af.FileId equals f.Id
                where af.PartnerId == partnerId
                select f;

            return await files.ToListAsync();
        }

        public async Task<List<LanguageLevelDto>> GetLanguageLevels()
        {
            return await _context
                .LanguageLevels
                .OrderBy(l => l.Rang)
                .ToListAsync();
        }

        public async Task<LanguageLevelDto> GetLanguageLevel(string name)
        {
            return await _context
                .LanguageLevels
                .FirstOrDefaultAsync(l => l.Name == name);
        }
    }
}