using System.IO;
using System.Threading.Tasks;

namespace CESP.Dal.Repositories.Files
{
    public interface IFileRepository
    {
        Task Add((Stream Content, string FileName) file, string subFolderName);

        Stream Get(string subFolderName, string name);
    }
}