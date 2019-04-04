using System.Collections.Generic;
using System.Threading.Tasks;
using CESP.Core.Models;

namespace CESP.Core.Contracts
{
    public interface ICespRepository
    {
        Task<List<Teacher>> GetListTeacher();
    }
}