using BdcMobile.Core.Models;
using BdcMobile.Core.Services.Interfaces;
using BdcMobile.Core.Commons;
using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using System.Threading.Tasks;


namespace BdcMobile.Core.Services.Implementations
{
    public class HttpService: IHttpService
    {        
        public async Task<User> LoginAsync(User user)
        {
            string apiUrl = Constants.AppAPI.IPAPI + string.Format(Constants.AppAPI.UserLoginAPI, user.AccountName, user.Password);
            var apiResponse = await NetWorkUtility.MakeRequestAsync(apiUrl, "POST");
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
        public async Task<User> VerifyUserAsync(string token)
        {
            string apiUrl = Constants.AppAPI.IPAPI + string.Format(Constants.AppAPI.UserVerifyAPI, token);
            var apiResponse = await NetWorkUtility.MakeRequestAsync(apiUrl, "GET");
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
        public async Task<List<Event>> QueryEventAsync(string token, DateTime? fromdate, DateTime? todate, int currentPage, int recpordPerPage)
        {
            //var fromdatestr = fromdate == null ? string.Empty: string.Format("{0:ddMMyyyy}", fromdate);
            //var todatestr = todate == null ? string.Empty : string.Format("{0:ddMMyyyy}", todate);

            string apiUrl = Constants.AppAPI.IPAPI + string.Format(Constants.AppAPI.GetItemsAPI, currentPage, recpordPerPage, token);
            var apiResponse = await NetWorkUtility.MakeRequestAsync(apiUrl, "GET");
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

        /// <summary>
        /// Query all Chat in of a event
        /// </summary>
        /// <param name="token"></param>
        /// <param name="eventID"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public async Task<List<ChatMessage>> QueryChatAsync(string token, int eventID, int type)
        {
            //var fromdatestr = fromdate == null ? string.Empty: string.Format("{0:ddMMyyyy}", fromdate);
            //var todatestr = todate == null ? string.Empty : string.Format("{0:ddMMyyyy}", todate);

            string apiUrl = Constants.AppAPI.IPAPI + string.Format(Constants.AppAPI.GetChatAPI, token, eventID, type);
            var apiResponse = await NetWorkUtility.MakeRequestAsync(apiUrl, "GET");
            if (apiResponse.Length > 25)
            {
                try
                {
                    var r = JsonConvert.DeserializeObject(apiResponse);
                    var result = JsonConvert.DeserializeObject<ListChatMessageResponseModel>(apiResponse);
                    return result.data;
                }
                catch (Exception ex)
                {

                }
            }
            return null;
        }



    }
}
