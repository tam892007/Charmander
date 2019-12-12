using BdcMobile.Core.Commons;
using BdcMobile.Core.Models;
using BdcMobile.Core.Services.Interfaces;
using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;

namespace BdcMobile.Core.ViewModels
{
    public class EventListViewModel : BaseViewModel<Event>
    {
        private Event _event;
        public static int RecordPerPage { get; set; }
        public string SearchText { get; set; }
        private readonly IEventService _eventService;
        //private readonly IMvxLog _mvxLog;
        public CancellationTokenSource LoadDatacts { get; set; }

        public EventListViewModel(IEventService eventService, IMvxNavigationService mvxNavigationService, IMvxLogProvider mvxLogProvider) : base(mvxLogProvider, mvxNavigationService)
        {
            _eventService = eventService;

            //_mvxLog = mvxLogProvider.GetLogFor(Constants.AppConfig.LogTag);
            CancelLoadDataCommand = new MvxCommand(()=> {                 
                LoadDatacts?.Cancel(); 
            });
            LoadMoreCommand = new MvxCommand(
                () =>
                {
                    LoadMoreTask = MvxNotifyTask.Create(LoadMore);
                    RaisePropertyChanged(() => LoadMoreTask);
                });
            RefreshCommand = new MvxAsyncCommand(async () => await ExecuteRefreshCommand());
            NavigateToNotificationListCommand = new MvxAsyncCommand(async (e) =>
            {
                await NavigateToNotificationList();
            });

            NavigateToEventDetailsCommand = new MvxAsyncCommand<Event>(async (e) =>
            {
                await NavigateToEventDetails(e);
            });
            SearchCommand = new MvxAsyncCommand(async () =>
            {
                SearchTask = MvxNotifyTask.Create(SearchData());
                
                //await SearchData(e);
            });
            

            //SearchCommand = new MvxAsyncCommand(async () =>
            //{
            //    SearchTask = MvxNotifyTask.Create(SearchData);
            //    await RaisePropertyChanged(() => SearchTask);
            //    //await SearchData();
            //});
            ShowMenuViewModelCommand = new MvxAsyncCommand(async () => await NavigationService.Navigate<MenuViewModel>());
            
        }


        public MvxObservableCollection<Event> Events { get; set; }


        public IMvxAsyncCommand<Event> NavigateToEventDetailsCommand { get; private set; }
        public IMvxAsyncCommand NavigateToNotificationListCommand { get; private set; }

        public IMvxCommand LoadMoreCommand { get; private set; }
        public MvxNotifyTask LoadMoreTask { get; private set; }
        public IMvxCommand CancelLoadDataCommand { get; private set; }

        public IMvxAsyncCommand RefreshCommand { get; private set; }
        public IMvxAsyncCommand SearchCommand { get; private set; }
        public MvxNotifyTask SearchTask { get; private set; }
        public IMvxAsyncCommand ShowMenuViewModelCommand { get; private set; }

        

        public override async Task Initialize()
        {
            RecordPerPage = 20;
            await RefreshCommand.ExecuteAsync();
            await base.Initialize();

            if (_event != null)
            {
                await NavigationService.Navigate<EventDetailsViewModel, Event>(_event);
                _event = null;
            }
        }


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

        private async Task ExecuteRefreshCommand()
        {
            IsBusy = true;
            // do refresh work here
            var token = App.User.api_token;
            //_mvxLog.Info("Start Refresh: " + SearchText);
            var text = SearchText;
            List<Event> newEvents;
            if (!string.IsNullOrWhiteSpace(text))
            {
                newEvents = await _eventService.SearchEventAsync(token, text, 1, RecordPerPage);
            }
            else
            {
                newEvents = await _eventService.QueryEventAsync(token, null, null, 1, RecordPerPage);
            }
            //_mvxLog.Info("End Refresh: " + SearchText);

            Events = new MvxObservableCollection<Event>();
            if (newEvents != null)
            {
                Events.AddRange(newEvents);
            } 

            await this.RaisePropertyChanged("Events");
            IsBusy = false;
        }

        private async Task LoadMore()
        {
            IsLoadingMore = true;
            if (Events == null)
            {
                Events = new MvxObservableCollection<Event>();
            }
            var token = App.User.api_token;
            var currentItemCount = Events == null ? 0 : Events.Count;
            var nextpage = currentItemCount / RecordPerPage + 1;

            //_mvxLog.Info("Start LoadMore: " + SearchText);
            var text = SearchText;
            List<Event> newEvents;
            if (!string.IsNullOrWhiteSpace(text))
            {
                newEvents = await _eventService.SearchEventAsync(token, text, nextpage, RecordPerPage);
            }
            else
            {
                newEvents = await _eventService.QueryEventAsync(token, null, null, nextpage, RecordPerPage);
            }
            //_mvxLog.Info("End LoadMore: " + SearchText);
            if (newEvents != null)
            {
                foreach (var ev in newEvents)
                {
                    Events.Add(ev);
                    await this.RaisePropertyChanged("Events");
                }
            }
            IsLoadingMore = false;
        }
        private async Task NavigateToEventDetails(Event e)
        {
            // Implement your logic here.
            await NavigationService.Navigate<EventDetailsViewModel, Event>(e);
        }

        private async Task NavigateToNotificationList()
        {
            // Implement your logic here.
            await NavigationService.Navigate<NotificationListViewModel>();
        }

        //private async Task SearchData()
        //{

        //    IsBusy = true;
        //    var token = App.User.api_token;

        //    //_mvxLog.Info("Start Search: " + SearchText);
        //    var text = SearchText;
        //    List<Event> newEvents;
        //    if (!string.IsNullOrWhiteSpace(text))
        //    {
        //        newEvents = await _eventService.SearchEventAsync(token, text, 1, RecordPerPage);
        //    }
        //    else
        //    {
        //        newEvents = await _eventService.QueryEventAsync(token, null, null, 1, RecordPerPage);
        //    }

        //    Events = new MvxObservableCollection<Event>();
        //    if (newEvents != null)
        //    {
        //        Events.AddRange(newEvents);
        //    }
        //    //_mvxLog.Info("End Search: " + text);
        //    await this.RaisePropertyChanged("Events");
        //    IsBusy = false;

        //}
        private async Task SearchData()
        {
            //LoadDatacts = new CancellationTokenSource();           

            IsBusy = true;
            var token = App.User.api_token;

            //_mvxLog.Info("Start Search: " + SearchText);
            var text = SearchText;
            List<Event> newEvents;
            try
            {
                if (!string.IsNullOrWhiteSpace(text))
                {
                    newEvents = await _eventService.SearchEventAsync(token, text, 1, RecordPerPage);
                }
                else
                {

                    newEvents = await _eventService.QueryEventAsync(token, null, null, 1, RecordPerPage);

                }
                Events = new MvxObservableCollection<Event>();
                if (newEvents != null)
                {
                    Events.AddRange(newEvents);
                }
                //_mvxLog.Info("End Search: " + text);
                await this.RaisePropertyChanged("Events");
                IsBusy = false;
            }
            catch (OperationCanceledException ex)
            {
                await SearchData();
                return;
            }    
        }


        public override void Prepare(Event e)
        {
            _event = e;
        }
    }
}
