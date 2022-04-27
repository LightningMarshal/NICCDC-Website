using Newtonsoft.Json;

namespace ProjectSaturn.Models
{
    public class Certifications //Awards that the user has recieved
    {
        public string Desc { get; set; }
        public DateTime? Date { get; set; }
        public bool Completed { get; set; }


        //Awards Constructor
        [JsonConstructor]
        public Certifications(string Desc, DateTime? Date, bool Completed)
        {
            this.Desc = Desc;
            this.Date = Date;
            this.Completed = Completed;
        }
    }
}
