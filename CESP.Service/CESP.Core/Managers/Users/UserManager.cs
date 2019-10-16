using System.Threading.Tasks;
using CESP.Core.Contracts;
using CESP.Core.Models;

namespace CESP.Core.Managers.Users
{
    public class UserManager: IUserManager
    {
        private readonly IUserProvider _userProvider;

        public UserManager(IUserProvider userProvider)
        {
            _userProvider = userProvider;
        }

        public async Task Save(User user)
        {
            await _userProvider.Save(user);
        }
    }
}