using System.Threading.Tasks;
using CESP.Core.Contracts;
using CESP.Core.Models;

namespace CESP.Core.Managers.Users
{
    public class UserManager: IUserManager
    {
        private readonly IUserProvider _userProvider;
        
        public async Task<bool> IsExists(string contact)
        {
            return await _userProvider.IsExists(contact);
        }

        public async Task Save(User user)
        {
            await _userProvider.Save(user);
        }
    }
}