using AutoMapper;
using CESP.Core.Models;
using CESP.Database.Context.Education.Models;

namespace CESP.Dal.Mapping
{
    public class SpeakingClubMappingProfile : Profile
    {
        public SpeakingClubMappingProfile()
        {
            CreateMap<SpeakingClubMeetingDto, SpeakingClubMeetingShort>()
                .ForMember(dest => dest.Photo,
                    opt => opt.MapFrom(src => src.Photo.Name))
                .ForMember(dest => dest.MinLanguageLevel,
                    opt => opt.MapFrom(src => src.MinLanguageLevel.Name))
                .ForMember(dest => dest.MaxLanguageLevel,
                    opt => opt.MapFrom(src => src.MaxLanguageLevel.Name))
                .ForMember(dest => dest.Teacher,
                    opt => opt.MapFrom(src => src.Teacher.Name));

            CreateMap<SpeakingClubMeetingDto, SpeakingClubMeeting>()
                .ForMember(dest => dest.MinLanguageLevel,
                    opt => opt.MapFrom(src => src.MinLanguageLevel.Name))
                .ForMember(dest => dest.MaxLanguageLevel,
                    opt => opt.MapFrom(src => src.MaxLanguageLevel.Name))
                .ForMember(dest => dest.Teacher,
                    opt => opt.MapFrom(src => src.Teacher.Name));
        }
    }
}