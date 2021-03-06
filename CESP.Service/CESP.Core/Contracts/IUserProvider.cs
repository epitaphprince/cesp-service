using System.Threading.Tasks;
using CESP.Core.Models;

namespace CESP.Core.Contracts
{
    public interface IUserProvider
    {
        Task Save(User user);
    }
}