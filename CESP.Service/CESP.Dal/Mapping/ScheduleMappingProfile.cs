using System.Collections.Generic;
using AutoMapper;
using CESP.Core.Models;
using CESP.Database.Context.Education.Models;
using CESP.Database.Context.Payments.Models;
using CESP.Database.Context.Schedules.Models;
using CESP.Database.Context.StudentGroups.Models;

namespace CESP.Dal.Mapping
{
    public class ScheduleMappingProfile : Profile
    {
        public ScheduleMappingProfile()
        {
            CreateMap<GroupBunchDto, GroupBunch>();

            CreateMap<(GroupBunchDto bunch,
                    IEnumerable<ScheduleSegment> segments),
                    ScheduleBlock>()
                .ForMember(dest => dest.Name,
                    opt => opt.MapFrom(
                        src => src.bunch.Name))
                .ForMember(dest => dest.Description,
                    opt => opt.MapFrom(
                        src => src.bunch.Duration))
                .ForMember(dest => dest.ScheduleSegments,
                    opt => opt.MapFrom(
                        src => src.segments));

            CreateMap<(StudentGroupDto group,
                    TeacherDto teacher,
                    ScheduleDto schedule,
                    PriceDto price,
                    LanguageLevelDto level),
                    ScheduleItem>()

                .ForMember(dest => dest.LanguageLevel,
                    opt => opt.MapFrom(
                        src => src.level
                    ))
                .ForMember(dest => dest.Bunch,
                    opt => opt.MapFrom(
                        src => BunchGroupEnumConverter
                            .ParseBunchGroupEnum(src.group.GroupBunchId)
                       ))
                .ForMember(dest => dest.BunchName,
                    opt => opt.MapFrom(
                        src => src.group.Bunch.Name))
                // Teacher info
                .ForMember(dest => dest.Teacher,
                    opt => opt.MapFrom(
                        src => src.teacher))
                .ForMember(dest => dest.TeacherPhoto,
                    opt => opt.MapFrom(
                        src => src.teacher.SmallPhoto.Name))
                .ForMember(dest => dest.TeacherName,
                    opt => opt.MapFrom(
                        src => src.teacher.Name))
                .ForMember(dest => dest.TeacherPost,
                    opt => opt.MapFrom(
                        src => src.teacher.Post))
                // Priority info
                .ForMember(dest => dest.BunchPriority,
                    opt => opt.MapFrom(
                        src => src.group.Bunch.Priority))
                .ForMember(dest => dest.TimePriority,
                    opt => opt.MapFrom(
                        src => src.group.GroupTime.Priority))
                // date-time info
                .ForMember(dest => dest.Days,
                    opt => opt.MapFrom(
                        src => src.schedule.Day))
                .ForMember(dest => dest.StartTime,
                    opt => opt.MapFrom(
                        src => src.schedule.StartLessonTime))
                .ForMember(dest => dest.EndTime,
                    opt => opt.MapFrom(
                        src => src.schedule.EndLessonTime))
                .ForMember(dest => dest.StartDate,
                    opt => opt.MapFrom(
                        src => src.group.Start))
                .ForMember(dest => dest.IsAvailable,
                    opt => opt.MapFrom(
                        src => src.group.IsAvailable))
                // price
                .ForMember(dest => dest.Price,
                    opt => opt.MapFrom(
                        src => src.price.Cost))
                .ForMember(dest => dest.PriceWithoutDiscount,
                    opt => opt.MapFrom(
                        src =>
                            src.price.DiscountPer == null
                                ? null
                                : src.price.CostFull
                    ))
                .ForMember(dest => dest.Discount,
                    opt => opt.MapFrom(
                        src => src.price.DiscountPer));
        }
    }
}