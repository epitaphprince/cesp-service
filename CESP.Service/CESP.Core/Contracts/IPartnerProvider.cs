using System.Collections.Generic;
using System.Threading.Tasks;
using CESP.Core.Models;

namespace CESP.Core.Contracts
{
    public interface IPartnerProvider
    {
        Task<List<PartnerShort>> GetPartners(int? count);
        
        Task<Partner> GetPartner(string sysName);
    }
}