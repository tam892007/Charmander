using System;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Content.PM;
using Android.OS;
using Android.Support.V7.Widget;
using Android.Util;
using Android.Views;
using Android.Views.InputMethods;
using Android.Widget;
using BdcMobile.Core.ViewModels;
using MvvmCross.Droid.Support.V7.AppCompat;
using static Android.Views.View;
using BdcMobile.Droid.UIListenner;

namespace BdcMobile.Droid.Views
{
    [Activity(Label = "Login", LaunchMode = LaunchMode.SingleTask, Theme = "@style/Theme.Splash")]
    public class LoginView : MvxAppCompatActivity<LoginViewModel>
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.SetTheme(Resource.Style.AppTheme);
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Login);

        }


        //public override bool OnTouchEvent(MotionEvent e)
        //{
        //    var Etusername = FindViewById(Resource.Id.username_input);
        //    InputMethodManager imm = (InputMethodManager)GetSystemService(Context.InputMethodService);
        //    imm.HideSoftInputFromWindow(Etusername.WindowToken, 0);
        //    return base.OnTouchEvent(e);
        //}

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

    }
}