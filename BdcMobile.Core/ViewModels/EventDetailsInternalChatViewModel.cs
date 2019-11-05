using BdcMobile.Core.Models;
using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.Plugin.PictureChooser;
using MvvmCross.ViewModels;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace BdcMobile.Core.ViewModels
{
    public class EventDetailsInternalChatViewModel : MvxNavigationViewModel
    {
        private readonly IMvxPictureChooserTask _pictureChooserTask;
        public MvxObservableCollection<ChatMessage> ChatMessages { get; set; }
        public EventDetailsInternalChatViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService, IMvxPictureChooserTask mvxPictureChooserTask) : base(logProvider, navigationService)
        {
            _pictureChooserTask = mvxPictureChooserTask;
        }

        public override Task Initialize()
        {
            ChatMessages = new MvxObservableCollection<ChatMessage>
            { 
                new ChatMessage { TextContent = "Hello!", IsFromMe = true, Type = ChatType.Text },
                new ChatMessage { TextContent = "Hello!", IsFromMe = false, Type = ChatType.Text },
                new ChatMessage { TextContent = "Hello!", IsFromMe = true, Type = ChatType.Text},
            };

            return base.Initialize();
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
    }
}
