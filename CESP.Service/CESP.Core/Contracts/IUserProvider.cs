using System.Threading.Tasks;
using CESP.Core.Models;

namespace CESP.Core.Contracts
{
    public interface IUserProvider
    {
        Task<bool> IsExists(string contact);

        Task Save(User user);
    }
}