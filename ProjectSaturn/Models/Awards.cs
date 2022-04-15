using Newtonsoft.Json;

namespace ProjectSaturn.Models
{
    public class Awards //Awards that the user has recieved
    {
        public string Desc { get; set; }
        public DateTime? Date { get; set; }



        //Awards Constructor
        [JsonConstructor]
        public Awards(string Desc, DateTime? Date)
        {
            this.Desc = Desc;
            this.Date = Date;
        }
    }
}
