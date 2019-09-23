using System.Collections.Generic;

namespace CESP.Core.Models
{
    public class Partner
    {
        public string SysName { get; set; }

        public string Name { get; set; }

        public string Url { get; set; }

        public string Info { get; set; }

        public string Email { get; set; }

        public string Address { get; set; }

        public string Phone { get; set; }

        public string SocialNetwork { get; set; }

        public string Photo { get; set; }

        public List<string> Photos { get; set; }
    }
}