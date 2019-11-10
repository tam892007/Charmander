using BdcMobile.Core.Commons;
using BdcMobile.Core.Models;
using BdcMobile.Core.Services.Interfaces;
using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System;
using System.IO;
using System.Threading.Tasks;

namespace BdcMobile.Core.ViewModels
{
    public class EventDetailsInternalChatViewModel : MvxNavigationViewModel<int>
    {
        private readonly IMediaService _pictureChooserTask;
        public MvxObservableCollection<ChatMessage> ChatMessages { get; set; }
        private readonly IHttpService _networkService;
        public int EventId { get; set; }
        public string Message { get; set; }


        public EventDetailsInternalChatViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService,
            IMediaService mvxPictureChooserTask, IHttpService networkService) 
            : base(logProvider, navigationService)
        {
            _pictureChooserTask = mvxPictureChooserTask;
            _networkService = networkService;
        }

        public override async Task Initialize()
        {
            ChatMessages = new MvxObservableCollection<ChatMessage>();
            await LoadChatMessages();
            await base.Initialize();
        }

        private IMvxAsyncCommand _takePictureCommand;

        public IMvxAsyncCommand TakePictureCommand
        {
            get
            {
                _takePictureCommand = _takePictureCommand ?? new MvxAsyncCommand(DoTakePicture);
                return _takePictureCommand;
            }
        }

        private async Task DoTakePicture()
        {
            var path = await _pictureChooserTask.TakePhotoAsync();
            OnPicture(path);
        }

        private IMvxAsyncCommand _choosePictureCommand;

        public IMvxAsyncCommand ChoosePictureCommand
        {
            get
            {
                _choosePictureCommand = _choosePictureCommand ?? new MvxAsyncCommand(DoChoosePicture);
                return _choosePictureCommand;
            }
        }

        private async Task DoChoosePicture()
        {
            var path = await _pictureChooserTask.PickPhotoAsync();
            OnPicture(path);
        }

        private byte[] _bytes;

        public byte[] Bytes
        {
            get { return _bytes; }
            set { _bytes = value; RaisePropertyChanged(() => Bytes); }
        }

        private void OnPicture(string path)
        {
            if (string.IsNullOrEmpty(path)) return;
            ChatMessages.Add(new ChatMessage { PicturePath = path, IsFromMe = true, CType = ChatType.Picture });
            RaisePropertyChanged(nameof(ChatMessages));
        }

        private MvxAsyncCommand _sendTextCommand;
        public MvxAsyncCommand SendTextCommand
        {
            get
            {
                _sendTextCommand = _sendTextCommand ?? new MvxAsyncCommand(async () => await SendText());
                return _sendTextCommand;
            }
        }        

        private async Task SendText()
        {
            try
            {
                var token = App.User.api_token;
                var chatmessage = new ChatMessage { Content = Message, IsFromMe = true, CType = ChatType.Text };
                ChatMessages.Add(chatmessage);
                Message = string.Empty;
                await RaisePropertyChanged("Message");
                await RaisePropertyChanged("ChatMessages");
                var chatId = await _networkService.SendChatAsync(token, EventId, Constants.ChatType.InternalChat, chatmessage.Content, 0, App.User.ID);
                chatmessage.ChatID = chatId;
                //chatmessage.Content += " sent";
                await RaisePropertyChanged("ChatMessages");
            }
            catch(Exception ex)
            {

            }
        }

        private async Task LoadChatMessages()
        {
            var listChat = await _networkService.QueryChatAsync(App.User.api_token, EventId, Constants.ChatType.InternalChat);
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
