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
            public static string FCMExtraName = "BDC_Notification";

            public static string LogTag = "BDC_Mobile";

        }

        public static class AppAPI
        {
            public static string IPAPI = "http://103.47.192.239:81/";
            public static string UserLoginAPI = "api/login?accountName={0}&password={1}";
            public static string UserVerifyAPI = "api/user?api_token={0}";
            public static string UserInformationAPI = "api/account?api_token={0}&accountID={1}";

            public static string GetItemsAPI = "api/vu-viec?fromDay=&toDay=&page={0}&record={1}&api_token={2}";
            public static string SearchItemsAPI = "api/tim-kiem-vu-viec?api_token={0}&keyWord={1}&page={2}&record={3}";

            public static string GetChatAPI = "api/get-chat?api_token={0}&surveyID={1}&type={2}";
            public static string GetNewChatAPI = "api/get-new-chat?api_token={0}&surveyID={1}&type={2}&createTime={3}";
            public static string GetOldChatAPI = "api/get-old-chat?api_token={0}&surveyID={1}&type={2}&createTime={3}";
            public static string SendChatAPI = "api/send-chat?api_token={0}&surveyID={1}&&type={2}&content={3}&belongingTo={4}&file={5}";
            

            public static string GetListFileAPI = "api/file-list?api_token={0}&surveyID={1}";
            public static string GetNotification = "api/get-notification?api_token={0}&page={1}&record={2}";
        }

        public static class SecureStorageKey
        {
            public static string Username = "Username";
            public static string Password = "Password";
            public static string OAuthToken = "oauth_token";
        }

        public static class ChatType
        {
            public static int InternalChat = 1;
            public static int ExternalChat = 0;
        }


    }
}
