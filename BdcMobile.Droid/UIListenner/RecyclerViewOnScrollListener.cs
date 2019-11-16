using Android.Support.V7.Widget;
using Android.Util;
using BdcMobile.Core.Commons;
using System;

namespace BdcMobile.Droid.UIListenner
{
    public class RecyclerViewOnScrollListener : RecyclerView.OnScrollListener
    {
        public bool IsLoading { get; set; }
        public delegate void LoadMoreEventHandler(object sender, EventArgs e);

        public int RemainingItemsToTriggerFetch { get; set; } = 3;
        public ScrollDirection Direction { get; set; }
        

        public event LoadMoreEventHandler LoadMoreEvent;

        private LinearLayoutManager LayoutManager;

        public RecyclerViewOnScrollListener(LinearLayoutManager layoutManager)
        {
            LayoutManager = layoutManager;
        }

        public override void OnScrolled(RecyclerView recyclerView, int dx, int dy)
        {
            base.OnScrolled(recyclerView, dx, dy);
            if (!IsLoading)
            {                
                IsLoading = true;                
                var visibleItemCount = recyclerView.ChildCount;
                var totalItemCount = recyclerView.GetAdapter().ItemCount;
                var pastVisiblesItems = LayoutManager.FindFirstVisibleItemPosition();
                if (dy > 0 && Direction == ScrollDirection.DOWN) //check for scroll down
                {
                    if (totalItemCount != 0
                    //&& pastVisiblesItems > 0
                    &&
                    (
                        RemainingItemsToTriggerFetch >= totalItemCount
                        || (visibleItemCount + pastVisiblesItems + RemainingItemsToTriggerFetch) >= totalItemCount
                    ))
                    {
                        LoadMoreEvent?.Invoke(this, null);
                    }
                } else if(dy < 0 && Direction == ScrollDirection.UP)
                {
                    if (pastVisiblesItems < RemainingItemsToTriggerFetch)
                    {
                        LoadMoreEvent?.Invoke(this, null);
                    }
                    
                }
                IsLoading = false;
            } 
        }
    }
}