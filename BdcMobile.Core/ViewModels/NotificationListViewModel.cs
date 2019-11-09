using BdcMobile.Core.Models;
using BdcMobile.Core.Services.Interfaces;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace BdcMobile.Core.ViewModels
{
    public class NotificationListViewModel : MvxNavigationViewModel
    {
        ICloudMessaging _cloudMessaging;
        public NotificationListViewModel(IMvxNavigationService mvxNavigationService, IMvxLogProvider mvxLogProvider, ICloudMessaging cloudMessaging) : base(mvxLogProvider, mvxNavigationService)
        {
            _cloudMessaging = cloudMessaging;
            _cloudMessaging.Subscribe(OnReceiveNotification);

            Notifications = new MvxObservableCollection<Notification>();
        }

        public MvxObservableCollection<Notification> Notifications { get; set; }

        public void OnReceiveNotification(Notification notification)
        {
            Notifications.Add(notification);
            RaisePropertyChanged(nameof(Notifications));
        }
    }
}
