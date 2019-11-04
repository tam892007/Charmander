using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using MvvmCross.Droid.Support.V7.RecyclerView;
using static Android.Support.V7.Widget.RecyclerView;

namespace BdcMobile.Droid.TemplateSelector
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
                if ((visibleItemCount + pastVisiblesItems) >= totalItemCount - 5)
                {
                    LoadMoreEvent(this, null);
                }
            }
        }
    }
}