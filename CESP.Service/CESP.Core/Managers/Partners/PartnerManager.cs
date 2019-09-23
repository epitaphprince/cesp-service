using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CESP.Core.Contracts;
using CESP.Core.Models;

namespace CESP.Core.Managers.Partners
{
    public class PartnerManager : IPartnerManager
    {
        private readonly IPartnerProvider _partnerProvider;
        private readonly ICespResourceProvider _cespResourceProvider;

        public PartnerManager(IPartnerProvider partnerProvider,
            ICespResourceProvider cespResourceProvider)
        {
            _partnerProvider = partnerProvider;
            _cespResourceProvider = cespResourceProvider;
        }

        public async Task<List<PartnerShort>> GetList(int? count)
        {
            var partners = await _partnerProvider.GetPartners(count);

            partners.ForEach(
                p => { p.Photo = _cespResourceProvider.GetFullUrl(p.Photo); });

            return partners;
        }

        public async Task<Partner> Get(string sysName)
        {
            var partner = await _partnerProvider.GetPartner(sysName);

            partner.Photo = _cespResourceProvider.GetFullUrl(partner.Photo);

            partner.Photos = partner.Photos.Select(
                c => _cespResourceProvider.GetFullUrl(c)).ToList();

            return partner;
        }
    }
}