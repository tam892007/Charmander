using BdcMobile.Core.Commons;
using BdcMobile.Core.Models;
using BdcMobile.Core.Services.Interfaces;
using MvvmCross.Commands;
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
        private ICommonService _commonService;
        public IMvxAsyncCommand RefreshCommand { get; set; }
        public IMvxCommand LoadMoreCommand { get; private set; }
        public MvxNotifyTask LoadMoreTask { get; private set; }
        private bool _isBusy;
        public bool IsBusy
        {
            get => _isBusy;
            set => SetProperty(ref _isBusy, value);
        }

        private bool _isLoadingMore;
        public bool IsLoadingMore
        {
            get => _isLoadingMore;
            set => SetProperty(ref _isLoadingMore, value);
        }

        public int ItemPerPage = 20;

        public NotificationListViewModel(IMvxNavigationService mvxNavigationService, IMvxLogProvider mvxLogProvider, 
            ICloudMessaging cloudMessaging, IHttpService networkService, ICommonService commonService) 
            : base(mvxLogProvider, mvxNavigationService)
        {
            _cloudMessaging = cloudMessaging;
            _networkService = networkService;
            _commonService = commonService;
            _cloudMessaging.Subscribe(OnReceiveNotification, nameof(NotificationListViewModel));
            _mvxLog = mvxLogProvider.GetLogFor(Constants.AppConfig.LogTag);
            Notifications = new MvxObservableCollection<Notification>();
            OpenNotificationCommand = new MvxAsyncCommand<Notification>(async (n) => await OpenNotification(n));
            RefreshCommand = new MvxAsyncCommand(() => LoadNotification());
            LoadMoreCommand = new MvxCommand(
                () =>
                {
                    LoadMoreTask = MvxNotifyTask.Create(LoadMoreNotification);
                    RaisePropertyChanged(() => LoadMoreTask);
                });
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

        private async Task LoadNotification(int page = 1)
        {
            if(page == 1)
            {
                // first load or refresh load
                IsBusy = true;
            } else
            {
                // load more data.
                IsLoadingMore = true;
            }
            var token = App.User.api_token;

            _mvxLog.Info("Start Load Notification");
            List<Notification> newNotifications = await _networkService.QueryNotificationAsync(token, page, ItemPerPage);


            Notifications = new MvxObservableCollection<Notification>();
            if (newNotifications != null)
            {
                Notifications.AddRange(newNotifications);
            }
            _mvxLog.Info("End Load Notification");
            await this.RaisePropertyChanged(nameof(Notifications));
            IsBusy = false;
            IsLoadingMore = false;
        }

        public IMvxAsyncCommand<Notification> OpenNotificationCommand { get; private set; }

        private async Task OpenNotification(Notification n)
        {
            var url = $"{App.Context.ServerAddress}/quan-ly/vu-viec/{n.Object}";
            switch (n.Type)
            {
                case NotificationType.InternalChatUpdate:
                    await NavigationService.Navigate<EventDetailsViewModel, Event>(new Event { SurveyID = n.Object, TabIndex = 0 }); return;

                case NotificationType.ExternalChatUpdate:
                    await NavigationService.Navigate<EventDetailsViewModel, Event>(new Event { SurveyID = n.Object, TabIndex = 1 }); return;

                case NotificationType.EventTypeUpdate:
                case NotificationType.IncomeDistributing:
                case NotificationType.EstimatedFeeSummary:
                    url += "#tai-chinh";break;

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

            _commonService.OpenBrowser(url);
        }

        private async Task LoadMoreNotification()
        {
            var currentItemCount = Notifications == null ? 0 : Notifications.Count;
            var nextpage = currentItemCount / ItemPerPage + 1;
            await LoadNotification(nextpage);
        }
    }
}
