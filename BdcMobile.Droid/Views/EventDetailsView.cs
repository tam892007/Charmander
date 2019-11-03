using Android.App;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Support.V4.View;
using Android.Views;
using Android.Widget;
using BdcMobile.Core.ViewModels;
using MvvmCross.Droid.Support.V7.AppCompat;
using MvvmCross.Platforms.Android.Presenters.Attributes;
using static Android.Support.Design.Widget.TabLayout;

namespace BdcMobile.Droid.Views
{
    [Activity(Label = "Event Details")]
    [MvxActivityPresentation]
    public class EventDetailsView: MvxAppCompatActivity<EventDetailsViewModel>, IOnTabSelectedListener
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.EventDetails);
            ViewModel.ShowInitialViewModelsCommand.Execute();
            SetUIForTabs();
        }

        private void SetUIForTabs()
        {
            var tabLayout = FindViewById<TabLayout>(Resource.Id.tabs);

            var tabCustomView = (ImageView)LayoutInflater.Inflate(Resource.Layout.Tab, null);
            tabCustomView.SetImageResource(Resource.Drawable.chat_selected);
            tabLayout.GetTabAt(0).SetCustomView(tabCustomView);

            tabCustomView = (ImageView)LayoutInflater.Inflate(Resource.Layout.Tab, null);
            tabCustomView.SetImageResource(Resource.Drawable.picture);
            tabLayout.GetTabAt(1).SetCustomView(tabCustomView);

            tabLayout.AddOnTabSelectedListener(this);
        }

        public void OnTabReselected(Tab tab)
        {
        }

        public void OnTabSelected(Tab tab)
        {
            int iconId = 0;
            switch (tab.Position)
            {
                case 0: iconId = Resource.Drawable.chat_selected; break;
                case 1: iconId = Resource.Drawable.picture_selected; break;
            }

            var imageView = tab.CustomView.FindViewById<ImageView>(Resource.Id.tabImg);
            imageView.SetImageResource(iconId);
        }

        public void OnTabUnselected(Tab tab)
        {
            int iconId = 0;
            switch (tab.Position)
            {
                case 0: iconId = Resource.Drawable.chat; break;
                case 1: iconId = Resource.Drawable.picture; break;
            }

            var imageView = tab.CustomView.FindViewById<ImageView>(Resource.Id.tabImg);
            imageView.SetImageResource(iconId);
        }
    }
}