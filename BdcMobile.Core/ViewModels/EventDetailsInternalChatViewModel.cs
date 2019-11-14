using BdcMobile.Core.Commons;
using BdcMobile.Core.Models;
using BdcMobile.Core.Services.Interfaces;
using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
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
            OpenMessageCommand = new MvxAsyncCommand<ChatMessage>(async (e) => {
                if (e.Files?.Count > 0) await OpenMessage(e);
            });

            await LoadChatMessages(DateTime.Now);
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
            await OnPictureAsync(new List<string> { path });
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
            var paths = await _pictureChooserTask.PickPhotosAsync();
            await OnPictureAsync(paths);
        }

        private byte[] _bytes;

        public byte[] Bytes
        {
            get { return _bytes; }
            set { _bytes = value; RaisePropertyChanged(() => Bytes); }
        }

        private async Task OnPictureAsync(IList<string> paths)
        {
            if (paths == null || paths.Count == 0)
            {
                return;
            }

            var chatmessage = new ChatMessage { Content = Message, IsFromMe = true, CreateTime = DateTime.Now, Files = new List<ChatPicture>() };

            foreach (var path in paths)
            {
                chatmessage.Files.Add(new ChatPicture { FilePath = path });
            }

            await SendChatMessage(chatmessage);

            await RaisePropertyChanged(nameof(ChatMessages));
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
                if (string.IsNullOrWhiteSpace(Message)) return;
                var token = App.User.api_token;
                var chatmessage = new ChatMessage { Content = Message, IsFromMe = true, CreateTime = DateTime.Now };
                ChatMessages.Add(chatmessage);
                Message = string.Empty;
                await RaisePropertyChanged(nameof(Message));
                await RaisePropertyChanged(nameof(ChatMessages));
                var chat = await _networkService.SendChatAsync(token, EventId, Constants.ChatType.InternalChat, chatmessage.Content, 0, App.User.ID);
                chatmessage.ChatID = chat.lastID;
                Log.Info(Constants.AppConfig.LogTag, "Sent:" + chatmessage.Content);
                await RaisePropertyChanged(nameof(ChatMessages));
            }
            catch(Exception ex)
            {
                Log.Error(Constants.AppConfig.LogTag, ex.ToString());
                Log.Error(Constants.AppConfig.LogTag, ex.StackTrace);
            }
        }

        private async Task SendChatMessage(ChatMessage msg)
        {
            try
            {
                var token = App.User.api_token;
                ChatMessages.Add(msg);
                Message = string.Empty;
                await RaisePropertyChanged(nameof(Message)); 
                await RaisePropertyChanged(nameof(ChatMessages));

                var filePaths = msg.Files.Select(x => x.FilePath);
                ////Send to server and handle failure
                var chat = await _networkService.SendChatFileAsync(token, EventId, Constants.ChatType.InternalChat, msg.Content, filePaths, 0, App.User.ID);
                msg.ChatID = chat.lastID;
                msg.FileIndex = chat.fileIndex;
                
                Log.Info(Constants.AppConfig.LogTag, "Sent:" + msg.ChatID);
                await RaisePropertyChanged(nameof(ChatMessages));
            }
            catch (Exception ex)
            {
                Log.Error(Constants.AppConfig.LogTag, ex.ToString());
                Log.Error(Constants.AppConfig.LogTag, ex.StackTrace);
            }
        }

        private async Task LoadChatMessages(DateTime datetime)
        {
            //var listChat = await _networkService.QueryChatAsync(App.User.api_token, EventId, Constants.ChatType.InternalChat, false, datetime);
            var listChat = await _networkService.QueryChatAsync(App.User.api_token, EventId, Constants.ChatType.InternalChat, true, datetime);

            if (listChat != null && listChat.Count > 0)
            {
                foreach (var chat in listChat)
                {
                    chat.IsFromMe = chat.UserID == App.User.ID;                                    
                    ChatMessages.Add(chat);                   
                }
            }
            await RaisePropertyChanged(nameof(ChatMessages));
        }

        public override void Prepare(int parameter)
        {
            EventId = parameter;
        }

        public IMvxAsyncCommand<ChatMessage> OpenMessageCommand { get; private set; }

        private async Task OpenMessage(ChatMessage m)
        {
            await NavigationService.Navigate(typeof(PictureChatFullScreenViewModel), m);
        }
    }
}
