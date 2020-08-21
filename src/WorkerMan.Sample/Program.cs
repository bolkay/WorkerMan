using System;
using System.IO;
using WorkerMan.Storage.Impl;

namespace WorkerMan.Sample
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Reading file....");
            FileStream file = File.Open(@"C:\Users\KTBolarinwa\Pictures\Camera Roll\sampleVideo.mp4", FileMode.Open);
            FirebaseStorageManager firebaseStorageManager = new FirebaseStorageManager();
            Console.WriteLine("Uploading.....");
            string path = firebaseStorageManager.UploadVideo(file, Path.GetFileName(file.Name)).Result;
            Console.WriteLine("Download URL= " + path);
            Console.ReadKey();
        }
    }
}
