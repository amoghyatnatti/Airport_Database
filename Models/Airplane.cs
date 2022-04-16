using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Airport_Database.Models
{
    public partial class Airplane
    {
        public Airplane()
        {
            Test = new HashSet<Test>();
        }

        public long RegistrationNo { get; set; }
        public int? ModelNo { get; set; }

        public virtual Model ModelNoNavigation { get; set; }
        public virtual ICollection<Test> Test { get; set; }
    }
}
