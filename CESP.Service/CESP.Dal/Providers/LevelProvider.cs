using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CESP.Core.Contracts;
using CESP.Core.Models;

namespace CESP.Dal.Providers
{
    public class LevelProvider : ILevelProvider
    {
        private ICespRepository _cespRepository;
        private readonly IMapper _mapper;

        public LevelProvider(ICespRepository cespRepository,
            IMapper mapper)
        {
            _cespRepository = cespRepository;
            _mapper = mapper;
        }


        public async Task<List<LevelShort>> GetLevels()
        {
            var levels = await _cespRepository.GetLanguageLevels();

            return levels.Select(l => _mapper.Map<LevelShort>(l)).ToList();
        }

        public async Task<Level> GetLevel(string name)
        {
            var level = await _cespRepository.GetLanguageLevel(name);

            return _mapper.Map<Level>(level);
        }
    }
}