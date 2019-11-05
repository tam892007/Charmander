using System;
using System.Collections.Generic;
using System.Text;

namespace BdcMobile.Core.Commons
{
    public static class Constants
    {
        public static class AppConfig
        {
            public static string FCMChannelID = "BDC_Notification_Channel";
            public static string FCMChannelName = "BDC Notification Channel";
            public static string FCMChannelDesc = "Channel to send notification for BDC App";
        }

        public static class AppAPI
        {
            public static string IPAPI = "http://103.47.192.239:81/";
            public static string UserLoginAPI = "api/login?accountName={0}&password={1}";
            public static string UserVerifyAPI = "api/user?api_token={0}";
            public static string UserInformationAPI = "api/account?api_token={0}&accountID={1}";
            public static string GetChatAPI = "api/get-chat?api_token={0}&surveyID={1}&type={2}";


            public static string GetItemsAPI = "api/vu-viec?fromDay=&toDay=&currentPage={0}&recpordPerPage={1}&api_token={2}";
        }

        public static class SecureStorageKey
        {
            public static string Username = "Username";
            public static string Password = "Password";
        }

        public static class ChatType
        {
            public static int InternalChat = 1;
            public static int ExternalChat = 0;
        }


    }
}
