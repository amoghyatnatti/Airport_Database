using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Airport_Database.Models
{
    public partial class Employee
    {
        public Employee()
        {
            ExpertiseIn = new HashSet<ExpertiseIn>();
        }

        public int Ssn { get; set; }
        public string Name { get; set; }
        public decimal? Salary { get; set; }
        public long? UnionMemNo { get; set; }
        public long? PhoneNo { get; set; }
        public string Street { get; set; }
        public int? Zip { get; set; }
        public string State { get; set; }
        public string City { get; set; }

        public virtual Technician Technician { get; set; }
        public virtual TrafficController TrafficController { get; set; }
        public virtual ICollection<ExpertiseIn> ExpertiseIn { get; set; }
    }
}
