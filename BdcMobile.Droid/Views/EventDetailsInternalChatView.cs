using Android.OS;
using Android.Runtime;
using Android.Views;
using BdcMobile.Core.ViewModels;
using MvvmCross.Droid.Support.V4;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using MvvmCross.Platforms.Android.Presenters.Attributes;

namespace BdcMobile.Droid.Views
{
    [MvxTabLayoutPresentation(TabLayoutResourceId = Resource.Id.tabs, ViewPagerResourceId = Resource.Id.viewpager, Title = "Internal Chat", ActivityHostViewModelType = typeof(EventDetailsViewModel))]
    [Register(nameof(EventDetailsInternalChatView))]
    public class EventDetailsInternalChatView : MvxFragment<EventDetailsInternalChatViewModel>
    {
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);

            return this.BindingInflate(Resource.Layout.EventDetailsInternalChat, null);
        }
    }
}