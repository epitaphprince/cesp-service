using AutoMapper;
using CESP.Core.Models;
using CESP.Database.Context.Payments.Models;

namespace CESP.Dal.Mapping
{
    public class PriceMappingProfile : Profile
    {
        public PriceMappingProfile()
        {
            CreateMap<PriceDto, Price>()
                .ForMember(dest => dest.Period,
                    opt => opt.MapFrom(
                        src =>  src.PaymentPeriod))
                .ForMember(dest => dest.Currency,
                    opt => opt.MapFrom(
                        src => src.Currency.Name));
        }
    }
}