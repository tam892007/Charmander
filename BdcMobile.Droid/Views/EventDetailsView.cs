using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V7.Widget;
using Android.Util;
using Android.Views;
using Android.Views.InputMethods;
using Android.Widget;
using BdcMobile.Core.Commons;
using BdcMobile.Core.ViewModels;
using BdcMobile.Droid.Extensions;
using BdcMobile.Droid.UIListenner;
using Java.Lang;
using MvvmCross.Droid.Support.V7.AppCompat;
using MvvmCross.Droid.Support.V7.RecyclerView;
using MvvmCross.Platforms.Android.Presenters.Attributes;
using Plugin.Permissions;
using static Android.Support.Design.Widget.TabLayout;

namespace BdcMobile.Droid.Views
{
    [Activity(Label = "Event Details", WindowSoftInputMode = SoftInput.AdjustPan)]
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
            tabCustomView.SetImageResource(Resource.Drawable.chatexternal);
            tabLayout.GetTabAt(1).SetCustomView(tabCustomView);

            tabCustomView = (ImageView)LayoutInflater.Inflate(Resource.Layout.Tab, null);
            tabCustomView.SetImageResource(Resource.Drawable.picture);
            tabLayout.GetTabAt(2).SetCustomView(tabCustomView);

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
                case 1: iconId = Resource.Drawable.chatexternalselected; break;
                case 2: iconId = Resource.Drawable.picture_selected; break;
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
                case 1: iconId = Resource.Drawable.chatexternal; break;
                case 2: iconId = Resource.Drawable.picture; break;
            }

            var imageView = tab.CustomView.FindViewById<ImageView>(Resource.Id.tabImg);
            imageView.SetImageResource(iconId);
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        public override bool DispatchTouchEvent(MotionEvent ev)
        {
            try
            {
                if (ev.Action == MotionEventActions.Down)
                {
                    View v = CurrentFocus;
                    if (v != null && v.GetType() == typeof(AppCompatEditText))
                    {
                        Rect outRect = new Rect();
                        v.GetGlobalVisibleRect(outRect);
                        if (!outRect.Contains((int)ev.RawX, (int)ev.RawY))
                        {
                            v.ClearFocus();
                            InputMethodManager imm = (InputMethodManager)this.GetSystemService(Context.InputMethodService);
                            imm.HideSoftInputFromWindow(v.WindowToken, 0);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(Constants.AppConfig.LogTag, ex.ToString());
                Log.Error(Constants.AppConfig.LogTag, ex.StackTrace);
            }

            return base.DispatchTouchEvent(ev);
        }
    }
}