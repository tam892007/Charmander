using BdcMobile.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BdcMobile.Core.Services.Interfaces
{
    public interface IHttpService
    {
        /// <summary>
        /// Login function with username and password
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        User LoginAsync(User user);

        /// <summary>
        /// Verify user login by token
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        User VerifyUserAsync(string token);

        /// <summary>
        /// Query all event in duration
        /// </summary>
        /// <param name="token"></param>
        /// <param name="fromdate"></param>
        /// <param name="todate"></param>
        /// <returns></returns>
        List<Event> QueryEvent(string token, DateTime? fromdate, DateTime? todate, int currentPage, int recpordPerPage);
    }
}
