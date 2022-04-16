using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Airport_Database.Models
{
    public partial class ExpertiseIn
    {
        public int Ssn { get; set; }
        public int ModelNo { get; set; }

        public virtual Model ModelNoNavigation { get; set; }
        public virtual Employee SsnNavigation { get; set; }
    }
}
