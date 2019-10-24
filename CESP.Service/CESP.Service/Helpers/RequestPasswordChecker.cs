using System.Linq;
using Microsoft.AspNetCore.Http;

namespace CESP.Service.Helpers
{
    public static class RequestPasswordChecker
    {
        public static bool CheckPassword(this HttpRequest request, string password)
        {
            return request.Headers.Any(header => header.Key.Equals("Password")
                                                 && header.Value.Equals(password));
        }
    }
}