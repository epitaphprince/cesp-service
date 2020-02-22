using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CESP.Core.Contracts;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace CESP.Core.Managers.File
{
    public class FileManager : IFileManager
    {
        public FileManager(IHostingEnvironment hostingEnvironment,
            IFileInfoProvider fileProvider)
        {
            _hostingEnvironment = hostingEnvironment;
            _fileProvider = fileProvider;
        }

        public string GetFilePath(string specificFolder, string fileName)
        {
            return string.IsNullOrEmpty(fileName)
                ? null
                : $"{specificFolder}/{fileName}";
        }

        public async Task UpdateFile(IFormFile fileNew, string fileOldUrl, string specificFolder, string fileInfo)
        {
            var fileNewPath = GetFilePath(specificFolder,  fileNew.FileName);
                
            if (!string.IsNullOrEmpty(fileOldUrl))
            {
                var fileOldPath = GetFilePath(specificFolder, Path.GetFileName(fileOldUrl));
                await DeleteContent(fileOldPath);
                    
                await _fileProvider.Update(fileOldPath, fileNewPath, fileInfo);
            }
            else
            {
                await _fileProvider.Add(fileNewPath, fileInfo);
            }

            await SaveContent(fileNew, specificFolder);
        }

        public Task DeleteContent(string fileName)
        {
            var filePathOld = Path.Combine(_hostingEnvironment.WebRootPath, fileName);
            if (System.IO.File.Exists(filePathOld))
            {
                System.IO.File.Delete(filePathOld);
            }
            throw new FileNotFoundException();
        }

        public async Task SaveContent(IFormFile file, string destinationFolder)
        {
            var uploads = Path.Combine(_hostingEnvironment.WebRootPath, destinationFolder);
            if (file.Length > 0)
            {
                var filePath = Path.Combine(uploads, file.FileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }
            }
        }

        public Task SaveImages(IFormFile[] files, string destinationFolder)
        {
            return Task.WhenAll(files.Select(file => SaveContent(file, destinationFolder)));
        }

        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IFileInfoProvider _fileProvider;
    }
}