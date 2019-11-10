namespace BdcMobile.Core.Models
{
    public class Notification
    {
        public string Title { get; set; }

        public string Content { get; set; }

        public NotificationType Type { get; set; }

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
}
