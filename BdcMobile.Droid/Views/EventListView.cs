using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Support.V7.Widget;
using Android.Text;
using Android.Util;
using Android.Views;
using Android.Views.InputMethods;
using Android.Widget;
using BdcMobile.Core.Commons;
using BdcMobile.Core.ViewModels;
using BdcMobile.Droid.UIListenner;
using Java.Lang;
using MvvmCross.Droid.Support.V7.AppCompat;
using MvvmCross.Droid.Support.V7.RecyclerView;
using System;

namespace BdcMobile.Droid.Views
{
    [Activity(Label = "Event List")]
    public class EventListView : MvxAppCompatActivity<EventListViewModel>
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.EventList);
            MvxRecyclerView recyclerView = this.FindViewById<MvxRecyclerView>(Resource.Id.mvxRecyclerView1);
            var onScrollListener = new RecyclerViewOnScrollListener();
            recyclerView.AddOnScrollListener(onScrollListener);
            onScrollListener.LoadMoreEvent += async (object sender, EventArgs e) =>  {
                //Load more stuff here
                await this.ViewModel.LoadMoreCommand.ExecuteAsync();
            };


            var editText = this.FindViewById<EditText>(Resource.Id.searchtextbox);
            //var listView = this.FindViewById<ListView>(Resource.Id.listView1);

            //listView.Adapter = new ArrayAdapter(this, Resource.Layout.ListItem, countries);

            //editText.AfterTextChanged += async (sender, e) =>
            //{
            //    if(e.Editable.Length() > 1)
            //    {
            //        await ViewModel.SearchCommand.ExecuteAsync(editText.Text);
            //    } else 
            //    {
            //        Log.Info(Constants.AppConfig.LogTag, e.Editable.Length() + "");
            //    }
                
            //};
            var customTextWatcher = new CustomTextWatcher();
            editText.AddTextChangedListener(customTextWatcher);

            customTextWatcher.TextChange += async () =>
            {
                await this.ViewModel.SearchCommand.ExecuteAsync();
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
                Log.Error(Constants.AppConfig.LogTag, ex.ToString());
                Log.Error(Constants.AppConfig.LogTag, ex.StackTrace);
            }

            return base.DispatchTouchEvent(ev);
        }
    }

    public class CustomTextWatcher : Java.Lang.Object, ITextWatcher
    {
        public delegate void AfterTextChangeEventHandler();
        public event AfterTextChangeEventHandler TextChange;
        bool considerChange = false;
       
        public void AfterTextChanged(IEditable s)
        {
            if (considerChange)
            {
                TextChange();
            }
            considerChange = !considerChange; //see that boolean value is being changed after if loop
        }

        public void BeforeTextChanged(ICharSequence s, int start, int count, int after)
        {
            
        }

        public new void Dispose()
        {
            base.Dispose();
        }

        public void OnTextChanged(ICharSequence s, int start, int before, int count)
        {
            
        }
    }
}