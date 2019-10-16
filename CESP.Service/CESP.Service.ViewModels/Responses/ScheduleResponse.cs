using System;

namespace CESP.Service.ViewModels.Responses
{
    public class ScheduleResponse
    {
        public string Level { get; set; }

        public LessonTimeResponse[] LessonTimes { get; set; }

        public DateTime? StartDate { get; set; }

        public PriceResponse[] Prices { get; set; }

        public bool IsAvailable { get; set; }

        public GroupDurationResponse[] Durations { get; set; }
    }

    public class LessonTimeResponse
    {
        public string Day { get; set; }

        public TimeSpan StartTime { get; set; }

        public TimeSpan EndTime { get; set; }
    }

    public class GroupDurationResponse
    {
        public double Duration { get; set; }

        public string Unit { get; set; }
    }

    public class PriceResponse
    {
        public decimal Cost { get; set; }

        public string Period { get; set; }

        public string Currency { get; set; }
    }
}