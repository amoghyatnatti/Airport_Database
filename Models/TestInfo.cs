using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Airport_Database.Models
{
    public partial class TestInfo
    {
        public TestInfo()
        {
            Test = new HashSet<Test>();
        }

        public int TestNo { get; set; }
        public string Name { get; set; }
        public int? MaxScore { get; set; }

        public virtual ICollection<Test> Test { get; set; }
    }
}
