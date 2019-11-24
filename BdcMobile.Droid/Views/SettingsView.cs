using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Views;
using BdcMobile.Core.Interactions;
using BdcMobile.Core.ViewModels;
using MvvmCross.Base;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Droid.Support.V7.AppCompat;
using MvvmCross.Platforms.Android.Presenters.Attributes;
using MvvmCross.ViewModels;

namespace BdcMobile.Droid.Views
{
    [Activity(Label = "Settings", LaunchMode = LaunchMode.SingleTop)]
    [MvxActivityPresentation]
    public class SettingsView : MvxAppCompatActivity<SettingsViewModel>
    {
        private IMvxInteraction<YesNoQuestion> _interaction;
        public IMvxInteraction<YesNoQuestion> Interaction
        {
            get => _interaction;
            set
            {
                if (_interaction != null)
                    _interaction.Requested -= OnInteractionRequested;

                _interaction = value;
                _interaction.Requested += OnInteractionRequested;
            }
        }

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.SettingsView);
            this.AddBindings(this, "Interaction ConfirmSave");
        }

        private void OnInteractionRequested(object sender, MvxValueEventArgs<YesNoQuestion> eventArgs)
        {
            var yesNoQuestion = eventArgs.Value;

            // show dialog
            Android.App.AlertDialog.Builder dialog = new AlertDialog.Builder(this);
            AlertDialog alert = dialog.Create();
            alert.SetMessage(yesNoQuestion.Question);
            alert.SetButton("Đồng ý", (c, ev) =>
            {
                yesNoQuestion.YesNoCallback(true);
            });
            alert.SetButton2("Hủy bỏ", (c, ev) =>
            {
                yesNoQuestion.YesNoCallback(false);
            });
            alert.Show();
        }
    }
}