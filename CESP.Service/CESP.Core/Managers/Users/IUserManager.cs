using System.Threading.Tasks;
using CESP.Core.Models;

namespace CESP.Core.Managers.Users
{
    public interface IUserManager
    {
        Task Save(User user);
    }
}