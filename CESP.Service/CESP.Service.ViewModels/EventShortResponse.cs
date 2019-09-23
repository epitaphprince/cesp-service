using System;

namespace CESP.Service.ViewModels
{
    public class EventShortResponse
    {
        public string SysName { get; set; }
        public string Name { get; set; }
        public string ShortInfo { get; set; }

        public DateTime Start { get; set; }
        public DateTime? End { get; set; }

        public string Photo { get; set; }
    }
}