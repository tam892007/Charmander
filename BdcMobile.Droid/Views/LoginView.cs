using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Graphics;
using Android.OS;
using Android.Support.V7.Widget;
using Android.Util;
using Android.Views;
using Android.Views.InputMethods;
using BdcMobile.Core.ViewModels;
using MvvmCross.Droid.Support.V7.AppCompat;
using MvvmCross.Platforms.Android.Presenters.Attributes;
using System;

namespace BdcMobile.Droid.Views
{
    [Activity(Label = "Login", LaunchMode = LaunchMode.SingleTask, NoHistory = true, ExcludeFromRecents =true)]
    [MvxActivityPresentation]
    public class LoginView : MvxAppCompatActivity<LoginViewModel>
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Login);
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
            catch(Exception ex)
            {
                Log.Error("BcdMobile", ex.ToString());
            }
            
            return base.DispatchTouchEvent(ev);
        }

        public override void OnBackPressed()
        {
            //navigate to the screen you want to
        }
    }
}