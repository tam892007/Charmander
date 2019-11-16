using BdcMobile.Core.Services.Interfaces;
using MvvmCross;
using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System.Threading.Tasks;

namespace BdcMobile.Core.ViewModels
{
    public abstract class BaseViewModel<T> : MvxNavigationViewModel<T>
    {
        public IAppContext AppContext { get; set; }

        public BaseViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService) : base(logProvider, navigationService)
        {
            AppContext = Mvx.IoCProvider.Resolve<IAppContext>();

            BackCommand = new MvxAsyncCommand(async () => await BackCommandTask());
        }

        public IMvxAsyncCommand BackCommand { get; private set; }
        private async Task BackCommandTask()
        {
            await NavigationService.Close(this);
        }

        public abstract override void Prepare(T parameter);

        public virtual bool Validate()
        {
            return true;
        } 
    }
}
