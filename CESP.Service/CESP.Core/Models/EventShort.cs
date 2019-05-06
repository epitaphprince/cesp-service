using System;

namespace CESP.Core.Models
{
    public class EventShort
    {
        public string SysName { get; set; }
        public string Name { get; set; }
        public string ShortInfo { get; set; }

        public DateTime Start { get; set; }
        public DateTime? End { get; set; }
        
        public string Photo { get; set; }
    }
}