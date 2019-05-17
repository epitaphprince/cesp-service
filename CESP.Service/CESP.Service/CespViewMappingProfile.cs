using AutoMapper;
using CESP.Core.Models;
using CESP.Service.ViewModels;

namespace CESP.Service
{
    public class CespViewMappingProfile: Profile
    {
        public CespViewMappingProfile()
        {
            CreateMap<Teacher,TeacherResponse>();
            CreateMap<Course, CourseResponse>();
            CreateMap<Feedback, FeedbackResponse>();
            CreateMap<Event, EventResponse>();
            CreateMap<EventShort, EventShortResponse>();
            CreateMap<SpeakingClubMeeting, SpeakingClubMeetingResponse>();
            CreateMap<SpeakingClubMeetingShort, SpeakingClubMeetingShortResponse>();
            CreateMap<Level, LevelResponse>();

            CreateMap<Schedule, ScheduleResponse>();
            CreateMap<Price, PriceResponse>();
            CreateMap<GroupDuration, GroupDurationResponse>();
            CreateMap<LessonTime, LessonTimeResponse>();
        }
    }
}