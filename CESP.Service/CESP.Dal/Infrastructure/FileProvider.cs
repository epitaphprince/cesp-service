using System.IO;

namespace CESP.Dal.Infrastructure
{
    public class FileProvider : IFileProvider
    {
//        public bool Exists(string filePath) => File.Exists(filePath);
//
//        public void Delete(string filePath) => File.Delete(filePath);
//
        public Stream Read(string filePath) => File.OpenRead(filePath);

        public Stream Write(string filePath) => File.OpenWrite(filePath);
    }
}