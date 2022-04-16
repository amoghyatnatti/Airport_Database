using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Airport_Database.Models
{
    public partial class Test
    {
        public int TestNo { get; set; }
        public long RegistrationNo { get; set; }
        public int? Ssn { get; set; }
        public DateTime Date { get; set; }
        public int? NoHours { get; set; }
        public decimal? Score { get; set; }

        public virtual Airplane RegistrationNoNavigation { get; set; }
        public virtual Technician SsnNavigation { get; set; }
        public virtual TestInfo TestNoNavigation { get; set; }
    }
}
