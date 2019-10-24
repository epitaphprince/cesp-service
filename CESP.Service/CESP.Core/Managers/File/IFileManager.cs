using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace CESP.Core.Managers.File
{
    public interface IFileManager
    {
        Task SaveImage(IFormFile file, string destinationFolder);
        Task SaveImages(IFormFile[] files, string destinationFolder);
    }
}