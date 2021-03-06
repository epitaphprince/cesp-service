using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CESP.Core.Contracts;
using CESP.Dal.Infrastructure;
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

        public async Task<TeacherDto> GetTeacher(int teacherId)
        {
            return await _context
                .Teachers
                .FirstAsync(t => t.Id == teacherId);
        }

        public async Task AddTeacher(TeacherDto teacherDto)
        {
            await _context.Teachers.AddAsync(teacherDto);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateTeacher(TeacherDto teacherDto)
        {
            var teacherFromDb = await _context
                .Teachers
                .FirstOrDefaultAsync(t => t.Id == teacherDto.Id);

            if (!string.IsNullOrEmpty(teacherDto.Name))
                teacherFromDb.Name = teacherDto.Name;
            if (!string.IsNullOrEmpty(teacherDto.Post))
                teacherFromDb.Post = teacherDto.Post;
            if (!string.IsNullOrEmpty(teacherDto.Info))
                teacherFromDb.Info = teacherDto.Info;
            if (!string.IsNullOrEmpty(teacherDto.ShortInfo))
                teacherFromDb.ShortInfo = teacherDto.ShortInfo;
            if (!string.IsNullOrEmpty(teacherDto.City))
                teacherFromDb.City = teacherDto.City;
            if (teacherDto.Rang != 0)
                teacherFromDb.Rang = teacherDto.Rang;
            if (teacherDto.Photo != null)
                teacherFromDb.Photo = teacherDto.Photo;
            if (teacherDto.LargePhoto != null)
                teacherFromDb.LargePhoto = teacherDto.LargePhoto;
            if (teacherDto.SmallPhoto != null)
                teacherFromDb.SmallPhoto = teacherDto.SmallPhoto;
            
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

        public async Task<List<StudentGroupDto>> GetStudentGroups()
        {
            return await GetQeuryStudentGroups()
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
                .Include(sg => sg.Bunch)
                .Include(sg => sg.GroupTime)
                .Include(sg => sg.Teacher.Photo)
                .Include(sg => sg.Teacher.LargePhoto)
                .Include(sg => sg.Teacher.Languages)
                .Include(sg => sg.Teacher.SmallPhoto);
        }
        
        public async Task<ScheduleDto> GetScheduleByGroupIdFirstOrDefault(int groupId)
        {
            return await _context
                .Schedules
                .Where(sch => sch.StudentGroupId == groupId)
                .FirstOrDefaultAsync();
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
                ? _context.Courses.OrderBy(c => c.Priority)
                : _context.Courses.OrderBy(c => c.Priority).Take((int) count);

            return await courses
                .Include(c => c.Photo)
                  .Include(p => p.CourseFiles)
                  .ThenInclude(courseFile => courseFile.File)
                .ToListAsync();
        }

        public async Task SaveCourseFile(CourseFileDto courseFile)
        {
            await _context.CourseFiles.AddAsync(courseFile);
            await _context.SaveChangesAsync();
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
        
        public async Task<PriceDto> GetPriceByGroupIdFirstOrDefault(int groupId)
        {
            return await _context
                .Prices
                .Where(pr => pr.StudentGroupId == groupId)
                .Include(pr => pr.Currency)
                .FirstOrDefaultAsync();
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

        public async Task UpdateFile(string fileNameOld, string fileNameNew, string info)
        {
            var file = await _context
                .Files
                .FirstOrDefaultAsync(f => string.Equals(f.Name,
                    fileNameOld,
                    StringComparison.OrdinalIgnoreCase));
            file.Name = fileNameNew;
            file.Info = info;
            
            _context.SaveChanges();
        }

        public async Task AddFile(FileDto file)
        {
            await _context.Files.AddAsync(file);
            await _context.SaveChangesAsync();
        }

        public async Task<FileDto> GetFile(string name)
        {
            return await _context
                .Files
                .FirstAsync(f => string.Equals(f.Name, name, StringComparison.OrdinalIgnoreCase));
        }
        
        public async Task<FileDto> GetFile(int id)
        {
            return await _context
                .Files
                .FirstAsync(f => f.Id == id);
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