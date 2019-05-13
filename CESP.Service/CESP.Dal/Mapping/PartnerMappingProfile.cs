using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using CESP.Core.Models;
using CESP.Database.Context.Files.Models;
using CESP.Database.Context.Partners.Models;

namespace CESP.Dal.Mapping
{
    public class PartnerMappingProfile: Profile
    {
        public PartnerMappingProfile()
        {
            CreateMap<PartnerDto, PartnerShort>()
                .ForMember(dest => dest.Photo,
                    opt => opt.MapFrom(src => src.Photo.Name));
            
            CreateMap<(PartnerDto partner, List<FileDto> files), Partner>()
                .ForMember(dest => dest.Photo,
                    opt => opt.MapFrom(src => src.partner.Photo.Name))
                .ForMember(dest => dest.Name,
                    opt => opt.MapFrom(src => src.partner.Name))
                .ForMember(dest => dest.SysName,
                    opt => opt.MapFrom(src => src.partner.SysName))
                .ForMember(dest => dest.Address,
                    opt => opt.MapFrom(src => src.partner.Address))
                .ForMember(dest => dest.Email,
                    opt => opt.MapFrom(src => src.partner.Email))
                .ForMember(dest => dest.Info,
                    opt => opt.MapFrom(src => src.partner.Info))
                .ForMember(dest => dest.Phone,
                    opt => opt.MapFrom(src => src.partner.Phone))
                .ForMember(dest => dest.SocialNetwork,
                    opt => opt.MapFrom(src => src.partner.SocialNetwork))
                .ForMember(dest => dest.Url,
                    opt => opt.MapFrom(src => src.partner.Url))
                
                .ForMember(dest => dest.Photos,
                    opt => opt.MapFrom(src => src.files.Select(f => f.Name)));
        }
    }
}