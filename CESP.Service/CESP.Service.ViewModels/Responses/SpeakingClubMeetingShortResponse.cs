using System;

namespace CESP.Service.ViewModels.Responses
{
    public class SpeakingClubMeetingShortResponse
    {
        public string SysName { get; set; }

        public string Name { get; set; }

        public string ShortInfo { get; set; }

        public DateTime Date { get; set; }

        public string Teacher { get; set; }

        public string MinLanguageLevel { get; set; }

        public string MaxLanguageLevel { get; set; }

        public string Photo { get; set; }
    }
}