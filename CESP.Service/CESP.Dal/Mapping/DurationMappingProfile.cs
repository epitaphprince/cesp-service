using AutoMapper;
using CESP.Core.Models;
using CESP.Database.Context.StudentGroups.Models;

namespace CESP.Dal.Mapping
{
    public class DurationMappingProfile : Profile
    {
        public DurationMappingProfile()
        {
            CreateMap<GroupDurationDto, GroupDuration>()
                .ForMember(dest => dest.Unit,
                    opt => opt.MapFrom(
                        src => src.TimeUnit.Name));
        }
    }
}