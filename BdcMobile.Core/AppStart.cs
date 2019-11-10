using BdcMobile.Core.Models;
using BdcMobile.Core.ViewModels;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System.Threading.Tasks;

namespace BdcMobile.Core
{
    public class AppStart : MvxAppStart
    {
        
        public AppStart(IMvxApplication application, IMvxNavigationService navigationService) : base(application, navigationService)
        {
            
        }

        protected override async Task NavigateToFirstViewModel(object hint = null)
        {
            if (hint is Notification)
            {
                var e = new Event { SurveyID = (hint as Notification).SurveyID };
                await NavigationService.Navigate(typeof(EventDetailsViewModel), e);
            }
            else
            {
                await NavigationService.Navigate<LoginViewModel>();
            }
        }
    }
}
