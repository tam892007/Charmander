
using Android.App;
using Android.Content;
using Android.OS;
using BdcMobile.Core.Commons;
using BdcMobile.Core.Services.Interfaces;
using Firebase.Iid;
using Firebase.Messaging;

namespace BdcMobile.Droid
{
    [Service]
    [IntentFilter(new[] { "com.google.firebase.MESSAGING_EVENT" })]
    public class FirebaseCloudMessaging : FirebaseMessagingService, ICloudMessaging
    {
        private Context _context;

        public FirebaseCloudMessaging() : base()
        {

        }

        public FirebaseCloudMessaging(Context context) : base()
        {
            _context = context;
        }

        public void Initialize()
        {
            if (Build.VERSION.SdkInt < BuildVersionCodes.O)
            {
                // Notification channels are new in API 26 (and not a part of the
                // support library). There is no need to create a notification
                // channel on older versions of Android.
                return;
            }

            var channel = new NotificationChannel(Constants.AppConfig.FCMChannelID,
                                                  Constants.AppConfig.FCMChannelName,
                                                  NotificationImportance.Default)
            {

                Description = Constants.AppConfig.FCMChannelDesc
            };

            var notificationManager = (NotificationManager)_context.GetSystemService(NotificationService);
            notificationManager.CreateNotificationChannel(channel);
        }

        public override void OnNewToken(string p0)
        {
            base.OnNewToken(p0);
        }

        public override void OnMessageReceived(RemoteMessage remoteMessage)
        {
            base.OnMessageReceived(remoteMessage);
        }
    }
}