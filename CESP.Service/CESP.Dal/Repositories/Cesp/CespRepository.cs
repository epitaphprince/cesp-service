using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CESP.Core.Contracts;
using CESP.Database.Context;
using CESP.Database.Context.Activities.Models;
using CESP.Database.Context.Education.Models;
using CESP.Database.Context.Feedbacks.Models;
using CESP.Database.Context.Files.Models;
using CESP.Database.Context.Partners.Models;
using CESP.Database.Context.Payments.Models;
using CESP.Database.Context.Schedules.Models;
using CESP.Database.Context.StudentGroups.Models;
using CESP.Database.Context.Users.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

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
            var teachersQuery = count != null
                ? _context.Teachers.OrderByDescending(t => t.Rang).Take((int) count)
                : _context.Teachers.OrderByDescending(t => t.Rang);

            return await teachersQuery
                .Include(t => t.Photo)
                .Include(t => t.SmallPhoto)
                .Include(t => t.LargePhoto)
                .Include(t => t.Languages)
                .ToListAsync();
        }

        public async Task AddTeacher(TeacherDto teacherDto)
        {
            await _context.Teachers.AddAsync(teacherDto);
            await _context.SaveChangesAsync();
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
            return await GetQeuryStudentGroups()
                .Where(sg => sg.GroupBunchId == bunchId)
                .ToListAsync();
        }

        public async Task<List<StudentGroupDto>> GetStudentGroupsByLevels(string[] levelNames)
        {
            return await GetQeuryStudentGroups()
                .Where(sg => levelNames.Contains(sg.LanguageLevel.Name))
                .ToListAsync();
        }

        private IIncludableQueryable<StudentGroupDto, FileDto> GetQeuryStudentGroups()
        {
            return _context
                .StudentGroups
                .Include(sg => sg.LanguageLevel)
                .Include(sg => sg.Teacher)
                .Include(sg => sg.Teacher.SmallPhoto);
        }

        public async Task<List<ScheduleDto>> GetSchedulesByGroupId(int groupId)
        {
            return await _context
                .Schedules
                .Where(sch => sch.StudentGroupId == groupId)
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
                : _context.Activities.OrderByDescending(f => f.Start).Take((int) count);

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

        public async Task AddEvent(ActivityDto eventDto)
        {
            await _context.Activities.AddAsync(eventDto);
            var activityFilesDto = new ActivityFilesDto
            {
                Activity = eventDto,
                File = eventDto.Photo
            };
            await _context.ActivityFiles.AddAsync(activityFilesDto);
            await _context.SaveChangesAsync();
        }

        public async Task<List<FileDto>> GetEventFiles(int eventId)
        {
            var files = from af in _context.ActivityFiles
                join f in _context.Files on af.FileId equals f.Id
                where af.ActivityId == eventId
                select f;

            return await files.ToListAsync();
        }

        public async Task AddSpeakingClubMeeting(SpeakingClubMeetingDto speakingClubMeetingDto)
        {
            await _context.SpeakingClubMeetings.AddAsync(speakingClubMeetingDto);
            await _context.SaveChangesAsync();
        }

        public async Task<List<PartnerDto>> GetPartners(int? count)
        {
            var partners = count == null
                ? _context.Partners
                : _context.Partners.Take((int) count);

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

        public async Task<List<SpeakingClubMeetingDto>> GetSpeakingClubMeetings(int? count)
        {
            var meetings = count == null
                ? _context.SpeakingClubMeetings.OrderByDescending(m => m.Date)
                : _context.SpeakingClubMeetings.OrderByDescending(m => m.Date).Take((int) count);

            return await meetings
                .Include(e => e.Photo)
                .Include(e => e.MinLanguageLevel)
                .Include(e => e.MaxLanguageLevel)
                .Include(e => e.Teacher)
                .ToListAsync();
        }

        public async Task<SpeakingClubMeetingDto> GetSpeakingClubMeeting(string sysName)
        {
            return await _context
                .SpeakingClubMeetings
                .Include(e => e.MinLanguageLevel)
                .Include(e => e.MaxLanguageLevel)
                .Include(e => e.Teacher)
                .FirstOrDefaultAsync(e => e.SysName == sysName);
        }

        private async Task<bool> IsUserExists(string contact)
        {
            return await _context.Users
                       .FirstOrDefaultAsync(
                           u => u.Contact == contact) != null;
        }

        public async Task SaveUser(UserDto user)
        {
            if (await IsUserExists(user.Contact))
            {
                return;
            }

            _context.Users.Add(user);
            _context.SaveChanges();
        }
    }
}