using System;

namespace CESP.Core.Models
{
    public class SpeakingClubMeeting
    {
        public string Name { get; set; }
        public string Info { get; set; }
        public DateTime Date { get; set; }
        public string Teacher { get; set; }
        public string MinLanguageLevel { get; set; }
        public string MaxLanguageLevel { get; set; }
        
        
        public string SysName { get; set; }
        public string ShortInfo { get; set; }
        public int? TeacherId { get; set; }

        public string FileName { get; set; }
        public string FileInfo { get; set; }
    }
}