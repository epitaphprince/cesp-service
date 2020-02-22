using System.Threading.Tasks;
using CESP.Core.Contracts;
using CESP.Database.Context.Files.Models;

namespace CESP.Dal.Providers
{
    public class FileInfoProvider : IFileInfoProvider
    {
        private ICespRepository _cespRepository;
        
        public FileInfoProvider(ICespRepository cespRepository)
        {
            _cespRepository = cespRepository;
        }

        public async Task Update(string fileNameOld, string fileNameNew, string info)
        {
            await _cespRepository.UpdateFile(fileNameOld, fileNameNew, info);
        }

        public async Task Add(string fileName, string fileInfo)
        {
            var file = new FileDto
            {
                Info = fileInfo,
                Name = fileName
            };
            
            await _cespRepository.AddFile(file);
        }
    }
}