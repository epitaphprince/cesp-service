using System;

namespace CESP.Core.Models
{
    public class Feedback
    {
        public string Message { get; set; }

        public DateTime Date { get; set; }

        public string Signature { get; set; }

        public string Source { get; set; }
        
        public string Photo { get; set; }
    }
}