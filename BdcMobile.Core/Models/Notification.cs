using System.Collections.Generic;
using System.ComponentModel;

namespace BdcMobile.Core.Models
{
    public class Notification
    {
        public string Title { get; set; }

        public string Content { get; set; }

        public string Action
        {
            get;set;
        }

        public NotificationType Type 
        {
            get
            {
                return GetNotificationTypeFromAction(Action);
            }
        }

        public string Created_at { get; set; }

        public int Object { get; set; }

        private NotificationType GetNotificationTypeFromAction(string action)
        {
            switch (Action)
            {
                case "Cập nhật diễn biến hiện tại": return NotificationType.CurrentProgressUpdate;
                case "Cập nhật loại": return NotificationType.EventTypeUpdate;
                case "Cập nhật ngày gửi": return NotificationType.CreatedDateUpdate;
                case "Cập nhật người yêu cầu": return NotificationType.RequestorUpdate;
                case "Duyệt báo cáo": return NotificationType.ReportApproval;
                case "Hủy vụ việc": return NotificationType.EventCancelled;
                case "Kết thúc": return NotificationType.Complete;
                case "Kết thúc GĐHT": return NotificationType.AssessmentComplete;
                case "Kết thúc vụ việc": return NotificationType.EventComplete;
                case "Không duyệt báo cáo": return NotificationType.ReportReject;
                case "Lưu phí chính thức": return NotificationType.OfficalFeeStoring;
                case "Phân chia % doanh thu": return NotificationType.IncomeDistributing;
                case "Phân công GĐV": return NotificationType.AssigneeUpdate;
                case "Tải lên file báo cáo": return NotificationType.FileUpload;
                case "Tạo": return NotificationType.EventCreated;
                case "Thảo luận nội bộ": return NotificationType.InternalChatUpdate;
                case "Thảo luận với khách hàng": return NotificationType.ExternalChatUpdate;
                case "Thêm diễn biến": return NotificationType.ProgressAdding;
                case "Tổng hợp phí dự kiến": return NotificationType.EstimatedFeeSummary;
                case "Xóa file báo cáo": return NotificationType.FileDeletion;
                case "Xuất hóa đơn": return NotificationType.Billing;
                case "Yêu cầu kết thúc": return NotificationType.RequestToComplete;
                default: return NotificationType.EventCreated;
            }
        }

        public string GetWebUrl(string baseWebUrl)
        {
            var url = $"{baseWebUrl}/quan-ly/vu-viec/{Object}";
            switch (Type)
            {
                case NotificationType.InternalChatUpdate:
                    url += "#thao-luan-noi-bo";break;

                case NotificationType.ExternalChatUpdate:
                    url += "#thao-luan-voi-khach-hang"; break;

                case NotificationType.EventTypeUpdate:
                case NotificationType.IncomeDistributing:
                case NotificationType.EstimatedFeeSummary:
                    url += "#tai-chinh"; break;

                case NotificationType.ReportApproval:
                case NotificationType.ReportReject:
                case NotificationType.AssessmentComplete:
                case NotificationType.FileUpload:
                    url += "#bao-cao"; break;

                case NotificationType.Complete:
                case NotificationType.EventComplete:
                case NotificationType.RequestToComplete:
                    url += "#tong-hop"; break;

                case NotificationType.AssigneeUpdate:
                    url += "#phan-cong"; break;

                case NotificationType.ProgressAdding:
                    url += "#dien-bien"; break;
            }

            return url;
        }
    }

    public enum NotificationType
    {
        [Description("Cập nhật diễn biến hiện tại")]
        CurrentProgressUpdate,
        [Description("Cập nhật loại")]
        EventTypeUpdate,
        [Description("Cập nhật ngày gửi")]
        CreatedDateUpdate,
        [Description("Cập nhật người yêu cầu")]
        RequestorUpdate,
        [Description("Duyệt báo cáo")]
        ReportApproval,
        [Description("Hủy vụ việc")]
        EventCancelled,
        [Description("Kết thúc")]
        Complete,
        [Description("Kết thúc GĐHT")]
        AssessmentComplete,
        [Description("Kết thúc vụ việc")]
        EventComplete,
        [Description("Không duyệt báo cáo")]
        ReportReject,
        [Description("Lưu phí chính thức")]
        OfficalFeeStoring,
        [Description("Phân chia % doanh thu")]
        IncomeDistributing,
        [Description("Phân công GĐV")]
        AssigneeUpdate,
        [Description("Tải lên file báo cáo")]
        FileUpload,
        [Description("Tạo")]
        EventCreated,
        [Description("Thảo luận nội bộ")]
        InternalChatUpdate,
        [Description("Thảo luận với khách hàng")]
        ExternalChatUpdate,
        [Description("Thêm diễn biến")]
        ProgressAdding,
        [Description("Tổng hợp phí dự kiến")]
        EstimatedFeeSummary,
        [Description("Xóa file báo cáo")]
        FileDeletion,
        [Description("Xuất hóa đơn")]
        Billing,
        [Description("Yêu cầu kết thúc")]
        RequestToComplete,
    }


    public class ListNotificationResponseModel
    {
        public List<Notification> data { get; set; }
    }
}
