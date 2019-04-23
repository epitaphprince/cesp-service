namespace CESP.Core.Models
{
    public class PriceInfo
    {
        public decimal Cost { get; set; }

        public string CostInfo { get; set; }
        public int? DiscountPercent { get; set; }
        public string DiscountInfo { get; set; }

        public string PaymentPeriod { get; set; }

        public string Currency { get; set; }
    }
}