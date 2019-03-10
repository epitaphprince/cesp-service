using AutoMapper;
using CESP.Core.Models;
using CESP.Database.Context.Education.Models;

namespace CESP.Dal.Repositories.Cesp
{
    public class CespMappingProfile: Profile
    {
        public CespMappingProfile()
        {
            CreateMap<TeacherDto,Teacher>()
                .ForMember(dest => dest.Photo, 
                    opt => opt.MapFrom(src => src.Photo.Name));
        }
    }
}