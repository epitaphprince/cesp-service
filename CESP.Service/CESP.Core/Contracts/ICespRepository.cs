using System.Collections.Generic;
using System.Threading.Tasks;
using CESP.Database.Context.Activities.Models;
using CESP.Database.Context.Education.Models;
using CESP.Database.Context.Feedbacks.Models;
using CESP.Database.Context.Files.Models;
using CESP.Database.Context.Partners.Models;
using CESP.Database.Context.Payments.Models;
using CESP.Database.Context.Schedules.Models;
using CESP.Database.Context.StudentGroups.Models;
using CESP.Database.Context.Users.Models;

namespace CESP.Core.Contracts
{
    public interface ICespRepository
    {
        Task<List<TeacherDto>> GetTeachers(int? count = null);
        Task<TeacherDto> GetTeacher(int teacherId);
        Task UpdateTeacher(TeacherDto teacherDto);

        Task AddTeacher(TeacherDto teacherDto);

        Task<List<CourseDto>> GetCourses(int? count);

        Task<List<StudentGroupDto>> GetStudentGroupsByCourseId(int courseId);
        
        Task<List<PriceDto>> GetPricesByGroupId(int groupId);
        
        Task<PriceDto> GetPriceByGroupIdFirstOrDefault(int groupId);

        Task<List<FeedbackDto>> GetFeedbacks(int? count);

        Task<List<StudentGroupDto>> GetStudentGroupsByBunchId(int bunchId);

        Task<List<StudentGroupDto>> GetStudentGroups();
        
        Task<ScheduleDto> GetScheduleByGroupIdFirstOrDefault(int groupId);

        Task<List<StudentGroupDto>> GetStudentGroupsByLevels(string[] levelNames);

        Task<List<ActivityDto>> GetEvents(int? count);

        Task<ActivityDto> GetEvent(string sysName);

        Task AddEvent(ActivityDto eventDto);

        Task<List<FileDto>> GetEventFiles(int eventId);

        Task UpdateFile(string fileNameOld, string fileNameNew, string info);

        Task AddFile(FileDto file);

        Task<FileDto> GetFile(string name);

        Task<List<SpeakingClubMeetingDto>> GetSpeakingClubMeetings(int? count);

        Task<SpeakingClubMeetingDto> GetSpeakingClubMeeting(string sysName);

        Task AddSpeakingClubMeeting(SpeakingClubMeetingDto speakingClubMeetingDto);

        Task<List<PartnerDto>> GetPartners(int? count);

        Task<PartnerDto> GetPartner(string sysName);

        Task<List<FileDto>> GetPartnerFiles(int partnerId);

        Task<List<LanguageLevelDto>> GetLanguageLevels();

        Task<LanguageLevelDto> GetLanguageLevel(string name);

        Task<List<GroupBunchDto>> GetGroupBunches();

        Task<int?> GetGroupBunchIdBySysNameOrNull(string sysName);
        
        Task SaveUser(UserDto user);
    }
}