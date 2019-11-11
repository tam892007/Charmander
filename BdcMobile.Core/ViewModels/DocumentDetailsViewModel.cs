using BdcMobile.Core.Models;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System.Collections.Generic;

namespace BdcMobile.Core.ViewModels
{
    public class DocumentDetailsViewModel: MvxViewModel<ListParameter<File>>
    {
        private IMvxNavigationService _navigationService;
        public IMvxAsyncCommand CloseDialogCommand { get; private set; }
        public DocumentDetailsViewModel(IMvxNavigationService mvxNavigationService) : base() 
        {
            _navigationService = mvxNavigationService;
            CloseDialogCommand = new MvxAsyncCommand(async () => await _navigationService.Close(this)); 
        }

        public ICollection<File> Files { get; private set; }

        public int SelectedIndex { get; private set; }
        public override void Prepare(ListParameter<File> parameter)
        {
            Files = parameter.Data;
            SelectedIndex = parameter.SelectedIndex;
        }
    }
}
