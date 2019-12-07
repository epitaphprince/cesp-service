using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CESP.Core.Contracts;
using CESP.Core.Models;

namespace CESP.Core.Managers.Schedulers
{
    public class ScheduleManager : IScheduleManager
    {
        private readonly IScheduleProvider _scheduleProvider;
        private const string title = "Группы уровня"; 
        
        public ScheduleManager(IScheduleProvider scheduleProvider)
        {
            _scheduleProvider = scheduleProvider;
        }

        public async Task<List<ScheduleSection>> GetList()
        {
            var result = new List<ScheduleSection>();
            var segments = await _scheduleProvider.GetSchedules();
            
            result.Add(SetSection(segments,
                LanguageLevelPriorityEnum.A1,
                LanguageLevelPriorityEnum.A2,
                $"{title} A1"));
            
            result.Add(SetSection(segments,
                LanguageLevelPriorityEnum.A2,
                LanguageLevelPriorityEnum.B1,
                $"{title} A2"));
            
            result.Add(SetSection(segments,
                LanguageLevelPriorityEnum.B1,
                LanguageLevelPriorityEnum.C1,
                $"{title} B"));
            
            result.Add(SetSection(segments,
                LanguageLevelPriorityEnum.C1,
                $"{title} C"));
            
            return result;
        }

        private ScheduleSection SetSection(List<ScheduleSegment> segmentsAll,
            LanguageLevelPriorityEnum startPriority,
            string name)
        {
            var segments = segmentsAll
                .Where(s => s.LevelRang >= (int) startPriority)
                .ToList();
            
            return SetSectionBySegmets(segments, name);
        }

        private ScheduleSection SetSection(List<ScheduleSegment> segmentsAll,
            LanguageLevelPriorityEnum startPriority,
            LanguageLevelPriorityEnum endPriority,
            string name)
        {
            var segments = segmentsAll
                .Where(s => s.LevelRang >= (int) startPriority
                            && s.LevelRang < (int) endPriority)
                .ToList();

            return SetSectionBySegmets(segments, name);
        }

        private ScheduleSection SetSectionBySegmets(List<ScheduleSegment> segments, string name)
        {
            if (segments.Count == 0)
            {
                return null;
            }

            return new ScheduleSection
            {
                ScheduleSegments = segments,
                Name = name
            };
        }
        
        public async Task<List<ScheduleSegment>> GetListByLevels(string[] levelNames)
        {
            return await _scheduleProvider.GetSchedulesByLevels(levelNames);
        }
    }
}