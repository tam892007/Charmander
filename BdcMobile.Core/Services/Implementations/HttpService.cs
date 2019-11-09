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
        /// <summary>
        /// Login function
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
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
        /// Search vu viec
        /// </summary>
        /// <param name="token"></param>
        /// <param name="keyword"></param>
        /// <param name="page"></param>
        /// <param name="record"></param>
        /// <returns></returns>
        public async Task<List<Event>> SearchEventAsync(string token, string keyword, int page, int record)
        {
            string apiUrl = Constants.AppAPI.IPAPI + string.Format(Constants.AppAPI.SearchItemsAPI, token, keyword, page, record);
            var apiResponse = await NetWorkUtility.MakeRequestAsync(apiUrl, "GET");
            if (apiResponse.Length > 25)
            {
                try
                {
                    var r = JsonConvert.DeserializeObject(apiResponse);
                    var result = JsonConvert.DeserializeObject<EventResponseModel>(apiResponse);
                    return result.data;
                }
                catch (Exception ex)
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

        /// <summary>
        /// Query all Chat in of a event
        /// </summary>
        /// <param name="token"></param>
        /// <param name="eventID"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public List<ChatMessage> QueryChat(string token, int eventID, int type)
        {
            string apiUrl = Constants.AppAPI.IPAPI + string.Format(Constants.AppAPI.GetChatAPI, token, eventID, type);
            var apiResponse = NetWorkUtility.MakeRequestSync(apiUrl, "GET");
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

        /// <summary>
        /// "api/send-chat?api_token={0}&surveyID={1}&&type={2}&content={3}&file=&belongingTo={4}"
        /// </summary>
        /// <param name="token"></param>
        /// <param name="eventID"></param>
        /// <param name="type"></param>
        /// <param name="message"></param>
        /// <param name="chat"></param>
        /// <param name="belongingTo"></param>
        /// <returns></returns>
        public async Task<int> SendChatAsync(string token, int eventID, int type, string message, int chat, int belongingTo)
        {
            string apiUrl = Constants.AppAPI.IPAPI + string.Format(Constants.AppAPI.SendChatAPI, token, eventID, type, message, 0);
            var apiResponse = await NetWorkUtility.MakeRequestAsync(apiUrl, "POST");

            return 1;
        }

        public Task QueryAllFilesAsync(string token, int eventID)
        {
            throw new NotImplementedException();
        }

        public Task<List<Notification>> QueryNotificationAsync(string token, int page, int recordPerPage)
        {
            throw new NotImplementedException();
        }
    }
}
