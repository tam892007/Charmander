﻿using BdcMobile.Core.Models;
using BdcMobile.Core.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
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
        public List<Event> QueryEvent(string token, DateTime? fromdate, DateTime? todate, int currentPage, int recpordPerPage)
        {
            var listEvents = _httpService.QueryEvent(token, fromdate, todate, currentPage, recpordPerPage);
            return listEvents;

        }

    }
}
