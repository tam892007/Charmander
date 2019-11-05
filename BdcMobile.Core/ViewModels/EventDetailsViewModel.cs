using BdcMobile.Core.Models;
using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BdcMobile.Core.ViewModels
{
    public class EventDetailsViewModel : MvxNavigationViewModel<Event>
    {
        public int EventId { get; set; }
        public IMvxAsyncCommand ShowInitialViewModelsCommand { get; private set; }

        public EventDetailsViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService) : base(logProvider, navigationService)
        {
            ShowInitialViewModelsCommand = new MvxAsyncCommand(ShowInitialViewModels);
        }

        private async Task ShowInitialViewModels()
        {
            await Task.WhenAll(new List<Task>
            {
                NavigationService.Navigate<EventDetailsInternalChatViewModel, int>(EventId),
                NavigationService.Navigate<EventDetailsExternalChatViewModel>(),
                NavigationService.Navigate<EventDetailsPictureGalleryViewModel>(),
            });
        }

        public override void Prepare(Event parameter)
        {
            EventId = parameter.SurveyID;
        }
    }
}
