using BdcMobile.Core.Models;
using BdcMobile.Core.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BdcMobile.Core.Services.Implementations
{
    public class EventService : IEventService
    {
        private IHttpService _httpService;

        public EventService(IHttpService httpService)
        {
            _httpService = httpService;
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
            var listEvents = await _httpService.QueryEventAsync(token, fromdate, todate, currentPage, recpordPerPage);
            return listEvents;

        }
        public async Task<List<Event>> QueryEventAsync(string token, DateTime? fromdate, DateTime? todate, int currentPage, int recpordPerPage, CancellationToken ct)
        {
            var listEvents = await _httpService.QueryEventAsync(token, fromdate, todate, currentPage, recpordPerPage, ct);
            return listEvents;

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
            var listEvents = await _httpService.SearchEventAsync(token, keyword, page, record);
            return listEvents;
        }

        public async Task<Event> GetEventById(string token, int id)
        {
            var evt = await _httpService.GetEventById(token, id);
            return evt;
        }
    }
}
