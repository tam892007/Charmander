using BdcMobile.Core.Models;

namespace BdcMobile.Core.Services.Interfaces
{
    public interface IAppContext
    {
        string ServerAddress { get; set; }
        string UserLoginName { get; set; }
        string UserDisplayName { get; set; }
        bool IsUserAuthenticated { get; set; }
        string ApiToken { get; set; }
        string AvatarUrl { get; set; }
        string CloudMessagingToken { get; set; }

        UserRole UserRole { get; set; }

        void Reset();

        void SyncContextFromUser(User user);

        void SetServerAddress(string address);
    }
}
