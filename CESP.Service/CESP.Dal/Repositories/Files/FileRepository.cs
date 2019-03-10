using System.IO;
using System.Threading.Tasks;
using CESP.Dal.Infrastructure;
using Microsoft.Extensions.Options;

namespace CESP.Dal.Repositories.Files
{
    public class FileRepository: IFileRepository
    {
        public const string BaseFolder = "Files";
        
        private readonly string _rootPath;
        private readonly IFolderProvider _folderProvider;
        private readonly IFileProvider _fileProvider;

        public FileRepository(IOptions<FileStorage> options, 
            IFolderProvider folderProvider,
            IFileProvider fileProvider)
        {
            _rootPath = Path.Combine(options.Value.FilesPath, BaseFolder);
            _folderProvider = folderProvider;
            _fileProvider = fileProvider;
        }


        public async Task Add((Stream Content, string FileName) file, string subFolderName)
        {
            var folder = GetFolderPath(subFolderName);

            CreateFolderIfNotExists(folder);

            string filePath = GetFilePath(folder, file.FileName);

            using (Stream f = _fileProvider.Write(filePath))
            {
                await file.Content.CopyToAsync(f);
            }
        }
        
        public Stream Get(string subFolderName, string name)
        {
            string folderPath = GetFolderPath(subFolderName);
            string filePath = GetFilePath(folderPath, folderPath);
            return _fileProvider.Read(filePath);
        }


        private string GetFolderPath(string subFolderName)
        {
            return Path.Combine(_rootPath, subFolderName);
        }

        private string GetFilePath(string folder, string fileName)
        {
            return Path.Combine(folder, fileName);
        }

        private void CreateFolderIfNotExists(string folderPath)
        {
            if (!_folderProvider.Exists(folderPath))
            {
                _folderProvider.Create(folderPath);
            }
        }
    }
}