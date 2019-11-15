using Android.Content;
using Android.Graphics.Drawables;
using Android.Runtime;
using Android.Support.V4.Content;
using Android.Util;
using Android.Views;
using Android.Widget;
using System;

namespace BdcMobile.Droid.UIControl
{
    public class EditTextWithClearButton: EditText
    {
        private Drawable clearButton;

        protected EditTextWithClearButton(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
        {
        }

        public EditTextWithClearButton(Context context) : base(context)
        {
            Init();
        }

        public EditTextWithClearButton(Context context, IAttributeSet attrs) : base(context, attrs)
        {
            Init(attrs);
        }

        public EditTextWithClearButton(Context context, IAttributeSet attrs, int defStyle) : base(context, attrs, defStyle)
        {
            Init(attrs);
        }

        private void Init(IAttributeSet attrs = null)
        {
            var activity = Plugin.CurrentActivity.CrossCurrentActivity.Current;
            if (activity == null)
                return;

            clearButton = ContextCompat.GetDrawable(Android.App.Application.Context, Resource.Drawable.clear);
            clearButton.SetBounds(0, 0, clearButton.IntrinsicWidth, clearButton.IntrinsicHeight);

            SetupEvents();
        }

        private void SetupEvents()
        {
            // Handle clear button visibility
            this.TextChanged += (sender, e) => {
                UpdateClearButton();
            };
            this.FocusChange += (sender, e) => {
                UpdateClearButton(e.HasFocus);
            };

            // Handle clearing the text
            this.Touch += (sender, e) => {
                if (this.GetCompoundDrawables()[2] == null || e.Event.Action != MotionEventActions.Up)
                {
                    e.Handled = false;
                    return;
                }
                if (e.Event.GetX() > (this.Width - this.PaddingRight - clearButton.IntrinsicWidth))
                {
                    this.Text = "";
                    UpdateClearButton();
                    e.Handled = true;
                }
                else
                    e.Handled = false;
            };
        }

        private void UpdateClearButton(bool hasFocus = true)
        {
            var compoundDrawables = this.GetCompoundDrawables();
            var compoundDrawable = this.Text.Length == 0 || !hasFocus ? null : clearButton;
            this.SetCompoundDrawables(compoundDrawables[0], compoundDrawables[1], compoundDrawable, compoundDrawables[3]);
        }
    }
}