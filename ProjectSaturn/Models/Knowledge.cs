using Newtonsoft.Json;

namespace ProjectSaturn.Models
{
    public class Knowledge //Knowledge a user may have
    {
        public string Catagory { get; set; }
        public string SkillDesc { get; set; }

        //Knowledge Constructor
        [JsonConstructor]
        public Knowledge(string Catagory, string SkillDesc)
        {
            this.Catagory = Catagory;
            this.SkillDesc = SkillDesc;
        }
    }
}