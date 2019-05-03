using AutoMapper;
using CESP.Core.Models;
using CESP.Database.Context.Schedules.Models;

namespace CESP.Dal.Mapping
{
    public class LessonTimeMappingProfile : Profile
    {
        public LessonTimeMappingProfile()
        {
            CreateMap<ScheduleDto, LessonTime>()
                .ForMember(dest => dest.StartTime,
                    opt => opt.MapFrom(
                        src => src.StartLessonTime))
                .ForMember(dest => dest.EndTime,
                    opt => opt.MapFrom(
                        src => src.EndLessonTime));
        }
    }
}