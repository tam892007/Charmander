using Android.Support.V7.Widget;
using MvvmCross.Droid.Support.V7.RecyclerView;
using System;
using static Android.Support.V7.Widget.RecyclerView;

namespace BdcMobile.Droid.UIListenner
{
    class RecyclerViewOnScrollListener : OnScrollListener
    {
        public delegate void LoadMoreEventHandler(object sender, EventArgs e);
        public event LoadMoreEventHandler LoadMoreEvent;

        public override void OnScrolled(RecyclerView recyclerView, int dx, int dy)
        {
            base.OnScrolled(recyclerView, dx, dy);
            var mvxRecyclerView = recyclerView as MvxRecyclerView;
            var  mLayoutManager = recyclerView.GetLayoutManager() as LinearLayoutManager;
            if (dy > 0) //check for scroll down
            {
                var visibleItemCount = recyclerView.ChildCount;
                var pastVisiblesItems = mLayoutManager.FindFirstVisibleItemPosition();
                var totalItemCount = recyclerView.GetAdapter().ItemCount;
                if ((visibleItemCount + pastVisiblesItems) >= totalItemCount)
                {
                    LoadMoreEvent(this, null);
                }
            }
        }
    }
}