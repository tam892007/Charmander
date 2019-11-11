using Android.OS;
using Android.Runtime;
using Android.Views;
using BdcMobile.Core.ViewModels;
using MvvmCross.Droid.Support.V4;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using MvvmCross.Platforms.Android.Presenters.Attributes;

namespace BdcMobile.Droid.Views
{
    [MvxFragmentPresentation(FragmentHostViewType = typeof(DocumentDetailsView))]
    [Register(nameof(DocumentContentFullScreenView))]
    public class DocumentContentFullScreenView: MvxFragment<DocumentContentFullScreenViewModel>
    {
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);
            var view = this.BindingInflate(Resource.Layout.DocumentContentFullScreen, null);
            return view;
        }
    }
}
