using Newtonsoft.Json;
using System.Data.SqlClient;

namespace ProjectJanus.Models
{
    public class Personal // Basic user information
    {
        // Name
        public string FirstName { get; set; }
        public string MiddleInit { get; set; }
        public string LastName { get; set; }
        // Address
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        // Contact Info
        public string Email { get; set; }
        public string DayPhone { get; set; }
        public string EveningPhone { get; set; }
        public string MobilePhone { get; set; }
        // Is US Citizen
        public bool IsUSCitizen { get; set; }


        // Personal Constructor
        public Personal() { }

        [JsonConstructor]
        public Personal(string FirstName, string MiddleInit, string LastName, string Address, string City, string State, string Zip, string Email,
            string DayPhone, string EveningPhone, string MobilePhone, bool IsUSCitizen)
        {
            // Name
            this.FirstName = FirstName;
            this.MiddleInit = MiddleInit;
            this.LastName = LastName;
            // Address
            this.Address = Address;
            this.City = City;
            this.State = State;
            this.Zip = Zip;
            // Contact Info
            this.Email = Email;
            this.DayPhone = DayPhone;
            this.EveningPhone = EveningPhone;
            this.MobilePhone = MobilePhone;
            // Is US Citizen
            this.IsUSCitizen = IsUSCitizen;
        }
    }
}
