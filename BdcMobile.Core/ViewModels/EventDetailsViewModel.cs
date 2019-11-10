using BdcMobile.Core.Models;
using FFImageLoading.Transformations;
using FFImageLoading.Work;
using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BdcMobile.Core.ViewModels
{
    public class EventDetailsViewModel : MvxNavigationViewModel<Event>
    {
        public int SurveyID { get; set; }
        public string SurveyNo { get; set; }
        public string SurveyDescription { get; set; }
        public string PartnerName { get; set; }
        public string PlaceOfSurvey { get; set; }
        public string TOR { get; set; }
        public string Status { get; set; }
        public string ImageURL { get; set; }
        public List<ITransformation> CircleTransformation => new List<ITransformation> { new CircleTransformation() };

        public IMvxAsyncCommand ShowInitialViewModelsCommand { get; private set; }
        

        public EventDetailsViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService) : base(logProvider, navigationService)
        {
            ShowInitialViewModelsCommand = new MvxAsyncCommand(ShowInitialViewModels);
        }

        private async Task ShowInitialViewModels()
        {
            await Task.WhenAll(new List<Task>
            {
                NavigationService.Navigate<EventDetailsInternalChatViewModel, int>(SurveyID),
                NavigationService.Navigate<EventDetailsExternalChatViewModel, int>(SurveyID),
                NavigationService.Navigate<EventDetailsPictureGalleryViewModel, int>(SurveyID),
            });
        }

        public override void Prepare(Event parameter)
        {
            SurveyID = parameter.SurveyID;
            SurveyNo = parameter.SurveyNo;
            SurveyDescription = parameter.SurveyDescription;
            TOR = parameter.TOR;
            Status = parameter.Status;
            PartnerName = parameter.PartnerName;
            PlaceOfSurvey = parameter.PlaceOfSurvey;
            ImageURL = parameter.ImageURL;
        }
    }
}
