using Android.Content;
using Android.Support.V7.Widget;

namespace BdcMobile.Droid.UIControl
{
    public class ToEndSmoothScroller: LinearSmoothScroller
    {
        public ToEndSmoothScroller(Context context) : base(context)
        {

        }

        protected override int VerticalSnapPreference
        {
            get { return SnapToEnd; }
        }
    }
}