using BdcMobile.Core.Commons;
using BdcMobile.Core.Models;
using BdcMobile.Core.Services.Interfaces;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System.Threading.Tasks;

namespace BdcMobile.Core.ViewModels
{
    public class EventDetailsExternalChatViewModel : MvxNavigationViewModel<int>
    {
        public MvxObservableCollection<ChatMessage> ChatMessages { get; set; }
        private readonly IHttpService _networkService;
        public int EventId { get; set; }
        public string Message { get; set; }

        public EventDetailsExternalChatViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService, IHttpService networkService) : base(logProvider, navigationService)
        {
            _networkService = networkService;

        }
        public override async Task Initialize()
        {
            ChatMessages = new MvxObservableCollection<ChatMessage>();
            await LoadChatMessages();
            await base.Initialize();
        }


        private async Task LoadChatMessages()
        {
            var listChat = await _networkService.QueryChatAsync(App.User.api_token, EventId, Constants.ChatType.ExternalChat);
            if (listChat != null && listChat.Count > 0)
            {
                foreach (var chat in listChat)
                {
                    chat.IsFromMe = chat.UserID == App.User.ID;
                    ChatMessages.Add(chat);
                }
            }
            await RaisePropertyChanged("ChatMessages");
        }

        public override void Prepare(int parameter)
        {
            EventId = parameter;
        }
    }
}
