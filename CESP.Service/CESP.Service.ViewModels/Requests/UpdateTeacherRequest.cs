using System.Collections.Generic;

namespace CESP.Service.ViewModels.Requests
{
    public sealed class UpdateTeacherRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Post { get; set; }
        public string ShortInfo { get; set; }
        public string City { get; set; }
        public string Info { get; set; }
        public int Rang { get; set; }
        
        public string Photo { get; set; }
        public string SmallPhoto { get; set; }
        public string LargePhoto { get; set; }

        public List<string> Languages { get; set; }
    }
}