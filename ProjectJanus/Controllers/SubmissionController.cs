using Newtonsoft.Json;
using ProjectJanus.Models;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Net.Mail;
using System.Net;

namespace ProjectJanus.Controllers
{
    public class SubmissionController : Controller
    {
        private readonly IOptions<Security> _config;
        public SubmissionController(IOptions<Security> config)
        {
            _config = config;
        }

        public dynamic Mail()
        {
            // Retrieve all of the user data
            List<Personal> PersonalList = new();
            dynamic personal = Checker("Personal", PersonalList);
            List<General> GeneralList = new();
            dynamic general = Checker("General", GeneralList);
            List<Education> EducationList = new();
            dynamic education = Checker("Education", EducationList);
            List<Professional> ProfessionList = new();
            dynamic professional = Checker("Professional", ProfessionList);
            List<Certifications> CertificationList = new();
            dynamic certification = Checker("Certification", CertificationList);
            List<Skills> SkillsList = new();
            dynamic skills = Checker("Skill", SkillsList);
            List<Awards> AwardsList = new();
            dynamic awards = Checker("Award", AwardsList);

            // Email of applicant
            string applicant = HttpContext.Request.Cookies["user"];

            // Subject of Email (user's email is added for easy identification)
            string subject = "SFS Applicant: " + applicant;

            // All of the application information
            string message = ""; 

            // Build the Message:
            if (personal != null)
            {
                message = message + "-----------------------------------------------Personal--------------------------------------------------\n" + GetPersonalMessage(personal) + "\n";
            }
            if (general != null)
            {
                message = message + "------------------------------------------------General--------------------------------------------------\n" + GetGeneralMessage(general) + "\n";
            }
            if (education != null)
            {
                message = message + "-----------------------------------------------Education-------------------------------------------------\n" + GetEducationMessage(education) + "\n";
            }
            if (professional != null)
            {
                message = message + "-----------------------------------------------Profession------------------------------------------------\n" + GetProfessionMessage(professional) + "\n";
            }
            if (certification != null)
            {
                message = message + "---------------------------------------------Certifications----------------------------------------------\n" + GetCertificationMessage(certification) + "\n";
            }
            if (skills != null)
            {
                message = message + "-------------------------------------------------Skills--------------------------------------------------\n" + GetSkillsMessage(skills) + "\n";
            }
            if (awards != null)
            {
                message = message + "-------------------------------------------------Awards--------------------------------------------------\n" + GetAwardsMessage(awards) + "\n";
            }
            
            // Mailing Functionality
            Debug.WriteLine(message);

            var security = _config.Value;

            try
            {
                if (ModelState.IsValid)
                {
                    // To change the credentials, please input credentials into secrets.json as provided by your supervisor.
                    var senderEmail = new MailAddress(security.senderEmail, "Application");
                    var receiverEmail = new MailAddress(security.recieverEmail, security.recieverName);
                    var sub = subject;
                    var body = "<pre>" + message + "</pre>";
                    var smtp = new SmtpClient
                    {
                        Host = security.smtpHost,
                        Port = security.smtpPort,
                        EnableSsl = true,
                        DeliveryMethod = SmtpDeliveryMethod.Network,
                        UseDefaultCredentials = false,
                        Credentials = new NetworkCredential(senderEmail.Address, security.senderPassword)
                    };
                    using (var mess = new MailMessage(senderEmail, receiverEmail)
                    {
                        IsBodyHtml = true,
                        Subject = subject,
                        Body = body
                    })
                    {
                        smtp.Send(mess);
                    }

                    // Delete Cookies
                    HttpContext.Response.Cookies.Delete("user");
                    HttpContext.Response.Cookies.Delete("Personal");
                    HttpContext.Response.Cookies.Delete("General");
                    HttpContext.Response.Cookies.Delete("Education");
                    HttpContext.Response.Cookies.Delete("Professional");
                    HttpContext.Response.Cookies.Delete("Certification");
                    HttpContext.Response.Cookies.Delete("Skill");
                    HttpContext.Response.Cookies.Delete("Award");

                    return RedirectToAction("Finished", "Application");
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }

            return Json("error");
        }

        // This returns either a list of objects or null. It allows for the same value to be checked for null and used as a list.
        private dynamic Checker<T>(string cookie, List<T> t)
        {
            var settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
            };

            string jsonString = HttpContext.Request.Cookies[cookie];
            if (jsonString != null)
            {
                t = JsonConvert.DeserializeObject<List<T>>(jsonString, settings);
                return t;
            }
            return null;
        }
        
        // Gets the personal portion of the message
        private static string GetPersonalMessage(List<Personal> PersonalList)
        {
            string retval = "";
            foreach (Personal person in PersonalList)
            {           
                // Name and Email Address
                retval += String.Format($"{"Name: " + person.FirstName + " " + person.MiddleInit + " " + person.LastName, -60} {"Email: " + person.Email, -60}\n");

                // Day and Night Phones
                retval += String.Format($"{"Day Phone: " + person.DayPhone,-60} {"Evening Phone: " + person.EveningPhone,-60}\n");

                // Mobile Phone and US Citizen
                retval += String.Format($"{"Mobile Phone: " + person.MobilePhone,-60} {"Is US Citizen: " + person.IsUSCitizen,-60}\n");

                // Address, City, State, and Zip
                retval += String.Format($"{"Address: " + person.Address}{", " + person.City}{", " + person.State}{" " + person.Zip}\n");
                
                // Space
                retval += "\n";
            }
            return retval;
        }

        // Gets the general portion of the message
        private static string GetGeneralMessage(List<General> GeneralList)
        {
            string retval = "";
            foreach (General general in GeneralList)
            {
                // Degree and Degree Status
                retval += String.Format($"{"Degree: " + general.Degree,-60} {"Degree Status: " + general.DegreeStatus,-60}\n");

                // Graduation Date and Overall GPA
                retval += String.Format($"{"Graduation Date: " + general.AntiGradDate,-60} {"Overall GPA: " + general.OverallGPA,-60}\n");

                // Major GPA and SAT Verbal
                retval += String.Format($"{"Major GPA: " + general.MajorGPA,-60} {"SATV: " + general.SATV,-60}\n");

                // ACT Verbal and SAT Mathematics
                retval += String.Format($"{"ACTV: " + general.ACTV,-60} {"SATM: " + general.SATM,-60}\n");

                // ACT Mathematics and GRE Verbal
                retval += String.Format($"{"ACTM: " + general.ACTM,-60} {"GREV: " + general.GREV,-60}\n");

                // GRE Quantative and GRE Analytical
                retval += String.Format($"{"GREQ: " + general.GREQ,-60} {"GREA: " + general.GREA,-60}\n");
                
                // Space
                retval += "\n";
            }
            return retval;
        }

        // Gets the education portion of the message
        private static string GetEducationMessage(List<Education> EducationList)
        {
            string retval = "";
            foreach (Education education in EducationList)
            {
                // School Name and Major
                retval += String.Format($"{"Name: " + education.Name,-60} {"Major: " + education.Major,-60}\n");

                // Degree Earned and Date Earned
                retval += String.Format($"{"DegreeEarned: " + education.DegreeEarned,-60} {"DateEarned: " + education.DegreeDate,-60}\n");

                // City and State
                retval += String.Format($"{"School City: " + education.SchoolCity,-60} {"School State: " + education.SchoolState, -60}\n");

                // Zip and Overall GPA
                retval += String.Format($"{"School Zip: " + education.SchoolZip,-60} {"Overall GPA: " + education.OverallGPA,-60}\n");

                // Major GPA
                retval += String.Format($"{"Major GPA: " + education.MajorGPA,-60}\n");

                // SkillsGained
                retval += String.Format($"{"Skills Gained: "}\n");
                
                // Each Skill
                foreach (string skill in education.SkillsGained)
                {
                    retval += String.Format($"\t-{skill,-60}\n");
                }

                // Space
                retval += "\n";
            }
            return retval;
        }

        // Gets the professional portion of the message
        private static string GetProfessionMessage(List<Professional> ProfessionList)
        {
            string retval = "";
            foreach (Professional profession in ProfessionList)
            {
                // Profession Name and Position
                retval += String.Format($"{"Profession: " + profession.Name, -60} {"Position: " + profession.Position, -60}\n");

                // Format End Date
                string EndDate = "";
                if (profession.EndDate == null)
                {
                    EndDate = "Current";
                }
                else
                {
                    EndDate = profession.EndDate.ToString();
                }

                // Start and End Dates
                retval = retval + String.Format($"{"Start Date: " + profession.StartDate,-60} {"End Date: " + EndDate,-60}\n");

                // City and State
                retval += String.Format($"{"City: " + profession.ProfessionCity, -60} {"State: " + profession.ProfessionState, -60}\n");

                // SkillsGained
                retval += String.Format($"{"Skills Gained: ",-60}\n");

                // Each Skill
                foreach (string skill in profession.SkillsGained)
                {
                    retval += String.Format($"\t-{skill,-60}\n");
                }

                // Space
                retval += "\n";
            }
            return retval;
        }

        // Gets the certification portion of the message
        private static string GetCertificationMessage(List<Certifications> CertificationList)
        {
            string retval = "";
            foreach (Certifications certification in CertificationList)
            {
                // Certification Name and Earn Date
                retval += String.Format($"{"Certification Name: " + certification.Certification, -60} {"Earn Date: " + certification.Date,-60}\n");
                
                // Completed
                retval += String.Format($"{"Completed: " + certification.Completed, -60}\n");
            }

            // Space
            retval += "\n";

            return retval;
        }

        // Gets the skill portion of the message
        private static string GetSkillsMessage(List<Skills> SkillsList)
        {
            string retval = "";
            foreach (Skills skills in SkillsList)
            {
                // Skill
                retval += String.Format($"-{"" + skills.Skill, -60}\n");
            }

            // Space
            retval += "\n";

            return retval;
        }

        // Gets the awards portion of the message
        private static string GetAwardsMessage(List<Awards> AwardList)
        {
            string retval = "";
            foreach (Awards award in AwardList)
            {
                // Awards
                retval += String.Format($"-{"Award: " + award.Award, -60} {"Date: " + award.EarnDate, -60}\n");
            }

            // Space
            retval += "\n";

            return retval;
        }
    }
}
