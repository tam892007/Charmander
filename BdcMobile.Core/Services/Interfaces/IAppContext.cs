using BdcMobile.Core.Models;
using System;

namespace BdcMobile.Core.Services.Interfaces
{
    public interface IAppContext
    {
        string ServerAddress { get; set; }
        int PullMessageTime { get; set; }
        string UserLoginName { get; set; }
        string UserDisplayName { get; set; }
        bool IsUserAuthenticated { get; set; }
        string ApiToken { get; set; }
        string AvatarUrl { get; set; }
        string CloudMessagingToken { get; set; }

        DateTime FromDate { get; set; }

        DateTime ToDate { get; set; }

        UserRole UserRole { get; set; }

        void Reset();

        void SyncContextFromUser(User user);

        void SaveSettings(string address, int refreshTime, DateTime fromDate, DateTime toDate);
    }
}
