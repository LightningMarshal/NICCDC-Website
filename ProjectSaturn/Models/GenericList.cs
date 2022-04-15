using Newtonsoft.Json;

namespace ProjectSaturn.Models
{
    public class GenericList //Awards that the user has recieved
    {
        public List<string> strings { get; set; }

        //Awards Constructor
        [JsonConstructor]
        public GenericList(List<string> strings)
        {
            this.strings = strings;
        }
    }
}
