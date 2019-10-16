using System;

namespace CESP.Service.ViewModels.Responses
{
    public class FeedbackResponse
    {
        public string Message { get; set; }

        public DateTime Date { get; set; }

        public string Signature { get; set; }
        
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Source { get; set; }

        public string Photo { get; set; }
    }
}