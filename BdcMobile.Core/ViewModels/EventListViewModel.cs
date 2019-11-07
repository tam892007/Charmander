using BdcMobile.Core.Models;
using BdcMobile.Core.Services.Interfaces;
using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace BdcMobile.Core.ViewModels
{
    public class EventListViewModel : MvxNavigationViewModel
    {
        public static int RecordPerPage { get; set; }
        private readonly IEventService _eventService;


        public EventListViewModel(IEventService eventService, IMvxNavigationService mvxNavigationService, IMvxLogProvider mvxLogProvider) : base(mvxLogProvider, mvxNavigationService)
        {
            _eventService = eventService;
            NavigateToEventDetailsCommand = new MvxAsyncCommand<Event>(async (e) => await NavigateToEventDetails(e));
            LoadMoreCommand = new MvxAsyncCommand(async () => await LoadMore());
            RefreshCommand = new MvxAsyncCommand(async () => await ExecuteRefreshCommand());
            NavigateToNotificationListCommand = new MvxAsyncCommand(async (e) => await NavigateToNotificationList());
        }

        public ObservableCollection<Event> Events { get; set; }


        public IMvxAsyncCommand<Event> NavigateToEventDetailsCommand { get; private set; }
        public IMvxAsyncCommand NavigateToNotificationListCommand { get; private set; }
        public IMvxAsyncCommand LoadMoreCommand { get; private set; }
        public IMvxAsyncCommand RefreshCommand { get; private set; }
        public override async Task Initialize()
        {
           
            var token = App.User.api_token;
            RecordPerPage = 20;
            var newEvents = await _eventService.QueryEventAsync(token, null, null, 1, RecordPerPage);
            if (Events == null)
            {
                Events = new ObservableCollection<Event>();
            }
            foreach (var ev in newEvents)
            {
                Events.Add(ev);
            }
            await RefreshCommand.ExecuteAsync();

            await base.Initialize();
        }


        private bool _isBusy;
        public bool IsBusy
        {
            get => _isBusy;
            set => SetProperty(ref _isBusy, value);
        }
        

        private async Task ExecuteRefreshCommand()
        {
            IsBusy = true;
            // do refresh work here
            var token = App.User.api_token;
            var newEvents = await _eventService.QueryEventAsync(token, null, null, 1, RecordPerPage);            
            Events = new ObservableCollection<Event>();            
            foreach(var ev in newEvents)
            {
                Events.Add(ev);
            }
            
            _ = this.RaisePropertyChanged("Events");
            IsBusy = false;
        }

        private async Task LoadMore()
        {
            if (Events == null)
            {
                Events = new ObservableCollection<Event>();
            }
            var token = App.User.api_token;
            var currentItemCount = Events == null ? 0 : Events.Count;
            var nextpage = currentItemCount / RecordPerPage + 1;
            var newEvents = await _eventService.QueryEventAsync(token, null, null, nextpage, RecordPerPage);
            if (newEvents != null)
            {

                foreach (var ev in newEvents)
                {
                    Events.Add(ev);
                }
            }
            await this.RaisePropertyChanged("Events");
        }
        private async Task NavigateToEventDetails(Event e)
        {
            // Implement your logic here.
            await NavigationService.Navigate<EventDetailsViewModel>();
        }

        private async Task NavigateToNotificationList()
        {
            // Implement your logic here.
            await NavigationService.Navigate<NotificationListViewModel>();
        }
    }
}
