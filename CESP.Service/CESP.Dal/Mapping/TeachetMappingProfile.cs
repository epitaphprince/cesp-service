using AutoMapper;
using CESP.Core.Models;
using CESP.Database.Context.Education.Models;

namespace CESP.Dal.Mapping
{
    public class TeachetMappingProfile: Profile
    {
        public TeachetMappingProfile()
        {
            CreateMap<TeacherDto,Teacher>()
                .ForMember(dest => dest.Photo, 
                    opt => opt.MapFrom(src => src.Photo.Name))
                .ForMember(dest => dest.Position,
                    opt => opt.MapFrom(
                        src => src.Post));
        }
    }
}