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
    class OnClickListener : Java.Lang.Object, View.IOnClickListener
    {

        public delegate void HideKeyboardEventHandler();
        public event HideKeyboardEventHandler OnClick;

        void View.IOnClickListener.OnClick(View v)
        {
            OnClick();
            return true;
        }
    }
}