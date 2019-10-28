using System;

namespace CESP.Service.ViewModels.Requests
{
    public class AddSpeakingClubRequest
    {
        public string SysName { get; set; }
        public string Name { get; set; }
        public string ShortInfo { get; set; }
        public string Info { get; set; }
        public DateTime Date { get; set; }
        public string TeacherName { get; set; }
        public string LanguageLevelName { get; set; }
    }
}