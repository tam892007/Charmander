using BdcMobile.Core.Commons;
using FFImageLoading.Transformations;
using FFImageLoading.Work;
using System.Collections.Generic;

namespace BdcMobile.Core.Models
{
    public class Event
    {
        public int SurveyID { get; set; }

        private string _surveyNo { get; set; }
        public string SurveyNo
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_surveyNo))
                {
                    return "00/00/1234/XX/XX";
                }
                return _surveyNo;
            }
            set
            {
                _surveyNo = value;
            }
        }
        public string PartnerName { get; set; }
        public string TOR { get; set; }
        public string SurveyDescription { get; set; }
        public string PlaceOfSurvey { get; set; }
        public string Inspector { get; set; }
        public string InspectorImage { get; set; }
        public string ImageURL
        {
            get
            {
                if (string.IsNullOrWhiteSpace(InspectorImage))
                {
                    // TODO: Remove later
                    //return "http://103.47.192.239:81/public/Uploads/HR/EMPLOYEE_IMAGE/6_MR_2019-04-03_07-54-10.jpg";
                    return Constants.AppAPI.IPAPI;
                }
                else
                {
                    return Constants.AppAPI.IPAPI + InspectorImage;
                }
                
            }
        }
        public double DownsampleWidth => 200d;
        public List<ITransformation> CircleTransformation => new List<ITransformation> { new CircleTransformation() };
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
