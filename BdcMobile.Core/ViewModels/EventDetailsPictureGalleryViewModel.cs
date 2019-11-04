using BdcMobile.Core.Models;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BdcMobile.Core.ViewModels
{
    public class EventDetailsPictureGalleryViewModel : MvxNavigationViewModel
    {
        public EventDetailsPictureGalleryViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService) : base(logProvider, navigationService)
        {
        }

        public override Task Initialize()
        {
            return base.Initialize();
        }
    }
}
