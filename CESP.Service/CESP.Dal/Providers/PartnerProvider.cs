using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CESP.Core.Contracts;
using CESP.Core.Models;
using CESP.Database.Context.Files.Models;
using CESP.Database.Context.Partners.Models;

namespace CESP.Dal.Providers
{
    public class PartnerProvider : IPartnerProvider
    {
        private ICespRepository _cespRepository;
        private readonly IMapper _mapper;

        public PartnerProvider(ICespRepository cespRepository,
            IMapper mapper)
        {
            _cespRepository = cespRepository;
            _mapper = mapper;
        }

        public async Task<List<PartnerShort>> GetPartners(int? count)
        {
            var partners = await _cespRepository.GetPartners(count);

            return partners.Select(t => _mapper.Map<PartnerShort>(t)).ToList();
        }

        public async Task<Partner> GetPartner(string sysName)
        {
            var partner = await _cespRepository.GetPartner(sysName);

            var files = await _cespRepository.GetPartnerFiles(partner.Id);

            return _mapper.Map<(PartnerDto, List<FileDto>), Partner>((partner, files));
        }
    }
}