using System;

namespace CESP.Service.ViewModels.Responses
{
    public class ScheduleResponse
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public ScheduleSegmentResponse[] ScheduleSegments { get; set; }
    }

    public class ScheduleSegmentResponse
    {
        public string Level { get; set; }
        
        public ScheduleItemResponse[] ScheduleItems { get; set; }
    }
    

    public class ScheduleItemResponse
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