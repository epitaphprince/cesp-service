using AutoMapper;
using CESP.Core.Models;
using CESP.Database.Context.Education.Models;
using CESP.Database.Context.Payments.Models;

namespace CESP.Dal.Mapping
{
    public class CourseMappingProfile : Profile
    {
        public CourseMappingProfile()
        {
            CreateMap<(CourseDto course, PriceDto price), Course>()
                .ForMember(dest => dest.Photo,
                    opt => opt.MapFrom(src => src.course.Photo.Name))
                .ForMember(dest => dest.Name,
                    opt => opt.MapFrom(src => src.course.Name))
                .ForMember(dest => dest.CostInfo,
                    opt => opt.MapFrom(src => src.price.CostInfo))
                .ForMember(dest => dest.DiscountPer,
                    opt => opt.MapFrom((src => src.price.DiscountPer)))
                .ForMember(dest => dest.Description,
                    opt => opt.MapFrom(src => src.course.Description))
                .ForMember(dest => dest.DurationInfo,
                    opt => opt.MapFrom(src => src.course.DurationInfo));
        }
    }
}