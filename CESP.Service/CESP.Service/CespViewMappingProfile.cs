using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using CESP.Core.Models;
using CESP.Service.ViewModels.Requests;
using CESP.Service.ViewModels.Responses;

namespace CESP.Service
{
    public class CespViewMappingProfile : Profile
    {
        public CespViewMappingProfile()
        {
            CreateMap<Teacher, TeacherResponse>()
                .ForMember(dest => dest.Position,
                    opt => opt.MapFrom(
                        src => src.Post));
            CreateMap<UpdateTeacherRequest, Teacher>()
                .ForMember(dest => dest.Languages,
                    opt => opt.MapFrom(
                        src => src
                            .Languages
                            .Select(l => new Language(l))
                    ));
            
            CreateMap<Course, CourseResponse>();
            CreateMap<Feedback, FeedbackResponse>();
            CreateMap<Event, EventResponse>();
            CreateMap<EventShort, EventShortResponse>();
            CreateMap<SpeakingClubMeeting, SpeakingClubMeetingResponse>();
            CreateMap<SpeakingClubMeetingShort, SpeakingClubMeetingShortResponse>();
            CreateMap<Level, LevelResponse>();
            CreateMap<Language, LanguageResponse>();

            CreateMap<ScheduleSegment, ScheduleSegmentResponse>()
                .ForMember(dest => dest.Level,
                    opt => opt.MapFrom(
                        src => src.Title
                    ));
            CreateMap<ScheduleItem, ScheduleItemResponse>()
                .ForMember(dest => dest.StartTime,
                    opt => opt.MapFrom(
                        src => src.StartTime.ToString(@"hh\:mm")))
                .ForMember(dest => dest.EndTime,
                    opt => opt.MapFrom(src => 
                        src.EndTime.ToString(@"hh\:mm")));
            
            CreateMap<IEnumerable<ScheduleBlock>, ScheduleResponse>();
            
            CreateMap<AddSpeakingClubRequest, SpeakingClubMeeting>()
                .ForMember(dest => dest.FileInfo,
                    opt => opt.MapFrom(src => src.Name
                    ));
        }
    }
}