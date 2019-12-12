using BdcMobile.Core.Models;
using BdcMobile.Core.Services.Interfaces;
using BdcMobile.Core.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using System.Threading.Tasks;
using MvvmCross.Logging;
using System.Threading;

namespace BdcMobile.Core.Services.Implementations
{
    public class HttpService: IHttpService
    {
        private IMvxLogProvider _mvxLogProvider;
        //private IMvxLog mvxLog;


        public HttpService(IMvxLogProvider mvxLogProvider)
        {
            _mvxLogProvider = mvxLogProvider;
            //mvxLog = _mvxLogProvider.GetLogFor(Constants.AppConfig.LogTag);
        }
        /// <summary>
        /// Login function
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<User> LoginAsync(User user, CancellationToken token = default)
        {
            string apiUrl = App.Context.ServerAddress + string.Format(Constants.AppAPI.UserLoginAPI, user.AccountName, user.Password, user.FCMToken);
            try
            {
                var apiResponse = await NetWorkUtility.MakeRequestAsync(apiUrl, "POST", token);
                if (apiResponse.Length > 25)
                {
                    var result = JsonConvert.DeserializeObject<LoginResponseModel>(apiResponse);
                    return result.user;
                }
            }
            catch (Exception ex)
            {
            }
            return null;
        }

        public bool Logout(string apiToken)
        {
            string apiUrl = App.Context.ServerAddress + string.Format(Constants.AppAPI.UserLogoutAPI, apiToken);
            var apiResponse = NetWorkUtility.MakeRequestSync(apiUrl, "GET");
            var result = JsonConvert.DeserializeObject<LoginResponseModel>(apiResponse);
            if (bool.TryParse(result.data, out bool v))
            {
                return v;
            }

            return false;
        }

        /// <summary>
        /// Verify user login by token
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<User> VerifyUserAsync(string token)
        {
            string apiUrl = App.Context.ServerAddress + string.Format(Constants.AppAPI.UserVerifyAPI, token);
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

            string apiUrl = App.Context.ServerAddress + string.Format(Constants.AppAPI.GetItemsAPI, currentPage, recpordPerPage, token);
            var apiResponse = await NetWorkUtility.MakeRequestAsync(apiUrl, "GET");
            if (apiResponse.Length > 25)
            {
                try
                {
                    var result = JsonConvert.DeserializeObject<EventResponseModel>(apiResponse);
                    return result.data;
                }
                catch(Exception ex)
                {
                    //mvxLog.Error(ex.ToString());
                    //mvxLog.Error(ex.StackTrace);
                }
            }
            return null;
        }

        public async Task<List<Event>> QueryEventAsync(string token, DateTime? fromdate, DateTime? todate, int currentPage, int recpordPerPage, CancellationToken ct)
        {
            //var fromdatestr = fromdate == null ? string.Empty: string.Format("{0:ddMMyyyy}", fromdate);
            //var todatestr = todate == null ? string.Empty : string.Format("{0:ddMMyyyy}", todate);
            try
            {
                string apiUrl = App.Context.ServerAddress + string.Format(Constants.AppAPI.GetItemsAPI, currentPage, recpordPerPage, token);
                var apiResponse = await NetWorkUtility.MakeRequestAsync(apiUrl, "GET", ct);
                if (apiResponse.Length > 25)
                {
                    try
                    {
                        var result = JsonConvert.DeserializeObject<EventResponseModel>(apiResponse);
                        return result.data;
                    }
                    catch (Exception ex)
                    {
                        //mvxLog.Error(ex.ToString());
                        //mvxLog.Error(ex.StackTrace);
                    }
                }
                return null;
            }
            catch (OperationCanceledException ex)
            {
                throw ex;
            }
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
            string apiUrl = App.Context.ServerAddress + string.Format(Constants.AppAPI.SearchItemsAPI, token, keyword, page, record);
            var apiResponse = await NetWorkUtility.MakeRequestAsync(apiUrl, "GET");
            if (apiResponse.Length > 25)
            {
                try
                {
                    var result = JsonConvert.DeserializeObject<EventResponseModel>(apiResponse);
                    return result.data;
                }
                catch (Exception ex)
                {
                    //mvxLog.Error(ex.ToString());
                    //mvxLog.Error(ex.StackTrace);
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
        public async Task<List<Event>> SearchEventAsync(string token, string keyword, int page, int record, CancellationToken ct)
        {
            try
            {
                string apiUrl = App.Context.ServerAddress + string.Format(Constants.AppAPI.SearchItemsAPI, token, keyword, page, record);
                var apiResponse = await NetWorkUtility.MakeRequestAsync(apiUrl, "GET", ct);
                if (apiResponse.Length > 25)
                {
                    try
                    {
                        var result = JsonConvert.DeserializeObject<EventResponseModel>(apiResponse);
                        return result.data;
                    }
                    catch (Exception ex)
                    {
                        //mvxLog.Error(ex.ToString());
                        //mvxLog.Error(ex.StackTrace);
                    }
                }
                return null;
            }
            catch (OperationCanceledException ex)
            {
                throw ex;
            }
            
        }


        public async Task<Event> GetEventById(string token, int id)
        {
            string apiUrl = App.Context.ServerAddress + string.Format(Constants.AppAPI.GetItemByIdAPI, token, id);
            var apiResponse = await NetWorkUtility.MakeRequestAsync(apiUrl, "GET");
            if (apiResponse.Length > 25)
            {
                try
                {
                    var result = JsonConvert.DeserializeObject<EventResponseModel>(apiResponse);
                    if (result != null && result.data != null && result.data.Count == 1)
                    {
                        return result.data[0];                        
                    }                
                }
                catch (Exception ex)
                {
                    //mvxLog.Error(ex.ToString());
                    //mvxLog.Error(ex.StackTrace);
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
            string apiUrl = App.Context.ServerAddress + string.Format(Constants.AppAPI.GetChatAPI, token, eventID, type);
            var apiResponse = await NetWorkUtility.MakeRequestAsync(apiUrl, "GET");
            if (apiResponse.Length > 25)
            {
                try
                {
                    var result = JsonConvert.DeserializeObject<ListChatMessageResponseModel>(apiResponse);

                    ///parse list file
                    foreach (var d in result.data)
                    {
                        try
                        {
                            var paths = JsonConvert.DeserializeObject<string[]>(d.FileIndex.Trim('"'));
                            if (paths == null || paths.Length == 0) continue;
                            d.Files = paths.Select(p => new ChatPicture { FilePath = App.Context.ServerAddress + p.Replace("\\\\", "/") }).ToList();
                        }
                        catch (Exception ex)
                        {
                            ///Cannot deserialize. API response is not normalized
                            Console.WriteLine(ex);
                            continue;
                        }
                    }

                    return result.data;
                }
                catch (Exception ex)
                {
                    ///Cannot deserialize. API response is not normalized
                    Console.WriteLine(ex);
                }
            }
            return null;
        }

        /// <summary>
        /// Get Chat from Server
        /// </summary>
        /// <param name="token"></param>
        /// <param name="eventID"></param>
        /// <param name="type"></param>
        /// <param name="isNewQuery"></param>
        /// <param name="createTime"></param>
        /// <returns></returns>
        public async Task<List<ChatMessage>> QueryChatAsync(string token, int eventID, int type, bool isNewQuery, DateTime createTime)
        {
            if(createTime.Kind == DateTimeKind.Local)
            {
                var utcdatetime = createTime.ToUniversalTime();
                var estZone = TimeZoneInfo.FindSystemTimeZoneById(Constants.TimeZone.HanoiTime);

                createTime = TimeZoneInfo.ConvertTimeFromUtc(utcdatetime, estZone);
            }

            var createTimestr = string.Format(Constants.DateTimeFormat.DateAndTimeFormatConvert, createTime);

            var api = isNewQuery ? Constants.AppAPI.GetNewChatAPI : Constants.AppAPI.GetOldChatAPI;

            string apiUrl = App.Context.ServerAddress + string.Format(api, token, eventID, type, createTimestr);
            var apiResponse = await NetWorkUtility.MakeRequestAsync(apiUrl, "GET");
            if (apiResponse.Length > 25)
            {
                try
                {
                    var result = JsonConvert.DeserializeObject<ListChatMessageResponseModel>(apiResponse);

                    ///parse list file
                    foreach (var d in result.data)
                    {
                        var paths = JsonConvert.DeserializeObject<string[]>(d.FileIndex.Trim('"'));
                        if (paths == null || paths.Length == 0) continue;
                        d.Files = paths.Select(p => new ChatPicture { FilePath = App.Context.ServerAddress + p.Replace("\\\\", "/") }).ToList();
                    }

                    return result.data;
                }
                catch (Exception ex)
                {
                    //mvxLog.Error(ex.ToString());
                    //mvxLog.Error(ex.StackTrace);
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
            string apiUrl = App.Context.ServerAddress + string.Format(Constants.AppAPI.GetChatAPI, token, eventID, type);
            var apiResponse = NetWorkUtility.MakeRequestSync(apiUrl, "GET");
            if (apiResponse.Length > 25)
            {
                try
                {
                    var result = Utility.DeserializeObject<ListChatMessageResponseModel>(apiResponse);
                    return result.data;
                }
                catch (Exception ex)
                {
                    //mvxLog.Error(ex.ToString());
                    //mvxLog.Error(ex.StackTrace);
                }
            }
            return null;
        }

        /// <summary>
        /// Query Chat in of a event by date
        /// </summary>
        /// <param name="token"></param>
        /// <param name="eventID"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public List<ChatMessage> QueryChat(string token, int eventID, int type, bool isNewQuery, DateTime createTime)
        {
            var createTimestr = string.Format("{0:yyyy/mm/dd hh:mm:ss}", createTime);
            var api = isNewQuery ? Constants.AppAPI.GetNewChatAPI : Constants.AppAPI.GetOldChatAPI;

            string apiUrl = App.Context.ServerAddress + string.Format(api, token, eventID, type, createTimestr);
            var apiResponse = NetWorkUtility.MakeRequestSync(apiUrl, "GET");
            if (apiResponse.Length > 25)
            {
                try
                {
                    var result = JsonConvert.DeserializeObject<ListChatMessageResponseModel>(apiResponse);
                    return result.data;
                }
                catch (Exception ex)
                {
                    //mvxLog.Error(ex.ToString());
                    //mvxLog.Error(ex.StackTrace);
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
        public async Task<ChatSentResponse> SendChatAsync(string token, int eventID, int type, string message, int chat, int belongingTo)
        {
            string apiUrl = App.Context.ServerAddress + string.Format(Constants.AppAPI.SendChatAPI, token, eventID, type, message, 0, string.Empty);
            var apiResponse = await NetWorkUtility.MakeRequestAsync(apiUrl, "POST");
            if (apiResponse.Length > 25)
            {
                try
                {
                    var result = JsonConvert.DeserializeObject<ChatSentResponse>(apiResponse);
                    return result;
                }
                catch (Exception ex)
                {
                    //mvxLog.Error(ex.ToString());
                    //mvxLog.Error(ex.StackTrace);
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
        public async Task<ChatSentResponse> SendChatFileAsync(string token, int eventID, int type, string message, byte[] data, int chat, int belongingTo, string fileName)
        {
            string apiUrl = App.Context.ServerAddress + string.Format(Constants.AppAPI.SendChatAPI, token, eventID, type, message, 0, string.Empty);
            var apiResponse = await NetWorkUtility.SendFile(apiUrl, new List<byte[]>() { data }, new List<string>() { string.Empty });
            if (apiResponse.Length > 25)
            {
                try
                {
                    var result = JsonConvert.DeserializeObject<ChatSentResponse>(apiResponse);
                    return result;
                }
                catch (Exception ex)
                {
                    //mvxLog.Error(ex.ToString());
                    //mvxLog.Error(ex.StackTrace);
                }
            }
            return null;
        }

        public async Task<List<File>> QueryAllFilesAsync(string token, int eventID)
        {

            string apiUrl = App.Context.ServerAddress + string.Format(Constants.AppAPI.GetListFileAPI, token, eventID);
            var apiResponse = await NetWorkUtility.MakeRequestAsync(apiUrl, "GET");
            if (apiResponse.Length > 25)
            {
                try
                {
                    var serializeSettings = new JsonSerializerSettings();
                    serializeSettings.DateFormatString = Constants.DateTimeFormat.DateOnlyFormat;
                    var result = JsonConvert.DeserializeObject<ListFileResponseModel>(apiResponse, serializeSettings);
                    return result.data;
                }
                catch (Exception ex)
                {
                    //mvxLog.Error(ex.ToString());
                    //mvxLog.Error(ex.StackTrace);
                }
            }
            return null;
        }

        /// <summary>
        /// api/get-notification?api_token={0}&page={1}&record={2}
        /// </summary>
        /// <param name="token"></param>
        /// <param name="page"></param>
        /// <param name="recordPerPage"></param>
        /// <returns></returns>
        public async Task<List<Notification>> QueryNotificationAsync(string token, int page, int record)
        {
            string apiUrl = App.Context.ServerAddress + string.Format(Constants.AppAPI.GetNotification, token, page, record);
            var apiResponse = await NetWorkUtility.MakeRequestAsync(apiUrl, "GET");
            if (apiResponse.Length > 25)
            {
                try
                {
                    var result = JsonConvert.DeserializeObject<ListNotificationResponseModel>(apiResponse);
                    return result.data;
                }
                catch (Exception ex)
                {
                    //mvxLog.Error(ex.ToString());
                    //mvxLog.Error(ex.StackTrace);
                }
            }
            return null;
        }

        public async Task<ChatSentResponse> SendChatFileAsync(string token, int eventID, int type, string message, IEnumerable<string> filePaths, int chat, int belongingTo)
        {
            if (string.IsNullOrEmpty(message))
            {
                message = DateTime.Now.ToShortDateString();
            }

            string apiUrl = App.Context.ServerAddress + string.Format(Constants.AppAPI.SendChatAPI, token, eventID, type, message, 0, string.Empty);

            List<byte[]> data = new List<byte[]>();
            List<string> fileNames = new List<string>();
            foreach (var path in filePaths)
            {
                data.Add(System.IO.File.ReadAllBytes(path));
                fileNames.Add(System.IO.Path.GetFileName(path));
            }

            var apiResponse = await NetWorkUtility.SendFile(apiUrl, data, fileNames);
            if (apiResponse.Length > 25)
            {
                try
                {
                    var result = JsonConvert.DeserializeObject<ChatSentResponse>(apiResponse);
                    return result;
                }
                catch (Exception ex)
                {
                    //mvxLog.Error(ex.ToString());
                    //mvxLog.Error(ex.StackTrace);
                }
            }
            return null;
        }
    }
}
