using System.Collections.Generic;
using System.Threading.Tasks;
using CESP.Core.Models;

namespace CESP.Core.Contracts
{
    public interface ILevelProvider
    {
        Task<List<LevelShort>> GetLevels();

        Task<Level> GetLevel(string name);
    }
}