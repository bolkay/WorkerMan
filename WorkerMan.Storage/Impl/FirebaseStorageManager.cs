using Firebase.Storage;
using System;
using System.IO;
using System.Threading.Tasks;
using WorkerMan.Storage.Core;

namespace WorkerMan.Storage.Impl
{
    public class FirebaseStorageManager : BaseStorageManager
    {
        public enum FileType
        {
            Image,
            Video
        }
        public const string FirebaseBucket = "workerman-562d2.appspot.com";
        public const double PhotoUploadTimeoutInMinutes = 5;
        public const double VideoUploadTimeoutInMinutes = 30;
        public override async Task<string> UploadPhoto(Stream stream, string fileName)
        {
            return await UploadFile(stream, fileName, PhotoUploadTimeoutInMinutes, FileType.Image);
        }

        private void Progress_ProgressChanged(object sender, FirebaseStorageProgress e)
        {
            Console.WriteLine(e.Length + "   " + e.Position + "   " + e.Percentage + "%");
        }

        public override async Task<string> UploadVideo(Stream stream, string fileName)
        {
            return await UploadFile(stream, fileName, VideoUploadTimeoutInMinutes, FileType.Video);
        }
        private async Task<string> UploadFile(Stream stream, string fileName, double timeOut, FileType fileType)
        {

            FirebaseStorageOptions firebaseStorageOptions = new FirebaseStorageOptions
            {
                HttpClientTimeout = TimeSpan.FromMinutes(timeOut),
                ThrowOnCancel = false
            };

            string type = fileType == FileType.Image ? "images" : "videos";

            FirebaseStorageTask firebaseStorageTask = new FirebaseStorage(FirebaseBucket, firebaseStorageOptions)
                    .Child(type)
                    .Child(fileName)
                    .PutAsync(stream);

            firebaseStorageTask.Progress.ProgressChanged += Progress_ProgressChanged;

            string result = firebaseStorageTask.GetAwaiter().GetResult();

            return await Task.FromResult(result);
        }
    }
}
