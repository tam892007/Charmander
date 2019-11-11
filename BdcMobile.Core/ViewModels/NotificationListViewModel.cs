using BdcMobile.Core.Commons;
using BdcMobile.Core.Models;
using BdcMobile.Core.Services.Interfaces;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BdcMobile.Core.ViewModels
{
    public class NotificationListViewModel : BaseViewModel<object>
    {
        ICloudMessaging _cloudMessaging;
        private IHttpService _networkService;
        private readonly IMvxLog _mvxLog;
        public NotificationListViewModel(IMvxNavigationService mvxNavigationService, IMvxLogProvider mvxLogProvider, ICloudMessaging cloudMessaging, IHttpService networkService) 
            : base(mvxLogProvider, mvxNavigationService)
        {
            _cloudMessaging = cloudMessaging;
            _networkService = networkService;
            _cloudMessaging.Subscribe(OnReceiveNotification, nameof(NotificationListViewModel));
            _mvxLog = mvxLogProvider.GetLogFor(Constants.AppConfig.LogTag);
            Notifications = new MvxObservableCollection<Notification>();
        }
            

        public override async Task Initialize()
        {
            await LoadNotification();
            await base.Initialize();
        }

        public MvxObservableCollection<Notification> Notifications { get; set; }

        public void OnReceiveNotification(Notification notification)
        {
            Notifications.Add(notification);
            RaisePropertyChanged(nameof(Notifications));
        }

        public override void Prepare(object parameter)
        {
            
        }

        private async Task LoadNotification()
        {
            var token = App.User.api_token;

            _mvxLog.Info("Start Load Notification");
            List<Notification> newNotifications = await _networkService.QueryNotificationAsync(token, 1, 20);


            Notifications = new MvxObservableCollection<Notification>();
            if (newNotifications != null)
            {
                Notifications.AddRange(newNotifications);
            }
            _mvxLog.Info("End Load Notification");
            await this.RaisePropertyChanged(nameof(Notifications));

        }
    }
}
