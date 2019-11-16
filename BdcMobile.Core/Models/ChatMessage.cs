using BdcMobile.Core.Commons;
using BdcMobile.Core.Services.Interfaces;
using FFImageLoading.Transformations;
using FFImageLoading.Work;
using MvvmCross;
using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BdcMobile.Core.Models
{
    public class ChatMessage : MvxViewModel
    {
        public IList<ChatPicture> Files { get; set; }

        private int _chatID;

        public int ChatID
        {
            get { return _chatID; }
            set {
                _chatID = value; 
                RaisePropertyChanged(() => SendStatus);
                RaisePropertyChanged(nameof(Time));
                RaisePropertyChanged(nameof(IsSent));
                RaisePropertyChanged(nameof(IsSending));
                RaisePropertyChanged(nameof(IsSendError));
                RaisePropertyChanged(nameof(IsNotSendError));
            }
        }

        public int SurID { get; set; }
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string Content { get; set; }
        public int BelongingTo { get; set; }
        public string FileIndex { get; set; }
        public int Type { get; set; }
        public string Func { get; set; }
        public string target { get; set; }

        [JsonConverter(typeof(InvalidDataFormatJsonConverter))]
        public DateTime? CreateTime { get; set; }

        public string Time 
        {
            get
            {
                if (IsSending)
                {
                    return "Sending...";
                } 
                if (CreateTime != null)
                {
                    return string.Format("{0:HH mm}", CreateTime);
                }
                return "Sending...";
            }
        }

        public List<ITransformation> CircleTransformation => new List<ITransformation> { new CircleTransformation() };
        public bool IsFromMe { get; set; }
        private int _sendStatus;
        /// <summary>
        /// Send Status: 0 - sending; 1 - sent; 2 - error
        /// </summary>
        public int SendStatus
        {
            get
            {
                if (ChatID != 0) _sendStatus = 1;
                return _sendStatus;
            }
            set
            {
                _sendStatus = value;
                RaisePropertyChanged(nameof(Time));
                RaisePropertyChanged(nameof(IsSent));
                RaisePropertyChanged(nameof(IsSending));
                RaisePropertyChanged(nameof(IsSendError));
                RaisePropertyChanged(nameof(IsNotSendError));
            }
        }
        public bool IsSent
        {
            get { return SendStatus == 1; }
        }
        public bool IsSending
        {
            get { return SendStatus == 0; }
        }
        public bool IsSendError
        {
            get
            {
                return SendStatus == 2;
            }
        }
        public bool IsNotSendError
        {
            get
            {
                return !IsSendError;
            }
        }
        public string UserImg { get; set; }
        public string UserImageURL
        {
            get
            {
                if (string.IsNullOrWhiteSpace(UserImg))
                {
                    // TODO: Remove later
                    //return "http://103.47.192.239:81/public/Uploads/HR/EMPLOYEE_IMAGE/6_MR_2019-04-03_07-54-10.jpg";
                    return Constants.AppAPI.IPAPI;
                }
                else
                {
                    return Constants.AppAPI.IPAPI + UserImg;
                }

            }
        }

        public bool IsSinglePicture => Files?.Count == 1;

        public bool IsMultiPicture => Files?.Count > 2;

        public string MultiPictureTextDisplay => $"+{Files?.Count - 2}";

        private MvxAsyncCommand _resendCommand;


        public MvxAsyncCommand ResendCommand
        {
            get
            {
                _resendCommand = _resendCommand ?? new MvxAsyncCommand(async () => await SendChatMessage());
                return _resendCommand;
            }
        }

        private async Task SendChatMessage()
        {
            //var ilog = Mvx.IoCProvider.Resolve<IMvxLogProvider>();
            //var log = ilog.GetLogFor(Constants.AppConfig.LogTag);
            var networkService = Mvx.IoCProvider.Resolve<IHttpService>();
            try
            {
                //log.Info(Constants.AppConfig.LogTag, "Resend: " + Content);

                var token = App.User.api_token;
                SendStatus = 0;
                await RaisePropertyChanged(nameof(Time));
                await RaisePropertyChanged(nameof(IsSendError));                
                ChatSentResponse chat = null;
                if(Files!= null && Files.Count() > 0)
                {
                    var filePaths = Files.Select(x => x.FilePath);
                    chat = await networkService.SendChatFileAsync(token, SurID, Type, Content, filePaths, 0, App.User.ID);
                } else
                {
                    chat = await networkService.SendChatAsync(token, SurID, Type, Content, 0, App.User.ID);
                }
                
                if (chat != null)
                {
                    ChatID = chat.lastID;
                }
                else
                {
                    SendStatus = 2;
                }

                await RaisePropertyChanged(nameof(Content));
                await RaisePropertyChanged(nameof(Time));
                await RaisePropertyChanged(nameof(IsSendError));
                //log.Info(Constants.AppConfig.LogTag, "Sent: " + Content);
            }
            catch (Exception ex)
            {
                //log.Error(Constants.AppConfig.LogTag, ex.ToString());
                //log.Error(Constants.AppConfig.LogTag, ex.StackTrace);
            }
        }
    }

    public class ListChatMessageResponseModel
    {
        public List<ChatMessage> data { get; set; }
    }

    public class ChatSentResponse
    {

        public int lastID { get; set; }
        public string fileIndex { get; set; }
    }
}
