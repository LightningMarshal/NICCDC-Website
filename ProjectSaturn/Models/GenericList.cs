using Newtonsoft.Json;

namespace ProjectSaturn.Models
{
    public class GenericList // This is used to allow the Json Deserilalizer to get a list of Json strings
    {
        public List<string> Strings { get; set; }

        // Generic List Constructor
        [JsonConstructor]
        public GenericList(List<string> Strings)
        {
            this.Strings = Strings;
        }
    }
}
