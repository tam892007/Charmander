using BdcMobile.Core.Commons;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace BdcMobile.Core.Models
{
    public class Notification
    {
        public string Title { get; set; }

        public string Content { get; set; }

        public string Action { get; set; }

        public NotificationType Type { get; set; }

        public string Created_at { get; set; }

        public int SurveyID { get; set; }
    }

    public enum NotificationType
    {
        NewEvent,
        AssignPIC,
        UpdateEvent,
        NewChat,
        CancelEvent,
        NewFile,
        ApproveEvent,
        InputIncome,
        RequestToCompleteEvent,
        CompleteEvent,
    }

    public class ListNotificationResponseModel
    {
        public List<Notification> data { get; set; }
    }
}
