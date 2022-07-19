using Newtonsoft.Json;

namespace ProjectJanus.Models
{
    public class Awards // Awards that the user has recieved
    {
        // Award
        public string Award { get; set; }
        public DateTime? EarnDate { get; set; }


        // Awards Constructor
        [JsonConstructor]
        public Awards(string Award, DateTime? EarnDate)
        {
            // Award
            this.Award = Award;
            this.EarnDate = EarnDate;
        }
    }
}
