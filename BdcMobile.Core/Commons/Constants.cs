using System;
using System.Collections.Generic;
using System.Text;

namespace BdcMobile.Core.Commons
{
    public static class Constants
    {
        public static class AppConfig
        {
            public const string FCMChannelID = "BDC_Notification_Channel";
            public const string FCMChannelName = "BDC Notification Channel";
            public const string FCMChannelDesc = "Channel to send notification for BDC App";
            public const string FCMExtraName = "BDC_Notification";

            public const string LogTag = "BDC_Mobile";
            public const int PullMessageTime = 20;

            public const string ServerAddressKey = "Server-Address";
            public const string PullMessageTimeKey = "Pull-Message-Time";
            public const string UserLoggedIn = "User-Logged-In";
            public const string DataFromDate = "Data-From-Date";
            public const string DataToDate = "Data-To-Date";
        }

        public static class AppAPI
        {
            //PROD: http://iis.badinco.com.vn/
            //UAT: http://103.47.192.239:81/
            public static string IPAPI = "http://iis.badinco.com.vn/";
            public static string UserLoginAPI = "api/login?accountName={0}&password={1}&fcm_token={2}";
            public static string UserVerifyAPI = "api/user?api_token={0}";
            public static string UserInformationAPI = "api/account?api_token={0}&accountID={1}";
            public static string UserLogoutAPI = "api/logout?api_token={0}";

            public static string GetItemByIdAPI = "api/get-survey-info?api_token={0}&surveyID={1}";
            public static string GetItemsAPI = "api/vu-viec?fromDay={3}&toDay={4}&page={0}&record={1}&api_token={2}";
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

        public static class TimeZone
        {
            public static string HanoiTime = "Asia/Bangkok";
        }

        public static class DateTimeFormat
        {
            public static string DateOnlyFormat = "dd/MM/yyyy";
            public static string DateAndTimeFormat = "dd/MM/yyyy HH:mm:ss";
            public static string DateAndTimeFormatConvert = "{0:dd/MM/yyyy HH:mm:ss}";
        }

        
    }
    public enum ScrollDirection
    {
        UP, DOWN
    }
}
