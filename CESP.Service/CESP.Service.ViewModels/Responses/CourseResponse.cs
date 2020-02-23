namespace CESP.Service.ViewModels.Responses
{
    public class CourseResponse
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string DurationInfo { get; set; }

        public string Photo { get; set; }

        public int? DiscountPercent { get; set; }

        public string Price { get; set; }

        public string[] Icons { get; set; }
    }
}