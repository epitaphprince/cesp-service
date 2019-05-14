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
    }
}