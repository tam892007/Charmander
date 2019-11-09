using Android.App;
using Android.OS;
using Android.Widget;
using BdcMobile.Core.ViewModels;
using BdcMobile.Droid.UIListenner;
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


        }
    }
}