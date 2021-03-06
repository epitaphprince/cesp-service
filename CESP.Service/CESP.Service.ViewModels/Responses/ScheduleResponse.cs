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

        public string Duration { get; set; }

        public decimal MaxPrice { get; set; }

        public decimal MinPrice { get; set; }
    }
    

    public class ScheduleItemResponse
    {
        public TeacherResponse Teacher { get; set; }

        public string Days { get; set; }
        public string StartTime { get; set; }

        public string EndTime { get; set; }

        public DateTime? StartDate { get; set; }
        
        public decimal Price { get; set; }
        
        public decimal PriceWithoutDiscount { get; set; }
        
        public decimal Discount { get; set; }

        public bool IsAvailable { get; set; }
    }
}