using System.Threading.Tasks;

namespace CESP.Core.Contracts
{
    public interface IFileInfoProvider
    {
        Task Update(string fileNameOld, string fileNameNew, string info);

        Task Add(string fileName, string fileInfo);
    }
}