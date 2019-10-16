using System.Threading.Tasks;
using AutoMapper;
using CESP.Core.Contracts;
using CESP.Core.Models;
using CESP.Database.Context.Users.Models;

namespace CESP.Dal.Providers
{
    public class UserProvider: IUserProvider
    {
        private ICespRepository _cespRepository;
        private readonly IMapper _mapper;

        public UserProvider(ICespRepository cespRepository, IMapper mapper)
        {
            _cespRepository = cespRepository;
            _mapper = mapper;
        }

        public async Task<bool> IsExists(string contact)
        {
            return await _cespRepository.IsUserExists(contact);
        }

        public async Task Save(User user)
        {
            var userDb = _mapper.Map<UserDto>(user);
            await _cespRepository.SaveUser(userDb);
        }
    }
}