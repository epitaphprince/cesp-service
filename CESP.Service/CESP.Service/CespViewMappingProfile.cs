using System.Collections.Generic;
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
            CreateMap<Teacher, TeacherResponse>();
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
                        src => MapLevel(src.Level, src.LevelInfo)
                    ));
            CreateMap<IEnumerable<ScheduleSection>, ScheduleResponse>();

            
            CreateMap<AddSpeakingClubRequest, SpeakingClubMeeting>()
                .ForMember(dest => dest.FileInfo,
                    opt => opt.MapFrom(src => src.Name
                    ));
        }


        private string MapLevel(string level, string info)
        {
            if (string.IsNullOrWhiteSpace(level) 
                && string.IsNullOrWhiteSpace(info))
            {
                return null;
            }

            if (string.IsNullOrWhiteSpace(level))
            {
                return info;
            }

            if (string.IsNullOrWhiteSpace(info))
            {
                return level;
            }

            return $"{level}, {info}";
        }
    }
}