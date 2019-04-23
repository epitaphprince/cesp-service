using System.Collections.Generic;

namespace CESP.Core.Models
{
    public class Course
    {
        public string Name { get; set; }
        
        public string Description { get; set; }

        public string DurationInfo { get; set; }

        public string Photo { get; set; }
        
        public int DiscountPer { get; set; }
        
        public string CostInfo { get; set; }
    }
}