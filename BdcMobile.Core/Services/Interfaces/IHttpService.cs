using BdcMobile.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BdcMobile.Core.Services.Interfaces
{
    public interface IHttpService
    {
        /// <summary>
        /// Login function with username and password
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        Task<User> LoginAsync(User user);

        /// <summary>
        /// Verify user login by token
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<User> VerifyUserAsync(string token);

        /// <summary>
        /// Query all event in duration
        /// </summary>
        /// <param name="token"></param>
        /// <param name="fromdate"></param>
        /// <param name="todate"></param>
        /// <returns></returns>
        Task<List<Event>> QueryEventAsync(string token, DateTime? fromdate, DateTime? todate, int currentPage, int recpordPerPage);

        /// <summary>
        /// Search vu viec
        /// </summary>
        /// <param name="token"></param>
        /// <param name="keyword"></param>
        /// <param name="page"></param>
        /// <param name="record"></param>
        /// <returns></returns>
        Task<List<Event>> SearchEventAsync(string token, string keyword, int page, int record);



        /// <summary>
        /// Query all Chat in of a event Synchronize
        /// </summary>
        /// <param name="token"></param>
        /// <param name="eventID"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        Task<List<ChatMessage>> QueryChatAsync(string token, int eventID, int type);

        /// <summary>
        /// Query all Chat in of a event
        /// </summary>
        /// <param name="token"></param>
        /// <param name="eventID"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        List<ChatMessage> QueryChat(string token, int eventID, int type);

        /// <summary>
        /// send chat message to server
        /// </summary>
        /// <param name="token"></param>
        /// <param name="eventID"></param>
        /// <param name="type"></param>
        /// <param name="message"></param>
        /// <param name="chat"></param>
        /// <param name="belongingTo"></param>
        /// <returns></returns>
        Task<int> SendChatAsync(string token, int eventID, int type, string message, int chat, int belongingTo);

        /// <summary>
        /// Get All file of Event
        /// </summary>
        /// <param name="token"></param>
        /// <param name="eventID"></param>
        /// <returns></returns>
        Task QueryAllFilesAsync(string token, int eventID);

        /// <summary>
        /// Get Notifications
        /// </summary>
        /// <param name="token"></param>
        /// <param name="page"></param>
        /// <param name="recordPerPage"></param>
        /// <returns></returns>
        Task<List<Notification>> QueryNotificationAsync(string token, int page, int recordPerPage);
    }
}
