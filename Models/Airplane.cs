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

    public partial class AirplaneData
    {
        public AirplaneData(long r, int? m, int? c, int? w)
        {
            RegistrationNo = r;
            ModelNo = m;
            Capacity = c;
            Weight = w;
        }
        public long RegistrationNo { get; set; }
        public int? ModelNo { get; set; }
        public int? Capacity { get; set; }
        public int? Weight { get; set; }

        public virtual IEnumerator<AirplaneData> ModelNoNavigation { get; set; }
    }

    public partial class AirplaneTest
    {
        public AirplaneTest(int TestNo, long RegistrationNo, decimal? Score, decimal? MaxScore, DateTime Date, string TestName, string TechnicianName, int Ssn)
        {
            this.TestNo = TestNo;
            this.RegistrationNo = RegistrationNo;
            this.Score = Score;
            this.MaxScore = MaxScore;
            this.Date = Date;
            this.TestName = TestName;
            this.TechnicianName = TechnicianName;
            this.Ssn = Ssn;
        }
        public int TestNo { get; set; }
        public long RegistrationNo { get; set; }
        public decimal? Score { get; set; }
        public decimal? MaxScore { get; set; }
        public string TestName { get; set; }
        public DateTime Date { get; set; }
        public string TechnicianName { get; set; }
        public int Ssn { get; set; }
        public virtual IEnumerator<AirplaneTest> ModelNoNavigation { get; set; }
    }
}
