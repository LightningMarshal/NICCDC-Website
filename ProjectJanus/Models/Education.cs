using Newtonsoft.Json;

namespace ProjectJanus.Models
{
    public class Education // Education portion of the application form
    {
        // General
        public string Name { get; set; }
        public string Major { get; set; }
        public string DegreeEarned { get; set; }
        public DateTime? DegreeDate { get; set; }
        public List<string> SkillsGained { get; set; }
        // School Address
        public string SchoolCity { get; set; }
        public string SchoolState { get; set; }
        public string SchoolZip { get; set; }
        // GPA
        public decimal? OverallGPA { get; set; }
        public decimal? MajorGPA { get; set; }


        // Education Constructor
        [JsonConstructor]
        public Education(string Name, string Major, string DegreeEarned, DateTime? DegreeDate, List<string> SkillsGained, string SchoolState, string SchoolCity, string SchoolZip, decimal? OverallGPA, decimal? MajorGPA)
        {
            // General
            this.Name = Name;
            this.Major = Major;
            this.DegreeEarned = DegreeEarned;
            this.DegreeDate = DegreeDate;
            this.SkillsGained = SkillsGained;
            // School Address
            this.SchoolState = SchoolState;
            this.SchoolCity = SchoolCity;
            this.SchoolZip = SchoolZip;
            // GPA
            this.OverallGPA = OverallGPA;
            this.MajorGPA = MajorGPA;
        }
    }
}
