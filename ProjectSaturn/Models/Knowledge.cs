using Newtonsoft.Json;

namespace ProjectSaturn.Models
{
    public class Knowledge // Knowledge a user may have
    {
        public string SkillDesc { get; set; }

        // Knowledge Constructor
        [JsonConstructor]
        public Knowledge(string SkillDesc)
        {
            this.SkillDesc = SkillDesc;
        }
    }
}