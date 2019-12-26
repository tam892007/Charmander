
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Preferences;
using Android.Support.V4.App;
using BdcMobile.Core;
using BdcMobile.Core.Commons;
using BdcMobile.Core.Models;
using BdcMobile.Core.Services.Interfaces;
using BdcMobile.Droid.Services;
using Firebase.Iid;
using Firebase.Messaging;
using MvvmCross;
using MvvmCross.Plugin.Messenger;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using Notification = BdcMobile.Core.Models.Notification;

namespace BdcMobile.Droid.CloudMessaging
{
    [Service]
    [IntentFilter(new[] { "com.google.firebase.MESSAGING_EVENT" })]
    public class FirebaseCloudMessaging : FirebaseMessagingService, ICloudMessaging
    {
        private static IMvxMessenger _messenger;
        private static Dictionary<Guid, MvxSubscriptionToken> _subscriptionToken;
        private const string PREFERENCE_LAST_NOTIF_ID = "PREFERENCE_LAST_NOTIF_ID";

        public FirebaseCloudMessaging() : base()
        {
            
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
                                                  NotificationImportance.High)
            {

                Description = Constants.AppConfig.FCMChannelDesc
            };

            var notificationManager = (NotificationManager)Application.Context.GetSystemService(NotificationService);
            notificationManager.CreateNotificationChannel(channel);

            _messenger = _messenger ?? Mvx.IoCProvider.Resolve<IMvxMessenger>();
            _subscriptionToken = new Dictionary<Guid, MvxSubscriptionToken>();
        }

        public override void OnNewToken(string p0)
        {
            base.OnNewToken(p0);
        }

        public override void OnMessageReceived(RemoteMessage remoteMessage)
        {
            base.OnMessageReceived(remoteMessage);

            var userLoggedIn = CommonService.GetFromSharedPreferences<bool>(Application.Context, Constants.AppConfig.UserLoggedIn);
            if (!userLoggedIn)
            {
                return;
            }

            var notification = GetNotification(remoteMessage);

            var droidNotification =
                new NotificationCompat.Builder(Application.Context, Constants.AppConfig.FCMChannelID)
                .SetContentTitle(notification.Title)
                .SetContentText(notification.Content)
                .SetSmallIcon(Resource.Drawable.ic_stat_bdc)
                .SetLargeIcon(BitmapFactory.DecodeResource(Resources, Resource.Mipmap.ic_launcher))
                .SetContentIntent(GetPendingIntent(notification))
                .SetPriority(NotificationCompat.PriorityHigh)
                .SetAutoCancel(true)
                .Build();

            var notificationManager = NotificationManagerCompat.From(Application.Context);
            notificationManager.Notify(DateTime.Now.Millisecond, droidNotification);

            Publish(notification);
        }

        private Notification GetNotification(RemoteMessage message)
        {
            return new Notification
            {
                Title = message.Data["title"],
                Content = message.Data["content"],
                Object = Convert.ToInt32(message.Data["surveyID"]),
                Action = message.Data["action"],
                Created_at = message.Data["notificationDate"]
            };
        }

        private PendingIntent GetPendingIntent(Notification notification)
        {
            Intent intent = null;
            switch (notification.Type)
            {
                case NotificationType.InternalChatUpdate:
                case NotificationType.ExternalChatUpdate:
                    intent = new Intent(Application.Context, typeof(SplashScreen));
                    intent.AddFlags(ActivityFlags.SingleTop);
                    intent.PutExtra(Constants.AppConfig.FCMExtraName, JsonConvert.SerializeObject(notification));
                    return PendingIntent.GetActivity(Application.Context, 0, intent, PendingIntentFlags.UpdateCurrent);
                default:
                    intent = new Intent(Intent.ActionView);
                    intent.SetData(Android.Net.Uri.Parse(notification.GetWebUrl(GetBaseWebUrl(Application.Context))));
                    return PendingIntent.GetActivity(Application.Context, 0, intent, PendingIntentFlags.UpdateCurrent);
            }
        }

        public void Publish(Notification notification)
        {
            _messenger?.Publish(new NotificationMessage(this, notification));
        }

        public Guid Subscribe(Action<Notification> OnReceiveNotification, string tag)
        {
            if (_messenger.HasSubscriptionsForTag<NotificationMessage>(tag))
            {
                return Guid.Empty;
            }

            var subscription = _messenger.SubscribeOnMainThread<NotificationMessage>((msg) =>
            {
                OnReceiveNotification(msg.Notification);
            }, MvxReference.Strong);

            _subscriptionToken.Add(subscription.Id, subscription);

            return subscription.Id;
        }

        public void Unsubscribe(Guid id)
        {
            _messenger.Unsubscribe<NotificationMessage>(_subscriptionToken[id]);
            _subscriptionToken.Remove(id);
        }

        [Obsolete]
        public string GetCloudMessagingToken()
        {
            return FirebaseInstanceId.Instance.Token;
        }

        private static int GetNextNotifId(Context context)
        {
            int id = CommonService.GetFromSharedPreferences<int>(context, PREFERENCE_LAST_NOTIF_ID) + 1;
            if (id == int.MaxValue) { id = 0; }
            CommonService.PutToSharedPreferences<int>(context, PREFERENCE_LAST_NOTIF_ID, id);
            return id;
        }

        private static string GetBaseWebUrl(Context context)
        {
            if (App.Context != null) return App.Context.ServerAddress;
            else
            {
                var baseUrl = CommonService.GetFromSharedPreferences<string>(context, Constants.AppConfig.ServerAddressKey);
                if (!string.IsNullOrEmpty(baseUrl))
                {
                    return baseUrl;
                }
                else
                {
                    return Constants.AppAPI.IPAPI;
                } 
            }
        }
    }
}