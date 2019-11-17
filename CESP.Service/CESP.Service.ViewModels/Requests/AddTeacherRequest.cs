namespace CESP.Service.ViewModels.Requests
{
    public sealed class AddTeacherRequest
    {
        public string Name { get; set; }
        public string Post { get; set; }
        public string ShortInfo { get; set; }
        public string City { get; set; }
        public string Info { get; set; }
        public int Rang { get; set; }
    }
}