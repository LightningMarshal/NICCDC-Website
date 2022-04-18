using Newtonsoft.Json;

namespace ProjectSaturn.Models
{
    public class GenericList //Awards that the user has recieved
    {
        public List<string> Strings { get; set; }

        //Awards Constructor
        [JsonConstructor]
        public GenericList(List<string> Strings)
        {
            this.Strings = Strings;
        }
    }
}
