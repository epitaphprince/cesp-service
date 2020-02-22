using AutoMapper;
using CESP.Core.Models;
using CESP.Database.Context.Education.Models;
using CESP.Service.ViewModels.Requests;

namespace CESP.Dal.Mapping
{
    public class TeacherMappingProfile : Profile
    {
        public TeacherMappingProfile()
        {
            CreateMap<TeacherDto, Teacher>()
                .ForMember(dest => dest.Photo,
                    opt => opt.MapFrom(src => src.Photo.Name))
                .ForMember(dest => dest.SmallPhoto,
                    opt => opt.MapFrom(src => src.SmallPhoto.Name))
                .ForMember(dest => dest.LargePhoto,
                    opt => opt.MapFrom(src => src.LargePhoto.Name))
                .ForMember(dest => dest.Languages,
                    opt => opt.MapFrom(src => src.Languages))
                .ForMember(dest => dest.Post,
                    opt => opt.MapFrom(
                        src => src.Post));
            CreateMap<Teacher, TeacherDto>();
            CreateMap<AddTeacherRequest, TeacherDto>();
            CreateMap<LanguageDto, Language>();
        }
    }
}