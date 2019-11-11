using Android.Content;
using Android.Util;
using Android.Widget;

namespace BdcMobile.Droid.UIControl
{
    public class SquareRelativeLayout : RelativeLayout
    {
        public SquareRelativeLayout(Context context) : base(context) { }

        public SquareRelativeLayout(Context context, IAttributeSet attrs) : base(context, attrs) { }

        public SquareRelativeLayout(Context context, IAttributeSet attrs, int defStyleAttr) : base(context, attrs, defStyleAttr) { }

        public SquareRelativeLayout(Context context, IAttributeSet attrs, int defStyleAttr, int defStyleRes) : base(context, attrs, defStyleAttr, defStyleRes) { }

        protected override void OnMeasure(int w, int h)
        {
            base.OnMeasure(w, w);
        }
    }
}