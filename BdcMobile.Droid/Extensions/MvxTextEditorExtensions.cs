using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Input;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using BdcMobile.Core.Commons;
using BdcMobile.Droid.UIListenner;
using MvvmCross.Commands;
using MvvmCross.ViewModels;

namespace BdcMobile.Droid.Extensions
{
    public static class MvxTextEditorExtensions
    {
        public static void OnTextChangeListener(this EditText editText, Func<MvxNotifyTask> onTextChangedNotify, Func<IMvxAsyncCommand> searchCommand, CancellationTokenSource cts)
        {
            var customTextWatcher = new BDCTextWatcher();
            editText.AddTextChangedListener(customTextWatcher);
            customTextWatcher.TextChange += () =>
            {
                var isTaskNotifycompleted = onTextChangedNotify.Invoke();
                
                if (isTaskNotifycompleted != null && isTaskNotifycompleted.IsNotCompleted)
                {
                    Log.Info(Constants.AppConfig.LogTag, "Cancel searchCommand ");
                    IMvxAsyncCommand scommand = searchCommand?.Invoke();
                    cts.Cancel();
                }

                if (isTaskNotifycompleted == null || !isTaskNotifycompleted.IsNotCompleted)
                {
                    Log.Info(Constants.AppConfig.LogTag, "Execute searchCommand ");
                    var command = searchCommand?.Invoke();
                    
                    var task = command.ExecuteAsync();
                    //command.Cancel();

                } else
                {
                    Log.Info(Constants.AppConfig.LogTag, "searchCommand is not completed");
                }
                
            };
        }
    }
}