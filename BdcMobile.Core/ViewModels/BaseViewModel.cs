using BdcMobile.Core.Services.Interfaces;
using MvvmCross;
using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace BdcMobile.Core.ViewModels
{
    public abstract class BaseViewModel<T> : MvxNavigationViewModel<T>
    {
        public IAppContext AppContext { get; set; }

        public BaseViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService) : base(logProvider, navigationService)
        {
            AppContext = Mvx.IoCProvider.Resolve<IAppContext>();

            BackCommand = new MvxAsyncCommand(async () => await NavigationService.Close(this));
        }

        public IMvxAsyncCommand BackCommand { get; private set; }

        public abstract override void Prepare(T parameter);

        public virtual bool Validate()
        {
            return true;
        } 
    }
}
