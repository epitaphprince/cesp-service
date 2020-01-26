using System.Collections.Generic;
using System.Linq;
using CESP.Core.Models;

namespace CESP.Core.Managers.Schedulers
{
    public class SplitRuleByLevel : ISplitRule
    {
        public List<ScheduleSegment> Split(ScheduleSegment segment)
        {
            var groups = segment
                .ScheduleItems
                .GroupBy(gr => gr.LanguageLevel.Id);

            var result = new List<ScheduleSegment>();
            
            foreach (var gr in groups)
            {
                var level = gr.FirstOrDefault().LanguageLevel;
                result.Add(new ScheduleSegment
                {
                    Title = GetTitle(level),
                    SortPriority = level.Rang,
                    ScheduleItems = gr
                });
            }

            return result;
        }
        
        private string GetTitle(Level languageLevel)
        {
            var level = languageLevel.Name;
            var info = languageLevel.Info;
           
            

                if (string.IsNullOrWhiteSpace(level) 
                    && string.IsNullOrWhiteSpace(info))
                {
                    return null;
                }

                if (string.IsNullOrWhiteSpace(level))
                {
                    return info;
                }

                if (string.IsNullOrWhiteSpace(info))
                {
                    return level;
                }

                return $"{level}, {info}";
            
        }
    }
}