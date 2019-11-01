using BdcMobile.Core.Models;
using BdcMobile.Core.Services.Interfaces;

namespace BdcMobile.Core.Services.Implementations
{
    public class LoginService: ILoginService
    {
        public User Login(string userName, string password)
        {
            return new User
            {
                LoginName = "tam892007",
                DisplayName = "Tam Lai Duc",
                IsAuthenticated = true,
                Token = "xxx",
            };
        }
    }
}
