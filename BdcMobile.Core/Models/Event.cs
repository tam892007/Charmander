using System;
using System.Collections.Generic;

namespace BdcMobile.Core.Models
{
    public class Event
    {
        public string SurveyID { get; set; }
        public string SurveyNo { get; set; }
        public string PartnerName { get; set; }
        public string TOR { get; set; }
        public string SurveyDescription { get; set; }
        public string PlaceOfSurvey { get; set; }
        public string Inspector { get; set; }
        public string InspectorImage { get; set; }
        public string SurveyStatus { get; set; }
        public string Status { get; set; }
        public string CreateTime { get; set; }
        public string DepartmentName { get; set; }
        public string SurveyClosingDate { get; set; }
    }
    public class EventResponseModel
    {        
        public List<Event> data { get; set; }
    }
}
