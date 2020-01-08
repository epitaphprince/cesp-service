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
        private const string childrenTitle = "Дети";
        private const string catalanTitle = "Каталанский";
        
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
            var segments = GetSegmentsByLevelPriority(segmentsAll, startPriority);
            return SetSectionBySegments(segments, name);
        }

        private ScheduleSection SetSection(List<ScheduleSegment> segmentsAll,
            LanguageLevelPriorityEnum startPriority,
            LanguageLevelPriorityEnum endPriority,
            string name)
        {
            var segments = GetSegmentsByLevelPriorities(segmentsAll, startPriority, endPriority);
            return SetSectionBySegments(segments, name);
        }
        
        private List<ScheduleSegment> GetSegmentsByLevelPriority(List<ScheduleSegment> segmentsAll,
            LanguageLevelPriorityEnum startPriority)
        {
           return segmentsAll
                .Where(s => s.LevelPriority >= (int) startPriority)
                .ToList();
        }

        private List<ScheduleSegment> GetSegmentsByLevelPriorities(List<ScheduleSegment> segmentsAll,
            LanguageLevelPriorityEnum startPriority,
            LanguageLevelPriorityEnum endPriority)
        {
            return segmentsAll
                .Where(s => s.LevelPriority >= (int) startPriority
                            && s.LevelPriority < (int) endPriority)
                .ToList();
        }

        private ScheduleSection SetSectionBySegments(List<ScheduleSegment> segments, string name)
        {
            if (segments.Count == 0)
            {
                return null;
            }

            var section = new ScheduleSection
            {
                ScheduleSegments = segments,
                Name = name
            };
            
            return Order(PutChildrenToSeparateSection(section));
        }

        private ScheduleSection PutChildrenToSeparateSection(ScheduleSection section)
        {
            var segments = new List<ScheduleSegment>();
            foreach (var segment in section.ScheduleSegments)
            {
                segments.AddRange(SplitSegmentByChildren(segment));
            }

            section.ScheduleSegments = segments;
            return section;
        }

        private List<ScheduleSegment> SplitSegmentByChildren(ScheduleSegment segment)
        {
            var groups = segment
                .ScheduleItems
                .GroupBy(it => it.Bunch)
                .Where(gr => gr.Any())
                .ToArray();

            var result = new List<ScheduleSegment>();

            foreach (var gr in groups)
            {
                
                result.Add(new ScheduleSegment
                    {
                        Level = segment.Level,
                        LevelInfo = GetLevelInfo(gr.Key, segment.LevelInfo),
                        LevelPriority = segment.LevelPriority,
                        ScheduleItems = gr
                    });
            }
            
            return result;
        }

        private string GetLevelInfo(BunchGroupEnum bunch, string info)
        {
            switch (bunch)
            {
                case BunchGroupEnum.Catalan :
                    return $"{info}, {catalanTitle}";
                case BunchGroupEnum.Children :
                    return $"{info}, {childrenTitle}";
                default: return info;
            }
        }

        private ScheduleSection Order(ScheduleSection section)
        {
            section.ScheduleSegments = section.ScheduleSegments.OrderBy(s => s.LevelPriority);
            foreach (var segment in section.ScheduleSegments)
            {
                segment.ScheduleItems = segment
                    .ScheduleItems
                    .OrderBy(sch => sch.BunchPriority)
                    .ThenBy(sch => sch.TimePriority);
            }
            return section;
        }

        public async Task<List<ScheduleSegment>> GetListByLevels(string[] levelNames)
        {
            return await _scheduleProvider.GetSchedulesByLevels(levelNames);
        }
    }
}