using System;

namespace CESP.Core.Models
{
    public class Schedule
    {
        public string Level { get; set; }

        public LessonTime[] LessonTimes { get; set; }

        public DateTime StartDate { get; set; }

        public Price[] Prices { get; set; }
        
        public bool IsAvailable { get; set; }
        
        public GroupDuration[] Durations { get; set; }
    }
}