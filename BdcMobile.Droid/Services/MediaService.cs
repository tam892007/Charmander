using System.IO;
using System.Threading.Tasks;
using BdcMobile.Core.Services.Interfaces;
using Plugin.Media;
using Plugin.Media.Abstractions;

namespace BdcMobile.Droid.Services
{
    class MediaService : IMediaService
    {
        public async Task<string> PickPhotoAsync()
        {
            var file = await CrossMedia.Current.PickPhotoAsync(new PickMediaOptions { CompressionQuality = 92, PhotoSize = PhotoSize.Medium, });
            return file?.Path;
        }

        public async Task<string> TakePhotoAsync()
        {
            var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
            {
                PhotoSize = PhotoSize.Medium,
                CompressionQuality = 92
            });
            return file?.Path;
        }
    }
}