using Newtonsoft.Json;

namespace ProjectSaturn.Models
{
    public class Education //Place that a user was educated at
    {
        public string Name { get; set; }
        public string Location { get; set; }
        public Decimal? GPA { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public List<string> SkillsGained { get; set; }

        //Education Constructor
        [JsonConstructor]
        public Education(string Name, string Location, Decimal? GPA, DateTime? StartDate, DateTime? EndDate, List<string> SkillsGained)
        {
            this.Name = Name;
            this.Location = Location;
            this.GPA = GPA;
            this.StartDate = StartDate;
            this.EndDate = EndDate;
            this.SkillsGained = SkillsGained;
        }
    }
}
