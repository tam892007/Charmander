using System;
using System.Collections.Generic;
using System.Text;

namespace BdcMobile.Core.Utility
{
    public static class Constants
    {
        public static class AppAPI
        {
            public static string IPAPI = "http://103.47.192.239:81/";
            public static string UserLoginAPI = "api/login?accountName={0}&password={1}";
            public static string UserVerifyAPI = "api/user?api_token={0}";
            public static string UserInformationAPI = "api/account?api_token={0}&accountID={1}";


            public static string GetItemsAPI = "api/vu-viec?fromDay=&toDay=&currentPage={0}&recpordPerPage={1}&api_token={2}";
        }
    }
}
