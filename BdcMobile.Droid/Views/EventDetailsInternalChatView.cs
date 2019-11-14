using Android.OS;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Views;
using BdcMobile.Core.Commons;
using BdcMobile.Core.ViewModels;
using BdcMobile.Droid.Extensions;
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
        public override void OnCreate(Bundle savedInstanceState)
        {            
            base.OnCreate(savedInstanceState);

            ViewModel.PropertyChanged += (o, e) =>
            {
                if (e.PropertyName == nameof(ViewModel.ChatMessages))
                {
                    var _scroller = new ToEndSmoothScroller(Context);
                    _scroller.TargetPosition = ViewModel.ChatMessages.Count - 1;
                    _chatListView.GetLayoutManager().StartSmoothScroll(_scroller);
                }
            };
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);
            var view = this.BindingInflate(Resource.Layout.EventDetailsInternalChat, null);

            _chatListView = view.FindViewById<MvxRecyclerView>(Resource.Id.reyclerview_internal_message_list);
            if (_chatListView != null)
            {
                var linearLayoutManager = new LinearLayoutManager(Activity);
                linearLayoutManager.StackFromEnd = true;
                _chatListView.SetLayoutManager(linearLayoutManager);
                _chatListView.HasFixedSize = true;
                _chatListView.AddOnScrollFetchItemsListener(linearLayoutManager, () => ViewModel.LoadPreviousMessageTask, () => ViewModel.LoadPreviousMessage, ScrollDirection.UP);
            }


            return view;
        }


        
    }
}