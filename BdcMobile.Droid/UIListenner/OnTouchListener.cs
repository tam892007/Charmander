using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace BdcMobile.Droid.UIListenner
{
    class OnTouchListener : Java.Lang.Object, View.IOnTouchListener
    {

        public delegate void HideKeyboardEventHandler();
        public event HideKeyboardEventHandler OnClick;

        public bool OnTouch(View v, MotionEvent e)
        {
            OnClick();
            return true;
        }
    }
}