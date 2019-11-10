using BdcMobile.Core.Models;

namespace BdcMobile.Core.Services.Interfaces
{
    public interface IAppContext
    {
        string UserLoginName { get; set; }
        string UserDisplayName { get; set; }
        bool IsUserAuthenticated { get; set; }
        string ApiToken { get; set; }
        string AvatarUrl { get; set; }
        string CloudMessagingToken { get; set; }

        void Reset();
    }
}
