using Newtonsoft.Json;

namespace ProjectSaturn.Models
{
    public class Professional //Place that a user worked at
    {
        public string Name { get; set; }
        public string Location { get; set; }
        public string PositionAtComp { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public List<string> SkillsGained { get; set; }


        //Professional Contructor
        [JsonConstructor]
        public Professional(string Name, string Location, DateTime? StartDate, DateTime? EndDate, List<string> SkillsGained, string PositionAtComp)
        {
            this.Name = Name;
            this.Location = Location;
            this.PositionAtComp = PositionAtComp;
            this.StartDate = StartDate;
            this.EndDate = EndDate;
            this.SkillsGained = SkillsGained;
        }
    }
}
