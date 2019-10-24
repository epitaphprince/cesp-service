using System;
using System.Collections.Generic;

namespace CESP.Service.ViewModels.Requests
{
    public class AddEventRequest
    {
        public string SysName { get; set; }
        public string Name { get; set; }
        public string Info { get; set; }
        public DateTime Start { get; set; }
        public DateTime? End { get; set; }
    }
}