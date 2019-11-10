using BdcMobile.Core.Models;
using BdcMobile.Core.Services.Interfaces;

namespace BdcMobile.Core.Services.Implementations
{
    public class AppContext : IAppContext
    {
        public AppContext()
        {
            Initialize();
        }
        public string UserLoginName { get; set; }
        public string UserDisplayName { get; set; }
        public bool IsUserAuthenticated { get; set; }
        public string ApiToken { get; set; }
        public string AvatarUrl { get; set; }
        public string CloudMessagingToken { get; set; }

        public void Reset()
        {
            Initialize();
        }

        private void Initialize()
        {
            UserLoginName = string.Empty;
            UserDisplayName = string.Empty;
            IsUserAuthenticated = false;
            UserLoginName = string.Empty;
            ApiToken = string.Empty;
            AvatarUrl = string.Empty;
            CloudMessagingToken = string.Empty;
        }
    }
}
