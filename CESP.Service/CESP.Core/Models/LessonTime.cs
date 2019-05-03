using System;

namespace CESP.Core.Models
{
    public class LessonTime
    {
        public string Day { get; set; }

        public TimeSpan StartTime { get; set; }
        
        public TimeSpan EndTime { get; set; }
    }
}