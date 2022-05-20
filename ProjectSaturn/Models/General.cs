using Newtonsoft.Json;
//using System.Data.SqlClient;    Uncomment if using the SQL data reader at the bottom of this page

namespace ProjectSaturn.Models
{
    public class General // All data found between the Personal Info and Previous Colleges in the Application Form
    {
        // General
        public string Degree { get; set; }
        public string DegreeStatus { get; set; }
        public DateTime? AntiGradDate { get; set; } // Anticipated Graduation Date
        // GPA
        public decimal? OverallGPA  { get; set; }
        public decimal? MajorGPA { get; set; }
        // SAT
        public Int16? SATV { get; set; }
        public Int16? SATM { get; set; }
        // ACT
        public Int16? ACTV { get; set; }
        public Int16? ACTM { get; set; }
        // GRE
        public Int16? GREV { get; set; }
        public Int16? GREQ { get; set; }
        public Int16? GREA { get; set; }


        // General Constructor
        public General() { }

        [JsonConstructor]
        public General(string Degree, string DegreeStatus, DateTime? AntiGradDate, decimal? OverallGPA, decimal? MajorGPA,
            Int16? SATV, Int16? SATM, Int16? ACTV, Int16? ACTM, Int16? GREV, Int16? GREQ, Int16? GREA)
        {
            // General
            this.Degree = Degree;
            this.DegreeStatus = DegreeStatus;
            this.AntiGradDate = AntiGradDate;
            // GPA
            this.OverallGPA = OverallGPA;
            this.MajorGPA = MajorGPA;
            // SAT
            this.SATV = SATV;
            this.SATM = SATM;
            // ACT
            this.ACTV = ACTV;
            this.ACTM = ACTM;
            // GRE
            this.GREV = GREV;
            this.GREQ = GREQ;
            this.GREA = GREA;
        }
    }
}
