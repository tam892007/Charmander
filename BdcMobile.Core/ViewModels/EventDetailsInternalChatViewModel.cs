using BdcMobile.Core.Commons;
using BdcMobile.Core.Models;
using BdcMobile.Core.Services.Interfaces;
using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.Plugin.PictureChooser;
using MvvmCross.ViewModels;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading.Tasks;

namespace BdcMobile.Core.ViewModels
{
    public class EventDetailsInternalChatViewModel : MvxNavigationViewModel<int>
    {
        private readonly IMvxPictureChooserTask _pictureChooserTask;
        public MvxObservableCollection<ChatMessage> ChatMessages { get; set; }
        private readonly IHttpService _networkService;
        public ObservableCollection<ChatMessage> ChatMessages { get; set; }
        public int EventId { get; set; }


        public EventDetailsInternalChatViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService, IMvxPictureChooserTask mvxPictureChooserTask, IHttpService networkService) 
            : base(logProvider, navigationService)
        {
            _pictureChooserTask = mvxPictureChooserTask;
            _networkService = networkService;
        }

        public override async Task Initialize()
        {
            ChatMessages = new ObservableCollection<ChatMessage>();
            var listChat = await _networkService.QueryChatAsync(App.User.api_token, EventId, Constants.ChatType.InternalChat);
            if(listChat != null && listChat.Count > 0)
            {
                foreach(var chat in listChat)
                {
                    ChatMessages.Add(chat);
                }
            }
            await base.Initialize();
        }

        private MvxCommand _takePictureCommand;

        public System.Windows.Input.ICommand TakePictureCommand
        {
            get
            {
                _takePictureCommand = _takePictureCommand ?? new MvxCommand(DoTakePicture);
                return _takePictureCommand;
            }
        }

        private void DoTakePicture()
        {
            _pictureChooserTask.TakePicture(400, 95, OnPicture, () => { });
        }

        private MvxCommand _choosePictureCommand;

        public System.Windows.Input.ICommand ChoosePictureCommand
        {
            get
            {
                _choosePictureCommand = _choosePictureCommand ?? new MvxCommand(DoChoosePicture);
                return _choosePictureCommand;
            }
        }

        private void DoChoosePicture()
        {
            _pictureChooserTask.ChoosePictureFromLibrary(400, 95, OnPicture, () => { });
        }

        private byte[] _bytes;

        public byte[] Bytes
        {
            get { return _bytes; }
            set { _bytes = value; RaisePropertyChanged(() => Bytes); }
        }

        private void OnPicture(Stream pictureStream)
        {
            var memoryStream = new MemoryStream();
            pictureStream.CopyTo(memoryStream);
            ChatMessages.Add(new ChatMessage { PictureContent = memoryStream.ToArray(), IsFromMe = true, Type = ChatType.Picture });

            RaisePropertyChanged("ChatMessages");
        }

        private MvxAsyncCommand _sendTextCommand;
        public MvxAsyncCommand SendTextCommand
        {
            get
            {
                _sendTextCommand = _sendTextCommand ?? new MvxAsyncCommand(SendText);
                return _sendTextCommand;
            }
        }

        private async Task SendText()
        {
            ChatMessages.Add(new ChatMessage { TextContent = "Hello It's Me!", IsFromMe = true, Type = ChatType.Text });
            await RaisePropertyChanged("ChatMessages");
        }

        public override void Prepare(int parameter)
        {
            EventId = parameter;
        }
    }
}
