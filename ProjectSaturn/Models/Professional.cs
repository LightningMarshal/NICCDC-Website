using Newtonsoft.Json;

namespace ProjectSaturn.Models
{
    public class Professional //Place that a user worked at
    {
        // Profession
        public string Name { get; set; }
        public string Location { get; set; }
        public string Position { get; set; }
        public List<string> SkillsGained { get; set; }
        // Date
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }


        //Professional Contructor
        [JsonConstructor]
        public Professional(string Name, string Location, List<string> SkillsGained, string Position, DateTime? StartDate, DateTime? EndDate)
        {
            // Profession
            this.Name = Name;
            this.Location = Location;
            this.Position = Position;
            this.SkillsGained = SkillsGained;
            // Date
            this.StartDate = StartDate;
            this.EndDate = EndDate;
        }
    }
}
