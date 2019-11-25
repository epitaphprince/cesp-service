using System;
using System.Collections.Generic;

namespace CESP.Core.Models
{
    public class ScheduleSection
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public IEnumerable<ScheduleSegment> ScheduleSegments { get; set; }
    }

    public class ScheduleSegment
    {
        public string Level { get; set; }
        
        public string LevelInfo { get; set; }
        
        public int LevelRang { get; set; }

        public IEnumerable<ScheduleItem> ScheduleItems { get; set; }
    }

    public class ScheduleItem
    {
        public string TeacherPhoto { get; set; }

        public string TeacherName { get; set; }

        public string TeacherPost { get; set; }

        public string Days { get; set; }

        public TimeSpan StartTime { get; set; }

        public TimeSpan EndTime { get; set; }

        public DateTime? StartDate { get; set; }
        
        public decimal Price { get; set; }
        
        public decimal PriceWithoutDiscount { get; set; }
        
        public decimal Discount { get; set; }

        public bool IsAvailable { get; set; }
    }
}