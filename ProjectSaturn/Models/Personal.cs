using Newtonsoft.Json;
using System.Data.SqlClient;

namespace ProjectSaturn.Models
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


        //TODO : The following is to read database entries in the DAL
        // IF the Analysis will be taking place in this application, this will be 
        // of great use!

        //Uncomment to Utilize

        //// Database Interaction
        //public Personal(SqlDataReader dr)
        //{
        //    Fill(dr);
        //}

        //public void Fill(SqlDataReader dr)
        //{
        //    // Name
        //    this.FirstName = (string)dr["FirstName"];
        //    this.MiddleInit = (string)dr["MiddleInit"];
        //    this.LastName = (string)dr["LastName"];
        //    // Address
        //    this.Address = (string)dr["Address"];
        //    this.City = (string)dr["City"];
        //    this.State = (string)dr["State"];
        //    this.Zip = (string)dr["Zip"];
        //    // Contact Info
        //    this.Email = (string)dr["Email"];
        //    this.DayPhone = (string)dr["DayPhone"];
        //    this.EveningPhone = (string)dr["EveningPhone"];
        //    this.MobilePhone = (string)dr["MobilePhone"];
        //    // Is US Citizen
        //    this.IsUSCitizen = (bool)dr["IsUSCitizen"];
        //}
    }
}
