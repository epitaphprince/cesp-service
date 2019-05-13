using System.Collections.Generic;
using System.Threading.Tasks;
using CESP.Core.Models;

namespace CESP.Core.Managers.Partners
{
    public interface IPartnerManager
    {
        Task<List<PartnerShort>> GetList(int? count);
        
        Task<Partner> Get(string sysName);
    }
}