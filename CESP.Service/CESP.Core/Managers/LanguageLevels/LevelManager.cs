using System.Collections.Generic;
using System.Threading.Tasks;
using CESP.Core.Contracts;
using CESP.Core.Models;

namespace CESP.Core.Managers.LanguageLevels
{
    public class LevelManager : ILevelManager
    {
        private readonly ILevelProvider _levelProvider;

        public LevelManager(ILevelProvider levelProvider)
        {
            _levelProvider = levelProvider;
        }

        public Task<List<LevelShort>> GetList()
        {
            return _levelProvider.GetLevels();
        }

        public Task<Level> Get(string name)
        {
            return _levelProvider.GetLevel(name);
        }
    }
}