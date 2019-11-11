using System;
using Android.Views;
using static Android.Support.V4.View.ViewPager;

namespace BdcMobile.Droid.UIListenner
{
    public class ZoomOutPageTransformer : Java.Lang.Object, IPageTransformer
    {
        private const float MIN_SCALE = 0.75f;

        public void TransformPage(View page, float position)
        {
            if (position < -1)
            {
                page.Alpha = 0f;
            }
            else if (position <= 0)
            {
                page.Alpha = 1f;
                page.TranslationX = 0f;
                page.ScaleX = 1f;
                page.ScaleY = 1f;
            }
            else if (position <= 1)
            {
                // Fade the page out.
                page.Alpha = 1 - position;
                // Counteract the default slide transition
                page.TranslationX = page.Width * -position;
                // Scale the page down (between MIN_SCALE and 1)
                var scaleFactor = (MIN_SCALE + (1 - MIN_SCALE) * (1 - Math.Abs(position)));
                page.ScaleX = scaleFactor;
                page.ScaleY = scaleFactor;
            }
            else
            {
                // This page is way off-screen to the right.
                page.Alpha = 0f;
            }
        }
    }
}