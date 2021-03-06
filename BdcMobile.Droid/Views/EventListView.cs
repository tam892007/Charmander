﻿using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Support.V4.View;
using Android.Support.V4.Widget;
using Android.Support.V7.Widget;
using Android.Util;
using Android.Views;
using Android.Views.InputMethods;
using Android.Widget;
using BdcMobile.Core.Commons;
using BdcMobile.Core.ViewModels;
using BdcMobile.Droid.Extensions;
using MvvmCross.Droid.Support.V7.AppCompat;
using MvvmCross.Droid.Support.V7.RecyclerView;
using Exception = System.Exception;

namespace BdcMobile.Droid.Views
{
    [Activity(Label = "Event List")]
    public class EventListView : MvxAppCompatActivity<EventListViewModel>
    {
        public DrawerLayout DrawerLayout { get; set; }

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.EventList);

            var recyclerView = FindViewById<MvxRecyclerView>(Resource.Id.mvxRecyclerView1);
            if (recyclerView != null)
            {
                recyclerView.HasFixedSize = true;
                var layoutManager = new LinearLayoutManager(this);
                recyclerView.SetLayoutManager(layoutManager);
                recyclerView.AddOnScrollFetchItemsListener(layoutManager, () => ViewModel.LoadMoreTask, () => ViewModel.LoadMoreCommand);
            }

            var editText = this.FindViewById<EditText>(Resource.Id.searchtextbox);
            if(editText != null)
            {
                editText.OnTextChangeListener(() => ViewModel.SearchTask, () => ViewModel.SearchCommand, () => ViewModel.CancelLoadDataCommand);
            }

            DrawerLayout = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);

            if (bundle == null)
            {
                ViewModel.ShowMenuViewModelCommand.Execute(null);
            }

            var menuBtn = FindViewById<ImageButton>(Resource.Id.menu);
            menuBtn.Click += delegate {
                DrawerLayout.OpenDrawer(GravityCompat.Start);
            };
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
                //Log.Error(Constants.AppConfig.LogTag, ex.ToString());
                //Log.Error(Constants.AppConfig.LogTag, ex.StackTrace);
            }

            return base.DispatchTouchEvent(ev);
        }

        public override void OnBackPressed()
        {
            //navigate to the screen you want to
        }
    }

    
}