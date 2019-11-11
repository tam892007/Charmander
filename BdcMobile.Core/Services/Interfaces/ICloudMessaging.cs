using BdcMobile.Core.Models;
using System;
using System.Threading.Tasks;

namespace BdcMobile.Core.Services.Interfaces
{
    public interface ICloudMessaging
    {
        void Initialize();

        void Publish(Notification notification);

        Guid Subscribe(Action<Notification> OnReceiveNotification, string tag);

        void Unsubscribe(Guid id);

        string GetCloudMessagingToken();
    }
}
