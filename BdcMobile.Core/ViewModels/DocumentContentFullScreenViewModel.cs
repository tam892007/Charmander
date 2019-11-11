using MvvmCross.ViewModels;

namespace BdcMobile.Core.ViewModels
{
    public class DocumentContentFullScreenViewModel : MvxViewModel
    {
        public DocumentContentFullScreenViewModel() : base() { }

        private string _imageUrl;
        public string ImageUrl
        {
            get { return _imageUrl; }
            set
            {
                if (_imageUrl != value)
                {
                    SetProperty(ref _imageUrl, value);
                }
            }
        }

        private string _imageName;
        public string ImageName
        {
            get { return _imageName; }
            set
            {
                if (_imageName != value)
                {
                    SetProperty(ref _imageName, value);
                }
            }
        }

        protected override void InitFromBundle(IMvxBundle parameters)
        {
            base.InitFromBundle(parameters);
            ImageName = parameters.Data["ImageName"];
            ImageUrl = parameters.Data["ImageUrl"];
        }
    }
}
