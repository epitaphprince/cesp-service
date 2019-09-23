using System.IO;

namespace CESP.Dal.Infrastructure
{
    public class FolderProvider : IFolderProvider
    {
        public bool Exists(string folderPath) => Directory.Exists(folderPath);

        public void Create(string folderPath) => Directory.CreateDirectory(folderPath);
    }
}