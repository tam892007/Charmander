﻿using BdcMobile.Core.Models;
using BdcMobile.Core.Services.Interfaces;
using System;

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

        public string ServerAddress { get; set; }
        public int PullMessageTime { get; set; }

        public DateTime FromDate { get; set; }

        public DateTime ToDate { get; set; }

        public UserRole UserRole { get; set; }

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
            UserRole = UserRole.None;
        }

        public void SyncContextFromUser(User user)
        {
            UserDisplayName = user.Name;
            UserLoginName = user.AccountName;
            IsUserAuthenticated = user.IsAuthenticated;
            AvatarUrl = user.Image;
            ApiToken = user.api_token;

            if (Enum.TryParse(user.Type, out UserRole role))
            {
                UserRole = UserRole.Employee;
            }
            else
            {
                UserRole = UserRole.None;
            }
        }

        public void SaveSettings(string address, int refreshTime, DateTime fromDate, DateTime toDate)
        {
            ServerAddress = address;
            PullMessageTime = refreshTime;
            FromDate = fromDate;
            ToDate = toDate;
        }
    }
}
