using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace CESP.Core.Managers.File
{
    public interface IFileManager
    {
        Task UpdateFile(IFormFile file, string fileUrlOld, string specificFolder, string fileInfo);
        Task SaveContent(IFormFile file, string destinationFolder);
        Task SaveImages(IFormFile[] files, string destinationFolder);
        Task DeleteContent(string fileName);
        string GetFilePath(string specificFolder, string fileName);
    }
}