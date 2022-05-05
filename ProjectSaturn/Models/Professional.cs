using Newtonsoft.Json;

namespace ProjectSaturn.Models
{
    public class Professional //Place that a user worked at
    {
        // Profession
        public string Name { get; set; }
        public string Position { get; set; }
        public List<string> SkillsGained { get; set; }
        // Location
        public string ProfessionCity { get; set; }
        public string ProfessionState { get; set; }
        // Date
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }


        //Professional Contructor
        [JsonConstructor]
        public Professional(string Name, string Position, List<string> SkillsGained, string ProfessionCity, string ProfessionState, DateTime? StartDate, DateTime? EndDate)
        {
            // Profession
            this.Name = Name;
            this.Position = Position;
            this.SkillsGained = SkillsGained;
            // Location
            this.ProfessionCity = ProfessionCity;
            this.ProfessionState = ProfessionState;
            // Date
            this.StartDate = StartDate;
            this.EndDate = EndDate;
        }
    }
}
