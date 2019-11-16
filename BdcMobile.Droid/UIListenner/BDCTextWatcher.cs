using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Text;
using Android.Util;
using Android.Views;
using Android.Widget;
using BdcMobile.Core.Commons;
using Java.Lang;

namespace BdcMobile.Droid.UIListenner
{
    public class BDCTextWatcher : Java.Lang.Object, ITextWatcher
    {

        Handler handler = new Handler(Looper.MainLooper/*UI thread*/);

        private long DELAY = 1000;

        public delegate void AfterTextChangeEventHandler();
        public event AfterTextChangeEventHandler TextChange;
        Runnable workRunnable;
        public void AfterTextChanged(IEditable s)
        {
            handler.RemoveCallbacks(workRunnable);
            workRunnable = new Runnable(new Action(delegate
            {
                TextChange();
            }));

            handler.PostDelayed(workRunnable, DELAY);
        }


        public new void Dispose()
        {
            base.Dispose();
        }

        public void BeforeTextChanged(ICharSequence s, int start, int count, int after)
        {            
        }

        public void OnTextChanged(ICharSequence s, int start, int before, int count)
        {
        }
    }
}