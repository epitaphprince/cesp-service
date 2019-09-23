using AutoMapper;
using CESP.Core.Models;
using CESP.Database.Context.Education.Models;

namespace CESP.Dal.Mapping
{
    public class LevelMappingProfile : Profile
    {
        public LevelMappingProfile()
        {
            CreateMap<LanguageLevelDto, LevelShort>()
                .ForMember(dest => dest.SysName,
                    opt => opt.MapFrom(
                        src => string.IsNullOrEmpty(src.Description) ? null : src.Name));
        }
    }
}