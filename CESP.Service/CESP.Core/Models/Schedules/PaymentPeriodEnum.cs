using System;

namespace CESP.Core.Models
{
    public enum PaymentPeriodEnum
    {
        Course,
        Month,
        Unknown
    }
    
    public static class PaymentPeriodEnumConverter
    {
        public static PaymentPeriodEnum ParsePaymentPeriodEnum(string paymentPeriod)
        {
            if (string.IsNullOrEmpty(paymentPeriod))
            {
                return PaymentPeriodEnum.Course;
            }

            if (string.Equals(paymentPeriod, "месяц", StringComparison.OrdinalIgnoreCase))
            {
                return PaymentPeriodEnum.Month;
            }


            return PaymentPeriodEnum.Unknown;
        }
    }
}