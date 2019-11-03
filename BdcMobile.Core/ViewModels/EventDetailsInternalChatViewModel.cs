using BdcMobile.Core.Models;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BdcMobile.Core.ViewModels
{
    public class EventDetailsInternalChatViewModel : MvxNavigationViewModel
    {
        public List<ChatMessage> ChatMessages { get; set; }
        public EventDetailsInternalChatViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService) : base(logProvider, navigationService)
        {

        }

        public override Task Initialize()
        {
            ChatMessages = new List<ChatMessage>
            {
                new ChatMessage { Content = "Hello!", IsFromMe=true },
                new ChatMessage { Content = "Hello!", IsFromMe=false },
                new ChatMessage { Content = "Hello!", IsFromMe=true },
            };

            return base.Initialize();
        }
    }
}
