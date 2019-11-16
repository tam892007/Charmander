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
using System.Timers;


namespace BdcMobile.Core.ViewModels
{
    public class EventDetailsExternalChatViewModel : MvxNavigationViewModel<int>
    {
        public DateTime BeginTime { get; set; }
        public DateTime EndTime { get; set; }

        public MvxObservableCollection<ChatMessage> ChatMessages { get; set; }
        private readonly IHttpService _networkService;
        public int EventId { get; set; }
        public string Message { get; set; }
        private readonly IMediaService _pictureChooserTask;

        public MvxCommand LoadPreviousMessage { get; set; }
        public MvxNotifyTask LoadPreviousMessageTask { get; set; }
        public MvxAsyncCommand LoadNewMessage { get; set; }
        public MvxNotifyTask LoadNewMessageTask { get; set; }

        public Timer timer { get; set; }
        public bool IsScrollingTop { get; set; }

        public EventDetailsExternalChatViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService,
            IMediaService mvxPictureChooserTask, IHttpService networkService)
            : base(logProvider, navigationService)
        {
            _pictureChooserTask = mvxPictureChooserTask;
            _networkService = networkService;
            LoadPreviousMessage = new MvxCommand(() =>
            {
                LoadPreviousMessageTask = MvxNotifyTask.Create(LoadPreviousChatMessages());
                RaisePropertyChanged(() => LoadPreviousMessageTask);

            });

            LoadNewMessage = new MvxAsyncCommand(async () =>
            {
                LoadNewMessageTask = MvxNotifyTask.Create(LoadLatestChatMessages());
                await RaisePropertyChanged(() => LoadPreviousMessageTask);

            });

        }

        public override async Task Initialize()
        {
            ChatMessages = new MvxObservableCollection<ChatMessage>();
            OpenMessageCommand = new MvxAsyncCommand<ChatMessage>(async (e) => {
                if (e.Files?.Count > 0) await OpenMessage(e);
            });

            BeginTime = EndTime = DateTime.Now;
            if (timer == null)
            {
                timer = new Timer(10000);
                timer.Elapsed += async (sender, e) =>
                {
                    if (LoadNewMessageTask == null || LoadNewMessageTask.IsCompleted)
                    {
                        await LoadNewMessage.ExecuteAsync();
                    }

                };
            }


            timer.Start();
            await LoadChatMessages();
            await base.Initialize();
        }
        public override void ViewAppeared()
        {
            Log.Info("ViewDisappearing");
            if (timer != null) timer.Start();
            base.ViewAppeared();
        }


        public override void ViewDisappearing()
        {
            Log.Info("ViewDisappearing");
            if (timer != null) timer.Stop();
            base.ViewDisappearing();
        }

        public override void ViewDisappeared()
        {
            Log.Info("ViewDisappeared");

            base.ViewDisappeared();
        }

        public override void ViewDestroy(bool viewFinishing = true)
        {
            Log.Info("ViewDestroy");
            if (timer != null) timer.Dispose();
            base.ViewDestroy(viewFinishing);
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

            var chatmessage = new ChatMessage()
            {
                Content = Message,
                SurID = EventId,
                Type = Constants.ChatType.ExternalChat,
                IsFromMe = true,
                CreateTime = DateTime.Now,
                Files = new List<ChatPicture>()
            };

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

        public bool RequestScroll { get; set; }

        private async Task SendText()
        {
            try
            {
                IsScrollingTop = false;
                if (string.IsNullOrWhiteSpace(Message)) return;
                var token = App.User.api_token;
                var msg = new ChatMessage()
                {
                    Content = Message,
                    IsFromMe = true,
                    CreateTime = DateTime.Now,
                    SurID = EventId,
                    Type = Constants.ChatType.ExternalChat,
                    SendStatus = 0
                };
                ChatMessages.Add(msg);
                Message = string.Empty;
                await RaisePropertyChanged(nameof(Message));
                await RaisePropertyChanged(nameof(ChatMessages));
                await RaisePropertyChanged(nameof(RequestScroll));
                var chat = await _networkService.SendChatAsync(token, msg.SurID, msg.Type, msg.Content, 0, App.User.ID);
                if (chat != null)
                {
                    msg.ChatID = chat.lastID;
                }
                else
                {
                    msg.SendStatus = 2;
                }
                await RaisePropertyChanged(nameof(ChatMessages));
                Log.Info(Constants.AppConfig.LogTag, "Sent: " + msg.Content);
            }
            catch (Exception ex)
            {
                Log.Error(Constants.AppConfig.LogTag, ex.ToString());
                Log.Error(Constants.AppConfig.LogTag, ex.StackTrace);
            }
        }

        private async Task SendChatMessage(ChatMessage msg)
        {
            try
            {
                IsScrollingTop = false;

                var token = App.User.api_token;







                ChatMessages.Add(msg);
                Message = string.Empty;
                await RaisePropertyChanged(nameof(Message));
                await RaisePropertyChanged(nameof(ChatMessages));
                await RaisePropertyChanged(nameof(RequestScroll));



                var filePaths = msg.Files.Select(x => x.FilePath);
                ////Send to server and handle failure
                var chat = await _networkService.SendChatFileAsync(token, msg.SurID, msg.Type, msg.Content, filePaths, 0, App.User.ID);
                
                if (chat != null)
                {
                    msg.ChatID = chat.lastID;
                    msg.FileIndex = chat.fileIndex;
                }
                else
                {
                    msg.SendStatus = 2;
                }

                Log.Info(Constants.AppConfig.LogTag, "Sent:" + msg.ChatID);
            }
            catch (Exception ex)
            {
                Log.Error(Constants.AppConfig.LogTag, ex.ToString());
                Log.Error(Constants.AppConfig.LogTag, ex.StackTrace);
            }
        }

        /// <summary>
        /// First Loading Data
        /// </summary>
        /// <returns></returns>
        private async Task LoadChatMessages()
        {
            EndTime = DateTime.Now;
            List<ChatMessage> listChat = await _networkService.QueryChatAsync(App.User.api_token, EventId, Constants.ChatType.ExternalChat);
            if (listChat != null && listChat.Count > 0)
            {
                listChat.Reverse();
                foreach (var chat in listChat)
                {
                    chat.IsFromMe = chat.UserID == App.User.ID;
                    if (chat.CreateTime != DateTime.MinValue && BeginTime > chat.CreateTime) BeginTime = chat.CreateTime.Value;
                    ChatMessages.Add(chat);
                }
            }

            await RaisePropertyChanged(nameof(ChatMessages));
            await RaisePropertyChanged(nameof(RequestScroll));
        }

        private async Task LoadPreviousChatMessages()
        {
            await LoadChatMessages(BeginTime, false);
        }


        private async Task LoadLatestChatMessages()
        {
            await LoadChatMessages(EndTime, true);
        }

        private async Task LoadChatMessages(DateTime datetime, bool isNewChatQuery)
        {
            if (isNewChatQuery)
            {
                EndTime = DateTime.Now;
            }
            else
            {
                IsScrollingTop = true;
            }

            List<ChatMessage> listChat = await _networkService.QueryChatAsync(App.User.api_token, EventId, Constants.ChatType.ExternalChat, isNewChatQuery, datetime);

            var hasNewChat = false;
            if (listChat != null && listChat.Count > 0)
            {
                if (isNewChatQuery) listChat.Reverse();
                foreach (var chat in listChat)
                {
                    chat.IsFromMe = chat.UserID == App.User.ID;
                    if (chat.CreateTime != DateTime.MinValue && BeginTime > chat.CreateTime) BeginTime = chat.CreateTime.Value;
                    if (isNewChatQuery)
                    {
                        var existed = ChatMessages.Any(c => c.ChatID == chat.ChatID);
                        if (!existed)
                        {
                            hasNewChat = true;
                            ChatMessages.Add(chat);
                        }
                    }
                    else
                    {
                        hasNewChat = true;
                        ChatMessages.Insert(0, chat);
                    }
                }
            }

            if (hasNewChat) await RaisePropertyChanged(nameof(ChatMessages));
            if (hasNewChat && isNewChatQuery && !IsScrollingTop) await RaisePropertyChanged(nameof(RequestScroll));
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
