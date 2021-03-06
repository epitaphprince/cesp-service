namespace CESP.Core.Models
{
    public class Teacher
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Post { get; set; }
        public string City { get; set; }
        public string Info { get; set; }
        public string ShortInfo { get; set; }
        public string Photo { get; set; }
        public string SmallPhoto { get; set; }
        public string LargePhoto { get; set; }
        public Language[] Languages { get; set; }
    }
}