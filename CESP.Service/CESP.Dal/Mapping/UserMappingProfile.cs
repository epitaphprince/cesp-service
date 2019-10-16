using AutoMapper;
using CESP.Core.Models;
using CESP.Database.Context.Users.Models;

namespace CESP.Dal.Mapping
{
    public class UserMappingProfile: Profile
    {
        public UserMappingProfile()
        {
            CreateMap<User, UserDto>();
        }
    }
}