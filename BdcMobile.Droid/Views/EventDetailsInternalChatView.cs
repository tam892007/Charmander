﻿using Android.OS;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Views;
using BdcMobile.Core.ViewModels;
using BdcMobile.Droid.UIControl;
using MvvmCross.Droid.Support.V4;
using MvvmCross.Droid.Support.V7.RecyclerView;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using MvvmCross.Platforms.Android.Presenters.Attributes;

namespace BdcMobile.Droid.Views
{
    [MvxTabLayoutPresentation(TabLayoutResourceId = Resource.Id.tabs, ViewPagerResourceId = Resource.Id.viewpager, Title = "Internal Chat", ActivityHostViewModelType = typeof(EventDetailsViewModel))]
    [Register(nameof(EventDetailsInternalChatView))]
    public class EventDetailsInternalChatView : MvxFragment<EventDetailsInternalChatViewModel>
    {
        private MvxRecyclerView _chatListView;
        private ToEndSmoothScroller _scroller;
        public override void OnCreate(Bundle savedInstanceState)
        {
            
            
            base.OnCreate(savedInstanceState);

            _scroller = new ToEndSmoothScroller(Context);

            ViewModel.PropertyChanged += (o, e) =>
            {
                if (e.PropertyName == nameof(ViewModel.ChatMessages))
                {
                    _scroller.TargetPosition = ViewModel.ChatMessages.Count - 1;
                    _chatListView.GetLayoutManager().StartSmoothScroll(_scroller);
                }
            };
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);
            var view = this.BindingInflate(Resource.Layout.EventDetailsInternalChat, null);

            _chatListView = view.FindViewById<MvxRecyclerView>(Resource.Id.reyclerview_message_list);
            var linearLayoutManager = new LinearLayoutManager(Activity);
            linearLayoutManager.StackFromEnd = true;
            _chatListView.SetLayoutManager(linearLayoutManager);

            return view;
        }

        
    }
}