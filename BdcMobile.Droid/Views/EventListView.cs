using Android.App;
using Android.OS;
using BdcMobile.Core.ViewModels;
using MvvmCross.Platforms.Android.Views;

namespace BdcMobile.Droid.Views
{
    [Activity(Label = "Event List")]
    public class EventListView : MvxActivity<EventListViewModel>
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.EventList);
        }
        
    }
}