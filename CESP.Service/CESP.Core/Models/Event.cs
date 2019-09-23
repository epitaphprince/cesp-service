using System;
using System.Collections.Generic;

namespace CESP.Core.Models
{
    public class Event
    {
        public string Name { get; set; }
        public string Info { get; set; }

        public DateTime Start { get; set; }
        public DateTime? End { get; set; }

        public List<string> Photos { get; set; }
    }
}