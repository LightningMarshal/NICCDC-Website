using Newtonsoft.Json;

namespace ProjectSaturn.Models
{
    public class Knowledge // Knowledge a user may have
    {
        public string Desc { get; set; }

        // Knowledge Constructor
        [JsonConstructor]
        public Knowledge(string Desc)
        {
            this.Desc = Desc;
        }
    }
}