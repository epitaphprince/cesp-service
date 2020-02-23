using System;

namespace CESP.Core.Models
{
    public class Course
    {
        public Course()
        {
            Prices = new decimal[0];
        }

        public string Name { get; set; }

        public string Description { get; set; }

        public string DurationInfo { get; set; }

        public string Photo { get; set; }

        public int? DiscountPercent { get; set; }

        public string Price { get; set; }
        
        public string PriceInfo { get; set; }

        public string CurrencyName { get; set; }

        public decimal[] Prices { get; set; }

        public string[] Icons { get; set; }
    }
}