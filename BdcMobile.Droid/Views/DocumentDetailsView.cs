using Android.OS;
using Android.Runtime;
using Android.Support.V4.View;
using Android.Views;
using BdcMobile.Core.ViewModels;
using BdcMobile.Droid.UIListenner;
using MvvmCross.Droid.Support.V4;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using MvvmCross.Platforms.Android.Presenters.Attributes;
using MvvmCross.ViewModels;
using System.Collections.Generic;

namespace BdcMobile.Droid.Views
{
    [MvxDialogFragmentPresentation]
    [Register(nameof(DocumentDetailsView))]
    public class DocumentDetailsView: MvxDialogFragment<DocumentDetailsViewModel>
    {
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetStyle(StyleNormal, Resource.Style.Dialog);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);
            var view = this.BindingInflate(Resource.Layout.DocumentFullScreen, null);
            SetupViewPager(view);
            return view;
        }

        public void SetupViewPager(View view)
        {
            var viewPager = view.FindViewById<ViewPager>(Resource.Id.viewPager);
            var fragments = new List<MvxViewPagerFragmentInfo>();

            foreach (var i in ViewModel.Files)
            {
                var data = new Dictionary<string, string>();
                data.Add("ImageUrl", i.PathToDisplay);
                data.Add("ImageName", i.RealName);
                var bundle = new MvxBundle(data);
                fragments.Add(new MvxViewPagerFragmentInfo(string.Empty, string.Empty, typeof(DocumentContentFullScreenView),
                    new MvxViewModelRequest<DocumentContentFullScreenViewModel>(bundle, null)));
            }

            viewPager.Adapter = new MvxCachingFragmentStatePagerAdapter(Activity, ChildFragmentManager, fragments);
            viewPager.SetPageTransformer(true, new ZoomOutPageTransformer());
            viewPager.CurrentItem = ViewModel.SelectedIndex;
        }
    }
}