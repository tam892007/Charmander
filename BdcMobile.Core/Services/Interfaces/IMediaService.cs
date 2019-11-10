using System.IO;
using System.Threading.Tasks;

namespace BdcMobile.Core.Services.Interfaces
{
    public interface IMediaService
    {
        Task<string> TakePhotoAsync();
        Task<string> PickPhotoAsync();
    }
}
