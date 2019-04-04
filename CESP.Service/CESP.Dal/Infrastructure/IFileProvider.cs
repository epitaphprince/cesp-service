using System.IO;

namespace CESP.Dal.Infrastructure
{
    public interface IFileProvider
    {
        Stream Read(string filePath);
        Stream Write(string filePath);
    }
}