using BdcMobile.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
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
        Task<User> LoginAsync(User user, CancellationToken token);

        /// <summary>
        /// Verify user login by token
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<User> VerifyUserAsync(string token);

        /// <summary>
        /// Logout
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        bool Logout(string apiToken);

        /// <summary>
        /// Query all event in duration
        /// </summary>
        /// <param name="token"></param>
        /// <param name="fromdate"></param>
        /// <param name="todate"></param>
        /// <returns></returns>
        Task<List<Event>> QueryEventAsync(string token, DateTime? fromdate, DateTime? todate, int currentPage, int recpordPerPage);
        Task<List<Event>> QueryEventAsync(string token, DateTime? fromdate, DateTime? todate, int currentPage, int recpordPerPage, CancellationToken ct);
        /// <summary>
        /// Search vu viec
        /// </summary>
        /// <param name="token"></param>
        /// <param name="keyword"></param>
        /// <param name="page"></param>
        /// <param name="record"></param>
        /// <returns></returns>
        Task<List<Event>> SearchEventAsync(string token, string keyword, int page, int record);
        Task<List<Event>> SearchEventAsync(string token, string keyword, int page, int record, CancellationToken ct);

        Task<Event> GetEventById(string token, int id);

        /// <summary>
        /// Query all Chat in of a event Synchronize
        /// </summary>
        /// <param name="token"></param>
        /// <param name="eventID"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        Task<List<ChatMessage>> QueryChatAsync(string token, int eventID, int type);


        /// <summary>
        /// Query Chat in of a event by date
        /// </summary>
        /// <param name="token"></param>
        /// <param name="eventID"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        Task<List<ChatMessage>> QueryChatAsync(string token, int eventID, int type, bool isNewQuery, DateTime createTime);

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
        Task<ChatSentResponse> SendChatAsync(string token, int eventID, int type, string message, int chat, int belongingTo);

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
        Task<ChatSentResponse> SendChatFileAsync(string token, int eventID, int type, string message, byte[] data, int chat, int belongingTo, string filename);

        Task<ChatSentResponse> SendChatFileAsync(string token, int eventID, int type, string message, IEnumerable<string>filePaths, int chat, int belongingTo);

        /// <summary>
        /// Get All file of Event
        /// </summary>
        /// <param name="token"></param>
        /// <param name="eventID"></param>
        /// <returns></returns>
        Task<List<File>> QueryAllFilesAsync(string token, int eventID);

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
