using System.IO;
using System.Threading.Tasks;

namespace WorkerMan.Storage.Core
{
    public abstract class BaseStorageManager
    {
        public abstract Task<string> UploadPhoto(Stream stream, string fileName);
        public abstract Task<string> UploadVideo(Stream stream, string fileName);

    }
}
