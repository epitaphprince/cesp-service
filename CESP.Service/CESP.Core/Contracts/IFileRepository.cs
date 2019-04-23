using System.IO;
using System.Threading.Tasks;

namespace CESP.Core.Contracts
{
    public interface IFileRepository
    {
        Task Add((Stream Content, string FileName) file, string subFolderName);

        Stream Get(string subFolderName, string name);
    }
}