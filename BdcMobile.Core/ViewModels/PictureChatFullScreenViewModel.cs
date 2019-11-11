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

        private string _imageUrl;
        public string ImageUrl
        {
            get { return _imageUrl; }
            set
            {
                if (_imageUrl != value)
                {
                    SetProperty(ref _imageUrl, value);
                }
            }
        }

        public int SelectedIndex { get; private set; }
        public override void Prepare(ChatMessage parameter)
        {
            ImageUrl = parameter.PicturePath.Replace('\\', '/');
        }
    }
}
