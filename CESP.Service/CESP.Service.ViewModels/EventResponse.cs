using System;
using System.Collections.Generic;

namespace CESP.Service.ViewModels
{
    public class EventResponse
    {
        public string Name { get; set; }
        public string Info { get; set; }

        public DateTime Start { get; set; }
        public DateTime? End { get; set; }

        public List<string> Photos { get; set; }
    }
}