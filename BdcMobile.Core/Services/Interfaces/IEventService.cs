using BdcMobile.Core.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BdcMobile.Core.Services.Interfaces
{
    public interface IEventService
    {
        /// <summary>
        /// Query all event in duration
        /// </summary>
        /// <param name="token"></param>
        /// <param name="fromdate"></param>
        /// <param name="todate"></param>
        /// <returns></returns>
        Task<List<Event>> QueryEventAsync(string token, DateTime? fromdate, DateTime? todate, int currentPage, int recpordPerPage);


        /// <summary>
        /// Query all event in duration
        /// </summary>
        /// <param name="token"></param>
        /// <param name="fromdate"></param>
        /// <param name="todate"></param>
        /// <returns></returns>
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

        Event GetEventById(int id);
    }
}
