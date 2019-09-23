using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using CESP.Core.Models;
using CESP.Database.Context.Activities.Models;
using CESP.Database.Context.Files.Models;

namespace CESP.Dal.Mapping
{
    public class EventMappingProfile : Profile
    {
        public EventMappingProfile()
        {
            CreateMap<ActivityDto, EventShort>()
                .ForMember(dest => dest.Photo,
                    opt => opt.MapFrom(src => src.Photo.Name));

            CreateMap<(ActivityDto ev, List<FileDto> files), Event>()
                .ForMember(dest => dest.Name,
                    opt => opt.MapFrom(src => src.ev.Name))
                .ForMember(dest => dest.Info,
                    opt => opt.MapFrom(src => src.ev.Info))
                .ForMember(dest => dest.Start,
                    opt => opt.MapFrom(src => src.ev.Start))
                .ForMember(dest => dest.End,
                    opt => opt.MapFrom(src => src.ev.End))
                .ForMember(dest => dest.Photos,
                    opt => opt.MapFrom(src => src.files.Select(f => f.Name)));
        }
    }
}