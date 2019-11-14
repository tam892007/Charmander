using BdcMobile.Core.Commons;
using BdcMobile.Core.Models;
using BdcMobile.Core.Services.Interfaces;
using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace BdcMobile.Core.ViewModels
{
    public class EventDetailsExternalChatViewModel : MvxNavigationViewModel<int>
    {
        public MvxObservableCollection<ChatMessage> ChatMessages { get; set; }
        private readonly IHttpService _networkService;
        public int EventId { get; set; }
        public string Message { get; set; }
        private readonly IMediaService _pictureChooserTask;
        
        public EventDetailsExternalChatViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService,
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
                if (e.CType == ChatType.Picture) await OpenMessage(e);
            });

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
            await OnPictureAsync(path);
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
            await OnPictureAsync(path);
        }

        private byte[] _bytes;

        public byte[] Bytes
        {
            get { return _bytes; }
            set { _bytes = value; RaisePropertyChanged(() => Bytes); }
        }

        private async Task OnPictureAsync(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                return;
            }

            string filename = Path.GetFileName(path);
            var ms = new MemoryStream();
            using (FileStream file = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                byte[] bytes = new byte[file.Length];
                file.Read(bytes, 0, (int)file.Length);
                ms.Write(bytes, 0, (int)file.Length);
            }
            var data = ms.ToArray();
            await SendFile(data, filename);

            await RaisePropertyChanged(nameof(ChatMessages));
        }
        private async Task OnPictureAsync(Stream pictureStream)
        {
            var memoryStream = new MemoryStream();
            pictureStream.CopyTo(memoryStream);

            var data = memoryStream.ToArray();
            await SendFile(data);

            await RaisePropertyChanged(nameof(ChatMessages));
        }



      /// <summary>
      /// ////////////////////////////////////////////////////////////
      /// </summary>
      /// <returns></returns>



        

        

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
                var chatmessage = new ChatMessage { Content = Message, IsFromMe = true, CType = ChatType.Text, CreateTime = DateTime.Now };
                ChatMessages.Add(chatmessage);
                Message = string.Empty;
                await RaisePropertyChanged(nameof(Message));
                await RaisePropertyChanged(nameof(ChatMessages));
                var chat = await _networkService.SendChatAsync(token, EventId, Constants.ChatType.ExternalChat, chatmessage.Content, 0, App.User.ID);
                chatmessage.ChatID = chat.lastID;
                //chatmessage.Content += " sent";
                await RaisePropertyChanged(nameof(ChatMessages));
            }
            catch (Exception ex)
            {
                Log.Error(Constants.AppConfig.LogTag, ex.ToString());
                Log.Error(Constants.AppConfig.LogTag, ex.StackTrace);
            }
        }

        private async Task SendFile(byte[] data, string filename = null)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(filename)) filename = "untitled.png";
                if (string.IsNullOrWhiteSpace(Message)) Message = filename;
                var token = App.User.api_token;
                var chatmessage = new ChatMessage { Content = Message, IsFromMe = true, PictureContent = data, CType = ChatType.Picture, CreateTime = DateTime.Now };
                ChatMessages.Add(chatmessage);
                Message = string.Empty;
                await RaisePropertyChanged(nameof(Message));
                await RaisePropertyChanged(nameof(ChatMessages));
                var chat = await _networkService.SendChatFileAsync(token, EventId, Constants.ChatType.ExternalChat, chatmessage.Content, data, 0, App.User.ID, filename);
                chatmessage.ChatID = chat.lastID;
                chatmessage.FileIndex = chat.fileIndex;
                chatmessage.PicturePath = Constants.AppAPI.IPAPI + chatmessage.FileIndex.Replace("[\"", "").Replace("\"]", "").Replace("\\\\", "\\");

                await RaisePropertyChanged(nameof(ChatMessages));
            }
            catch (Exception ex)
            {
                Log.Error(Constants.AppConfig.LogTag, ex.ToString());
                Log.Error(Constants.AppConfig.LogTag, ex.StackTrace);
            }
        }

        private async Task LoadChatMessages(DateTime? datetime = null)
        {
            List<ChatMessage> listChat;
            if (datetime == null)
            {
                listChat = await _networkService.QueryChatAsync(App.User.api_token, EventId, Constants.ChatType.ExternalChat);
            }
            else
            {
                listChat = await _networkService.QueryChatAsync(App.User.api_token, EventId, Constants.ChatType.ExternalChat, true, datetime.Value);
            }
           
            

            if (listChat != null && listChat.Count > 0)
            {
                foreach (var chat in listChat)
                {
                    chat.IsFromMe = chat.UserID == App.User.ID;
                    if (chat.CreateTime != null)
                    {
                        //TimeZoneInfo serverZone = TimeZoneInfo.FindSystemTimeZoneById(Constants.TimeZoneId.HanoiTime);
                        //chat.ClientDateTime = TimeZoneInfo(chat.CreateTime.Value, serverZone);
                    }
                    if (!string.IsNullOrWhiteSpace(chat.FileIndex) && chat.FileIndex != "[]")
                    {
                        chat.CType = ChatType.Picture;
                        chat.PicturePath = Constants.AppAPI.IPAPI + chat.FileIndex.Replace("[\"", "").Replace("\"]", "").Replace("\\\\", "\\");
                    }
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
