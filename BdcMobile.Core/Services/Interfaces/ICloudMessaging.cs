using BdcMobile.Core.Models;
using System;

namespace BdcMobile.Core.Services.Interfaces
{
    public interface ICloudMessaging
    {
        void Initialize();

        void Publish(Notification notification);

        Guid Subscribe(Action<Notification> OnReceiveNotification);

        void Unsubscribe(Guid id);
    }
}
