using System.Collections.Generic;
using AutoMapper;
using CESP.Core.Models;
using CESP.Database.Context.Payments.Models;
using CESP.Database.Context.Schedules.Models;
using CESP.Database.Context.StudentGroups.Models;

namespace CESP.Dal.Mapping
{
    public class ScheduleMappingProfile: Profile
    {
        public ScheduleMappingProfile()
        {
            CreateMap<(StudentGroupDto group,
                    List<ScheduleDto> schedules,
                    List<PriceDto> prices,
                    List<GroupDurationDto> durations),
                    Schedule>()
                .ForMember(dest => dest.LevelRang,
                    opt => opt.MapFrom(
                        src => src.group.LanguageLevel.Rang))
                .ForMember(dest => dest.IsAvailable,
                    opt => opt.MapFrom(
                        src => src.group.IsAvailable))
                .ForMember(dest => dest.StartDate,
                    opt => opt.MapFrom(
                        src => src.group.Start))
                .ForMember(dest => dest.Level,
                    opt => opt.MapFrom(
                        src => src.group.LanguageLevel.Name))
                
                .ForMember(dest => dest.Prices,
                    opt => opt.MapFrom(
                        src => src.prices))
                .ForMember(dest => dest.Durations,
                    opt => opt.MapFrom(
                        src => src.durations))
                .ForMember(dest => dest.LessonTimes,
                    opt => opt.MapFrom(
                        src => src.schedules))
                
                ;
        }
    }
}