using System.Collections.Generic;
using System.Linq;
using CESP.Core.Models;

namespace CESP.Core.Managers.Schedulers
{
    public class SplitRuleByBunch : ISplitRule
    {
        // коэфициент приоритета сортировки по этому правилу относительно других правил
        private const int coefPriority = 10;
        
        private const string childrenTitle = "Дети";
        private const string catalanTitle = "Каталанский";
        
        public List<ScheduleSegment> Split(ScheduleSegment  segment)
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
                    Title = GetTitle(gr.Key, segment.Title),
                    SortPriority = segment.SortPriority + gr.FirstOrDefault().BunchPriority * coefPriority,
                    ScheduleItems = gr
                });
            }
            
            return result;
        }
        
        private string GetTitle(BunchGroupEnum bunch, string info)
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
    }
}