using BdcMobile.Core.Models;
using BdcMobile.Core.Services.Interfaces;
using BdcMobile.Core.Utility;
using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using System.Threading.Tasks;


namespace BdcMobile.Core.Services.Implementations
{
    public class HttpService: IHttpService
    {        
        public User LoginAsync(User user)
        {
            string apiUrl = Constants.AppAPI.IPAPI + string.Format(Constants.AppAPI.UserLoginAPI, user.AccountName, user.Password);
            var apiResponse = Common.MakeRequest(apiUrl, "POST");
            if (apiResponse.Length > 25)
            {
                var result = JsonConvert.DeserializeObject<LoginResponseModel>(apiResponse);
                return result.user;
            }
            return null;
        }


        /// <summary>
        /// Verify user login by token
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public User VerifyUserAsync(string token)
        {
            string apiUrl = Constants.AppAPI.IPAPI + string.Format(Constants.AppAPI.UserVerifyAPI, token);
            var apiResponse = Common.MakeRequest(apiUrl, "GET");
            if (apiResponse.Length > 25)
            {
                var result = JsonConvert.DeserializeObject<LoginResponseModel>(apiResponse);
                return result.user;
            }
            return null;
        }


        /// <summary>
        /// Query all event in duration
        /// </summary>
        /// <param name="token"></param>
        /// <param name="fromdate"></param>
        /// <param name="todate"></param>
        /// <returns></returns>
        public List<Event> QueryEvent(string token, DateTime? fromdate, DateTime? todate, int currentPage, int recpordPerPage)
        {
            //var fromdatestr = fromdate == null ? string.Empty: string.Format("{0:ddMMyyyy}", fromdate);
            //var todatestr = todate == null ? string.Empty : string.Format("{0:ddMMyyyy}", todate);

            string apiUrl = Constants.AppAPI.IPAPI + string.Format(Constants.AppAPI.GetItemsAPI, currentPage, recpordPerPage, token);
            var apiResponse = Common.MakeRequest(apiUrl, "GET");
            if (apiResponse.Length > 25)
            {
                try
                {
                    var r = JsonConvert.DeserializeObject(apiResponse);
                    var result = JsonConvert.DeserializeObject<EventResponseModel>(apiResponse);
                    return result.data;
                }
                catch(Exception ex)
                {

                }
            }
            return null;
        }
    }
}
