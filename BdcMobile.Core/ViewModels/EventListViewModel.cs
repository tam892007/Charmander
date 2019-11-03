using BdcMobile.Core.Models;
using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BdcMobile.Core.ViewModels
{
    public class EventListViewModel : MvxNavigationViewModel
    {
        public List<Event> Events { get; set; }
        public IMvxAsyncCommand<Event> NavigateToEventDetailsCommand { get; private set; }
        public EventListViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService) : base(logProvider, navigationService)
        {
            NavigateToEventDetailsCommand = new MvxAsyncCommand<Event>(async(e) => await NavigateToEventDetails(e));
        }
        public override Task Initialize()
        {
            Events = new List<Event>
            {
                new Event(),
                new Event(),
                new Event(),
            };

            return base.Initialize();
        }

        private async Task NavigateToEventDetails(Event e)
        {
            // Implement your logic here.
            await NavigationService.Navigate<EventDetailsViewModel>();
        }
    }
}
