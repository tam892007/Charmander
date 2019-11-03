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

        public IMvxAsyncCommand LoadMoreCommand { get; private set; }
        public EventListViewModel(IEventService eventService, IMvxNavigationService mvxNavigationService, IMvxLogProvider mvxLogProvider) : base(mvxLogProvider, mvxNavigationService)
        {
            _eventService = eventService;
            //LoadMoreCommand = new MvxAsyncCommand(LoadMore);
        }

        public ObservableCollection<Event> Events { get; set; }


        public override Task Initialize()
        {
            var token = App.User.api_token;
            RecordPerPage = 5;
            var newEvents = _eventService.QueryEvent(token, null, null, 1, RecordPerPage);            
            if (Events == null)
            {
                Events = new ObservableCollection<Event>();
            }
            foreach (var ev in newEvents)
            {
                Events.Add(ev);
            }
            return base.Initialize();
        }


        private IMvxAsyncCommand _refreshCommand;
        public IMvxAsyncCommand RefreshCommand
            => _refreshCommand ?? (_refreshCommand = new MvxAsyncCommand(ExecuteRefreshCommand));

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
            RecordPerPage += 5;
            var newEvents = _eventService.QueryEvent(token, null, null, 1, RecordPerPage);
            if (Events == null)
            {
                Events = new ObservableCollection<Event>();
            }
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
            var currentItemCount = Events == null? 0: Events.Count;
            var nextpage = currentItemCount / RecordPerPage + 1;
            var newEvents = _eventService.QueryEvent(token, null, null, nextpage, RecordPerPage);
            if (newEvents != null)
            {

                foreach (var ev in newEvents)
                {
                    Events.Add(ev);
                }
            }
            await this.RaisePropertyChanged("Events");
        }
    }
}
