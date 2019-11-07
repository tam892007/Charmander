
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Support.V4.App;
using BdcMobile.Core.Commons;
using BdcMobile.Core.Models;
using BdcMobile.Core.Services.Interfaces;
using Firebase.Messaging;
using MvvmCross;
using MvvmCross.Plugin.Messenger;
using System;
using System.Collections.Generic;
using Notification = BdcMobile.Core.Models.Notification;

namespace BdcMobile.Droid.CloudMessaging
{
    [Service]
    [IntentFilter(new[] { "com.google.firebase.MESSAGING_EVENT" })]
    public class FirebaseCloudMessaging : FirebaseMessagingService, ICloudMessaging
    {
        private Context _context;

        private static IMvxMessenger _messenger;

        private static Dictionary<Guid, MvxSubscriptionToken> _subscriptionToken;

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
                                                  NotificationImportance.High)
            {

                Description = Constants.AppConfig.FCMChannelDesc
            };

            var notificationManager = (NotificationManager)_context.GetSystemService(NotificationService);
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

            var notification = GetNotification(remoteMessage);

            var droidNotification =
                new NotificationCompat.Builder(Application.Context, Constants.AppConfig.FCMChannelID)
                .SetContentTitle(notification.Title)
                .SetContentText(notification.Content)
                .SetSmallIcon(Resource.Drawable.ic_stat_bdc)
                .SetLargeIcon(BitmapFactory.DecodeResource(Resources, Resource.Mipmap.ic_launcher))
                //.SetContentIntent()
                .SetPriority(NotificationCompat.PriorityHigh)
                .Build();

            var notificationManager = NotificationManagerCompat.From(Application.Context);
            notificationManager.Notify(1, droidNotification);

            Publish(notification);
        }

        private Notification GetNotification(RemoteMessage message)
        {
            return new Notification
            {
                Title = message.Data["title"],
                Content = message.Data["content"],
                Type = Enum.Parse<NotificationType>(message.Data["type"]),
            };
        }

        public void Publish(Notification notification)
        {
            _messenger.Publish(new NotificationMessage(this, notification));
        }

        public Guid Subscribe(Action<Notification> OnReceiveNotification)
        {
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
    }
}