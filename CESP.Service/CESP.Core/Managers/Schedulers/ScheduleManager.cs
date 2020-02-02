using System;
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
            _splitRuleByBunch = new SplitRuleByBunch();
            _splitRuleByLevel = new SplitRuleByLevel();
        }

        public async Task<List<ScheduleBlock>> GetList()
        {
            var items = await _scheduleProvider.GetScheduleItems();

            var result = new List<ScheduleBlock>();
            result.Add(SetBlockByLevelLanguage(items,
                LanguageLevelPriorityEnum.A1,
                LanguageLevelPriorityEnum.A2,
                $"{title} A1"));

            result.Add(SetBlockByLevelLanguage(items,
                LanguageLevelPriorityEnum.A2,
                LanguageLevelPriorityEnum.B1,
                $"{title} A2"));

            result.Add(SetBlockByLevelLanguage(items,
                LanguageLevelPriorityEnum.B1,
                LanguageLevelPriorityEnum.C1,
                $"{title} B"));

            result.Add(SetBlockByLevelLanguage(items,
                LanguageLevelPriorityEnum.C1,
                $"{title} C"));

            return result;
        }

        public async Task<List<ScheduleSegment>> GetListByLevels(string[] levelNames)
        {
            var items = await _scheduleProvider.GetScheduleItemsByLevels(levelNames);

            var result = BuildLanguageLevelBlock(items, "");

            return result.ScheduleSegments.ToList();
        }

        private ScheduleBlock SetBlockByLevelLanguage(List<ScheduleItem> itemsAll,
            LanguageLevelPriorityEnum startPriority,
            string name)
        {
            var items = itemsAll
                .Where(s => s.LanguageLevel.Rang >= (int) startPriority)
                .ToList();

            return BuildLanguageLevelBlock(items, name);
        }

        private ScheduleBlock SetBlockByLevelLanguage(List<ScheduleItem> itemsAll,
            LanguageLevelPriorityEnum startPriority,
            LanguageLevelPriorityEnum endPriority,
            string name)
        {
            var items = itemsAll
                .Where(s => s.LanguageLevel.Rang >= (int) startPriority
                            && s.LanguageLevel.Rang < (int) endPriority)
                .ToList();

            return BuildLanguageLevelBlock(items, name);
        }

        private ScheduleBlock BuildLanguageLevelBlock(List<ScheduleItem> items, string name)
        {
            if (items.Count == 0)
            {
                return null;
            }

            var llBlock = CreateLanguageLevelBlock(items, name);
            llBlock = SeparateSection(llBlock, _splitRuleByLevel);
            llBlock = SeparateSection(llBlock, _splitRuleByBunch);

            foreach (var segment in llBlock.ScheduleSegments)
            {
                SetScheduleSegmentAgr(segment);
            }

            llBlock = Order(llBlock);
            return llBlock;
        }

        private void SetScheduleSegmentAgr(ScheduleSegment segment)
        {
            var query = segment
                .ScheduleItems
                .Where(it => it.PaymentPeriod == PaymentPeriodEnum.Course);
            if (query.Any())
            {
                segment.MinPrice = query.Min(it => it.Price);
                segment.MaxPrice = query.Max(it => it.Price);
            }

            segment.Duration = string.Join(", ",
                segment
                    .ScheduleItems
                    .Select(it => it.Duration)
                    .Distinct());
        }

        private ScheduleBlock CreateLanguageLevelBlock(List<ScheduleItem> items, string name)
        {
            var baseSegment = new ScheduleSegment
            {
                ScheduleItems = items,
            };

            return new ScheduleBlock
            {
                ScheduleSegments = new[] {baseSegment},
                Name = name
            };
        }

        private ScheduleBlock SeparateSection(ScheduleBlock block, ISplitRule splitRule)
        {
            var segments = new List<ScheduleSegment>();
            foreach (var segment in block.ScheduleSegments)
            {
                segments.AddRange(splitRule.Split(segment));
            }

            block.ScheduleSegments = segments;
            return block;
        }


        private ScheduleBlock Order(ScheduleBlock block)
        {
            // sort segments
            block.ScheduleSegments = block
                .ScheduleSegments
                .OrderBy(s => s.SortPriority);

            // sort items
            foreach (var segment in block.ScheduleSegments)
            {
                segment.ScheduleItems = segment
                    .ScheduleItems
                    .OrderBy(sch => sch.BunchPriority)
                    .ThenBy(sch => sch.TimePriority);
            }

            return block;
        }

        private SplitRuleByBunch _splitRuleByBunch;
        private SplitRuleByLevel _splitRuleByLevel;
    }
}