using Android.Views;
using MvvmCross.Binding.Bindings.Target;
using MvvmCross.Binding.Extensions;
using System.Reflection;

namespace BdcMobile.Droid.Bindings
{
    public class DroidVisibilityBinding : MvxPropertyInfoTargetBinding<View>
    {
        public DroidVisibilityBinding(object target, PropertyInfo targetPropertyInfo)
        : base(target, targetPropertyInfo)
        {
        }

        protected override void SetValueImpl(object target, object value)
        {
            ((View)target).Visibility = value.ConvertToBoolean() ? ViewStates.Visible : ViewStates.Invisible;
        }
    }
}