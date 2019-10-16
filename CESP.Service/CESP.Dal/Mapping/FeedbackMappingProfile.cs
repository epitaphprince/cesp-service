using AutoMapper;
using CESP.Core.Models;
using CESP.Database.Context.Feedbacks.Models;

namespace CESP.Dal.Mapping
{
    public class FeedbackMappingProfile : Profile
    {
        public FeedbackMappingProfile()
        {
            CreateMap<FeedbackDto, Feedback>()
                .ForMember(dest => dest.Photo,
                    opt => opt.MapFrom(src => src.Photo.Name))
                .ForMember(dest => dest.Source,
                    opt => opt.MapFrom(src => src.Source.Name));
        }
    }
}