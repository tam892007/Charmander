using BdcMobile.Core.Models;
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
        public List<ITransformation> CircleTransformation => new List<ITransformation> { new CircleTransformation() };

        public IMvxAsyncCommand ShowInitialViewModelsCommand { get; private set; }
        

        public EventDetailsViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService) : base(logProvider, navigationService)
        {
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

        }

        public override void Prepare(Event parameter)
        {
            SurveyID = parameter.SurveyID;
            SelectedTabIndex = parameter.TabIndex;
            //SurveyNo = parameter.SurveyNo;
            //SurveyDescription = parameter.SurveyDescription;
            //TOR = parameter.TOR;
            //Status = parameter.Status;
            //PartnerName = parameter.PartnerName;
            //PlaceOfSurvey = parameter.PlaceOfSurvey;
            //ImageURL = parameter.ImageURL;
        }

        public override void Prepare()
        {

        }
    }
}
