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
                .ForMember(dest => dest.Position,
                    opt => opt.MapFrom(
                        src => src.Post));
            CreateMap<AddTeacherRequest, TeacherDto>();
        }
    }
}