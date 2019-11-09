using BdcMobile.Core.Models;
using MvvmCross.Plugin.Messenger;

namespace BdcMobile.Droid.CloudMessaging
{
    public class NotificationMessage : MvxMessage
    {
        public readonly Notification Notification;
        public NotificationMessage(object sender, Notification notifications) : base(sender)
        {
            Notification = notifications;   
        }
    }
}