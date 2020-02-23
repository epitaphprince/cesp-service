using System.Linq;
using AutoMapper;
using CESP.Core.Models;
using CESP.Database.Context.Education.Models;

namespace CESP.Dal.Mapping
{
    public class CourseMappingProfile : Profile
    {
        public CourseMappingProfile()
        {
            CreateMap<CourseDto, Course>()
                .ForMember(dest => dest.Photo,
                    opt => opt.MapFrom(src => src.Photo.Name))
                .ForMember(dest => dest.Name,
                    opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Description,
                    opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.DurationInfo,
                    opt => opt.MapFrom(src => src.DurationInfo))
                .ForMember(dest => dest.Icons,
                    opt => opt.MapFrom(src =>
                        src.CourseFiles
                          .Where(cf => cf.File.FileType == (int)CourseFileTypeEnum.Icon)
                          .OrderBy(cf => cf.Priority)
                          .Select(cf => cf.File.Name)));
        }
    }
}