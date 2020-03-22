using System;
using System.Collections.Generic;

namespace CESP.Core.Models
{
    public class ScheduleBlock
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public IEnumerable<ScheduleSegment> ScheduleSegments { get; set; }
    }

    public class ScheduleSegment
    {
        public string Title { get; set; }

        public int SortPriority { get; set; }
        
        public string Duration { get; set; }

        public decimal MaxPrice { get; set; }

        public decimal MinPrice { get; set; }

        public IEnumerable<ScheduleItem> ScheduleItems { get; set; }
    }

    public class ScheduleItem
    {
        public Teacher Teacher { get; set; }
        public string Days { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public DateTime? StartDate { get; set; }
        
        public decimal Price { get; set; }
        public decimal PriceWithoutDiscount { get; set; }

        public PaymentPeriodEnum PaymentPeriod { get; set; }
        public decimal Discount { get; set; }
        
        public bool IsAvailable { get; set; }
        
        public string BunchName { get; set; }
        public BunchGroupEnum Bunch { get; set; }
        public int BunchPriority { get; set; }
        public int TimePriority { get; set; }

        public Level LanguageLevel { get; set; }

        public string Duration { get; set; }
    }
}