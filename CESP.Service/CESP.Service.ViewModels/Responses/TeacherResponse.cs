namespace CESP.Service.ViewModels.Responses
{
    public class TeacherResponse
    {
        public string Name { get; set; }
        public string Position { get; set; }
        public string Info { get; set; }
        public string City { get; set; }
        public string ShortInfo { get; set; }
        public string Photo { get; set; }
        public string SmallPhoto { get; set; }
        public string LargePhoto { get; set; }
        public LanguageResponse[] Languages { get; set; }
    }
}