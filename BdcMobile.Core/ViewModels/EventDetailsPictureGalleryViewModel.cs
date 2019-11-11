using BdcMobile.Core.Models;
using BdcMobile.Core.Services.Interfaces;
using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BdcMobile.Core.ViewModels
{
    public class EventDetailsPictureGalleryViewModel : MvxNavigationViewModel<int>
    {
        private readonly IHttpService _networkService;
        public int EventId { get; set; }
        public MvxObservableCollection<File> Files { get; set; }

        public IMvxAsyncCommand<File> OpenDocumentCommand { get; private set; }

        public EventDetailsPictureGalleryViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService, IHttpService networkService) : base(logProvider, navigationService)
        {
            _networkService = networkService;

            OpenDocumentCommand = new MvxAsyncCommand<File>(async (f) => await OpenDocument(f));
        }
        public override void Prepare(int parameter)
        {
            EventId = parameter;
        }

        public override async Task Initialize()
        {
            Files = new MvxObservableCollection<File>();
            await LoadDocuments();
            await base.Initialize();
        }

        
        private async Task LoadDocuments()
        {
            Files = new MvxObservableCollection<File>();
            var listfiles = await _networkService.QueryAllFilesAsync(App.User.api_token, EventId);
            if(listfiles != null && listfiles.Count > 0)
            {
                foreach (var f in listfiles)
                {
                    Files.Add(f);
                }
                await this.RaisePropertyChanged("Files");
            }
        }

        private async Task OpenDocument(File file)
        {
            var index = Files.IndexOf(file);
            await NavigationService.Navigate(typeof(DocumentDetailsViewModel), new ListParameter<File>(Files, index));
        }
    }
}
