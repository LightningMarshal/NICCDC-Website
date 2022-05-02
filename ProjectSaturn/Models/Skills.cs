using Newtonsoft.Json;

namespace ProjectSaturn.Models
{
    public class Skills // Knowledge a user may have
    {
        // Skill
        public string Skill { get; set; }

        // Knowledge Constructor
        [JsonConstructor]
        public Skills(string Skill)
        {
            this.Skill = Skill;
        }
    }
}