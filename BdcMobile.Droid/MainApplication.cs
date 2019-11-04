using Android.App;
using Android.Runtime;
using BdcMobile.Core;
using MvvmCross.Droid.Support.V7.AppCompat;
using System;

namespace BdcMobile.Droid
{
    [Application]
    public class MainApplication : MvxAppCompatApplication<Setup, App>
    {
        public MainApplication(IntPtr javaReference, JniHandleOwnership transfer)
            : base(javaReference, transfer)
        {
        }

        public override void OnCreate()
        {
            base.OnCreate();
        }
    }
}