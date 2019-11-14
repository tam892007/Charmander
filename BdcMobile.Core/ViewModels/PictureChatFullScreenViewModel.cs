using BdcMobile.Core.Models;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace BdcMobile.Core.ViewModels
{
    public class PictureChatFullScreenViewModel : MvxViewModel<ChatMessage>
    {
        private IMvxNavigationService _navigationService;
        public IMvxAsyncCommand CloseDialogCommand { get; private set; }
        public PictureChatFullScreenViewModel(IMvxNavigationService mvxNavigationService) : base()
        {
            _navigationService = mvxNavigationService;
            CloseDialogCommand = new MvxAsyncCommand(async () => await _navigationService.Close(this));
        }

        public ChatMessage ChatMessage { get; set; }
        public override void Prepare(ChatMessage parameter)
        {
            ChatMessage = parameter;
        }
    }
}
