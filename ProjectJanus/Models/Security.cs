namespace ProjectJanus.Models
{
    /*  
     *  This will allow the credentials from the secrets.json file to be read. The aforementioned file must be created
     *  locally in order to access the credentials. Please reach out to your supervisor for details reguarding these
     *  credentials
     */
    public class Security
    {
        public string senderEmail { get; set; }
        public string senderPassword { get; set; }
        public string recieverEmail { get; set; }
        public string recieverName { get; set; }
        public string smtpHost { get; set; }
        public int smtpPort { get; set; }
    }
}
