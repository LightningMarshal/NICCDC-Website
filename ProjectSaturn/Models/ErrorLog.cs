namespace ProjectSaturn.Models
{
    public class ErrorLog //This will log errors to be printed by the middleware
    {
        public static List<string> Msglist { get; set; }

        static ErrorLog()
        {
            Msglist = new List<string>();
        }
    }
}