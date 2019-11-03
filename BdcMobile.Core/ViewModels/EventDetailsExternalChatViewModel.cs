using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace BdcMobile.Core.ViewModels
{
    public class EventDetailsExternalChatViewModel : MvxNavigationViewModel
    {
        public EventDetailsExternalChatViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService) : base(logProvider, navigationService)
        {

        }
    }
}
