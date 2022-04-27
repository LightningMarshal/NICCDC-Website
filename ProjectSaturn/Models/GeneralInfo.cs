using Newtonsoft.Json;
using System.Data.SqlClient;

namespace ProjectSaturn.Models
{
    public class GeneralInfo // All data found between the Personal Info and Previous Colleges in the Application Form
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


        // GeneralInfo Constructor
        public GeneralInfo() { }

        [JsonConstructor]
        public GeneralInfo(string Degree, string DegreeStatus, DateTime? AntiGradDate, decimal? OverallGPA, decimal? MajorGPA,
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


        //TODO : The following is to read database entries in the DAL
        // IF the Analysis will be taking place in this application, this will be 
        // of great use!

        // Uncomment to utilize

        // Database Interaction
        //public GeneralInfo(SqlDataReader dr)
        //{
        //    Fill(dr);
        //}

        //public void Fill(SqlDataReader dr)
        //{
        //    // General
        //    this.Degree = (string)dr["Degree"];
        //    this.DegreeStatus = (string)dr["DegreeStatus"];
        //    this.AntiGradDate = (DateTime)dr["AntiGradDate"];
        //    // GPA
        //    this.OverallGPA = (decimal)dr["OverallGPA"];
        //    this.MajorGPA = (decimal)dr["MajorGPA"];
        //    // SAT
        //    this.SATV = (Int16?)dr["SATV"];
        //    this.SATM = (Int16?)dr["SATM"];
        //    // ACT
        //    this.ACTV = (Int16?)dr["ACTV"];
        //    this.ACTM = (Int16?)dr["ACTM"];
        //    // GRE
        //    this.GREV = (Int16?)dr["GREV"];
        //    this.GREQ = (Int16?)dr["GREQ"];
        //    this.GREA = (Int16?)dr["GREA"];
        //}
    }
}
