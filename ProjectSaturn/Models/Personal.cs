using Newtonsoft.Json;
using System.Data.SqlClient;
using System.ComponentModel.DataAnnotations;

namespace ProjectSaturn.Models
{
    public class Personal // Basic user information
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string Goal { get; set; }


        // Personal Constructor
        public Personal() { }

        [JsonConstructor]
        public Personal(string FirstName, string LastName, string Email, string PhoneNumber, string Address, string Goal)
        {
            this.FirstName = FirstName;
            this.LastName = LastName;
            this.Email = Email;
            this.PhoneNumber = PhoneNumber;
            this.Address = Address;
            this.Goal = Goal;
        }


        // Database Interaction
        public Personal(SqlDataReader dr)
        {
            Fill(dr);
        }

        public void Fill(SqlDataReader dr)
        {
            this.FirstName = (string)dr["FirstName"];
            this.LastName = (string)dr["LastName"];
            this.Email = (string)dr["EmailAddress"];
            this.PhoneNumber = (string)dr["PhoneNumber"];
            this.Address = (string)dr["Address"];
            this.Goal = (string)dr["Goal"];
        }
    }
}
