using BdcMobile.Core.Models;
using BdcMobile.Core.Services.Interfaces;
using FFImageLoading.Transformations;
using FFImageLoading.Work;
using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BdcMobile.Core.ViewModels
{
    public class EventDetailsViewModel : BaseViewModel<Event>
    {
        public int SurveyID { get; set; }
        public string SurveyNo { get; set; }
        public string SurveyDescription { get; set; }
        public string PartnerName { get; set; }
        public string PlaceOfSurvey { get; set; }
        public string TOR { get; set; }
        public string Status { get; set; }
        public string ImageURL { get; set; }
        public int SelectedTabIndex { get; set; }
        private readonly IHttpService _networkService;
        public List<ITransformation> CircleTransformation => new List<ITransformation> { new CircleTransformation() };

        public IMvxAsyncCommand ShowInitialViewModelsCommand { get; private set; }


        public EventDetailsViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService, IHttpService networkService) : base(logProvider, navigationService)
        {
            _networkService = networkService;
            ShowInitialViewModelsCommand = new MvxAsyncCommand(ShowInitialViewModels);
        }

        private async Task ShowInitialViewModels()
        {
            var tasks = new List<Task>();

            if (App.Context.UserRole == UserRole.Employee)
            {
                tasks.Add(NavigationService.Navigate<EventDetailsInternalChatViewModel, int>(SurveyID));
            }

            tasks.Add(NavigationService.Navigate<EventDetailsExternalChatViewModel, int>(SurveyID));
            tasks.Add(NavigationService.Navigate<EventDetailsPictureGalleryViewModel, int>(SurveyID));

            await Task.WhenAll(tasks);
        }

        public override async Task Initialize()
        {
            var token = App.User.api_token;

            ///check if content available. if not, pull from server.
            if (string.IsNullOrEmpty(SurveyDescription) && string.IsNullOrEmpty(Status))
            {
                var evt = await _networkService.GetEventById(token, SurveyID);
                if (evt != null)
                {
                    SurveyNo = evt.SurveyNo;
                    SurveyDescription = evt.SurveyDescription;
                    TOR = evt.TOR;
                    Status = evt.Status;
                    PartnerName = evt.PartnerName;
                    PlaceOfSurvey = evt.PlaceOfSurvey;
                    ImageURL = evt.ImageURL;
                    await RaisePropertyChanged(nameof(SurveyNo));
                    await RaisePropertyChanged(nameof(SurveyDescription));
                    await RaisePropertyChanged(nameof(TOR));
                    await RaisePropertyChanged(nameof(Status));
                    await RaisePropertyChanged(nameof(PartnerName));
                    await RaisePropertyChanged(nameof(PlaceOfSurvey));
                    await RaisePropertyChanged(nameof(ImageURL));
                }
            }
        }

        public override void Prepare(Event parameter)
        {
            SurveyID = parameter.SurveyID;
            SelectedTabIndex = parameter.TabIndex;
            SurveyNo = parameter.SurveyNo;
            SurveyDescription = parameter.SurveyDescription;
            TOR = parameter.TOR;
            Status = parameter.Status;
            PartnerName = parameter.PartnerName;
            PlaceOfSurvey = parameter.PlaceOfSurvey;
            ImageURL = parameter.ImageURL;
        }

        public override void Prepare()
        {

        }
    }
}
