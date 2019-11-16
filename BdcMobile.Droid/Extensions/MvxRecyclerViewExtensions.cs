using Android.Support.V7.Widget;
using Android.Util;
using BdcMobile.Core.Commons;
using BdcMobile.Droid.UIListenner;
using MvvmCross.Droid.Support.V7.RecyclerView;
using MvvmCross.ViewModels;
using System;
using System.Threading;
using System.Windows.Input;

namespace BdcMobile.Droid.Extensions
{
    public static class MvxRecyclerViewExtensions
    {
        public static void AddOnScrollFetchItemsListener(this MvxRecyclerView recyclerView, LinearLayoutManager linearLayoutManager, Func<MvxNotifyTask> fetchItemsTaskCompletionFunc, Func<ICommand> fetchItemsCommandFunc, ScrollDirection direction = ScrollDirection.DOWN)
        {
            var onScrollListener = new RecyclerViewOnScrollListener(linearLayoutManager);
            onScrollListener.Direction = direction;
            onScrollListener.LoadMoreEvent += (object sender, EventArgs e) =>
            {
                var fetchItemsTaskCompletion = fetchItemsTaskCompletionFunc.Invoke();
                if (fetchItemsTaskCompletion == null || !fetchItemsTaskCompletion.IsNotCompleted)
                {
                    //Log.Info(Constants.AppConfig.LogTag, "Execute searchCommand ");
                    fetchItemsCommandFunc?.Invoke().Execute(null);   
                }
                    
            };
            recyclerView.AddOnScrollListener(onScrollListener);
        }
    }
}