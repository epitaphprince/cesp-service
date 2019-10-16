using System.Threading.Tasks;
using CESP.Core.Models;

namespace CESP.Core.Managers.Users
{
    public interface IUserManager
    {
        Task<bool> IsExists(string contact);

        Task Save(User user);
    }
}