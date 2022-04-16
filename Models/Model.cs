using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Airport_Database.Models
{
    public partial class Model
    {
        public Model()
        {
            Airplane = new HashSet<Airplane>();
            ExpertiseIn = new HashSet<ExpertiseIn>();
        }

        public int ModelNo { get; set; }
        public int? Capacity { get; set; }
        public int? Weight { get; set; }

        public virtual ICollection<Airplane> Airplane { get; set; }
        public virtual ICollection<ExpertiseIn> ExpertiseIn { get; set; }
    }
}
