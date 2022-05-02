using Newtonsoft.Json;

namespace ProjectSaturn.Models
{
    public class Certifications // Certifications that the user has recieved
    {
        // Certification
        public string Certification { get; set; }
        public DateTime? Date { get; set; }
        public bool Completed { get; set; }


        // Certifications Constructor
        [JsonConstructor]
        public Certifications(string Certification, DateTime? Date, bool Completed)
        {
            // Certification
            this.Certification = Certification;
            this.Date = Date;
            this.Completed = Completed;
        }
    }
}
