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
    [Register(nameof(PictureChatFullScreenView))]
    public class PictureChatFullScreenView: MvxDialogFragment<PictureChatFullScreenViewModel>
    {
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetStyle(StyleNormal, Resource.Style.Dialog);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);
            var view = this.BindingInflate(Resource.Layout.PictureChatFullScreen, null);
            SetupViewPager(view);
            return view;
        }

        public void SetupViewPager(View view)
        {
            var viewPager = view.FindViewById<ViewPager>(Resource.Id.viewPager);
            var fragments = new List<MvxViewPagerFragmentInfo>();

            var numberOfFiles = ViewModel.ChatMessage.Files.Count;
            for (int i = 0; i< numberOfFiles; i++)
            {
                var data = new Dictionary<string, string>();
                data.Add("ImageUrl", ViewModel.ChatMessage.Files[i].FilePath);
                data.Add("ImageName", $"{i + 1}/{numberOfFiles}");
                var bundle = new MvxBundle(data);
                fragments.Add(new MvxViewPagerFragmentInfo(string.Empty, string.Empty, typeof(PictureGalleryView),
                    new MvxViewModelRequest<PictureGalleryViewModel>(bundle, null)));
            }

            viewPager.Adapter = new MvxCachingFragmentStatePagerAdapter(Activity, ChildFragmentManager, fragments);
            viewPager.SetPageTransformer(true, new ZoomOutPageTransformer());
        }
    }
}