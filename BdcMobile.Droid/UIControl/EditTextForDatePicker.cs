using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.App;
using Android.Util;
using Android.Views;
using Android.Widget;
using BdcMobile.Core.Commons;

namespace BdcMobile.Droid.UIControl
{
    class EditTextForDatePicker: EditText, DatePickerDialog.IOnDateSetListener
    {
        public EditTextForDatePicker(Context context, IAttributeSet attrs)
            : base(context, attrs)
        {
            Focusable = false;
            Click += delegate
            {
                var dialog = new DatePickerDialogFragment(Context, 
                    DateTime.ParseExact(Text, Constants.DateTimeFormat.DateOnlyFormat, System.Globalization.CultureInfo.CurrentCulture), 
                    this);
                dialog.Show(((FragmentActivity)Plugin.CurrentActivity.CrossCurrentActivity.Current.Activity).SupportFragmentManager, nameof(EditTextForDatePicker));
            };
        }

        public void OnDateSet(Android.Widget.DatePicker view, int year, int monthOfYear, int dayOfMonth)
        {
            Text = new DateTime(year, monthOfYear + 1, dayOfMonth).ToString(Constants.DateTimeFormat.DateOnlyFormat);
        }

        private class DatePickerDialogFragment : Android.Support.V4.App.DialogFragment
        {
            private readonly Context _context;
            private DateTime _date;
            private readonly DatePickerDialog.IOnDateSetListener _listener;

            public DatePickerDialogFragment(Context context, DateTime date, DatePickerDialog.IOnDateSetListener listener)
            {
                _context = context;
                _date = date;
                _listener = listener;
            }

            public override Dialog OnCreateDialog(Bundle savedState)
            {
                var dialog = new DatePickerDialog(_context, _listener, _date.Year, _date.Month - 1, _date.Day);
                return dialog;
            }
        }
    }
}