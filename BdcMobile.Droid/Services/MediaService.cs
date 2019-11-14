using BdcMobile.Core.Services.Interfaces;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace BdcMobile.Droid.Services
{
    class MediaService : IMediaService
    {
        public async Task<string> PickPhotoAsync()
        {
            var file = await CrossMedia.Current.PickPhotoAsync(new PickMediaOptions { CompressionQuality = 92, PhotoSize = PhotoSize.Full, });
            return file?.Path;
        }

        public async Task<IList<string>> PickPhotosAsync()
        {
            var files = await CrossMedia.Current.PickPhotosAsync(new PickMediaOptions { CompressionQuality = 92, PhotoSize = PhotoSize.Full, },
                new MultiPickerOptions { });
            return files?.Select(x => x.Path).ToList();
        }

        public async Task<string> TakePhotoAsync()
        {
            var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
            {
                PhotoSize = PhotoSize.Full,
                CompressionQuality = 92
            });
            return file?.Path;
        }
    }
}