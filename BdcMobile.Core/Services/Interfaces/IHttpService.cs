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
    }
}
