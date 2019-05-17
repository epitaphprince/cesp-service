using System.Collections.Generic;
using System.Threading.Tasks;
using CESP.Core.Models;

namespace CESP.Core.Managers.LanguageLevels
{
    public interface ILevelManager
    {
        Task<List<LevelShort>> GetList();

        Task<Level> Get(string name);
    }
}