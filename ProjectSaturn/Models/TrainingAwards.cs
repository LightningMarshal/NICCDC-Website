using Newtonsoft.Json;

namespace ProjectSaturn.Models
{
    public class TrainingAwards //Training or Awards that the user has taken/recieved
    {
        public string Desc { get; set; }
        public DateTime? Date { get; set; }



        //Extra Constructor
        [JsonConstructor]
        public TrainingAwards(string Desc, DateTime? Date)
        {
            this.Desc = Desc;
            this.Date = Date;
        }
    }
}
