using Newtonsoft.Json;
using ProjectSaturn.Models;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;
using System.Net;

namespace ProjectSaturn.Controllers
{
    public class SubmissionController : Controller
    {
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

            string applicant = HttpContext.Request.Cookies["user"];

            string subject = "SFS Applicant: " + applicant; // Subject of Email (user's email is added for easy identification)
            string message = ""; // All of the application information
            // Build the Message:
            if (personal != null)
            {
                message = message + "Personal: \n" + GetPersonalMessage(personal) + "\n";
            }
            if (general != null)
            {
                message = message + "General: \n" + GetGeneralMessage(general) + "\n";
            }
            if (education != null)
            {
                message = message +  "Education: \n" + GetEducationMessage(education) + "\n";
            }
            if (professional != null)
            {
                message = message + "Professional: \n" + GetProfessionMessage(professional) + "\n";
            }
            if (certification != null)
            {
                message = message + "Certifications: \n" + GetCertificationMessage(certification) + "\n";
            }
            if (skills != null)
            {
                message = message + "Skills: \n" + GetSkillsMessage(skills) + "\n";
            }
            if (awards != null)
            {
                message = message + "Awards: \n" + GetAwardsMessage(awards) + "\n";
            }
            
            // Mail the message here
            Debug.WriteLine(message);

            try
            {
                if (ModelState.IsValid)
                {
                    //TODO: Change to client email
                    var senderEmail = new MailAddress("testing.env.kason.summers@gmail.com", "Kason");
                    //TODO: Change to Kaye's email
                    var receiverEmail = new MailAddress("kason.summers.pro@gmail.com", "Kason");
                    //TODO: Change to client password
                    var password = "ukjfscxaotlmfzfx";
                    var sub = subject;
                    var body = message;
                    var smtp = new SmtpClient
                    {
                        Host = "smtp.gmail.com",
                        Port = 587,
                        EnableSsl = true,
                        DeliveryMethod = SmtpDeliveryMethod.Network,
                        UseDefaultCredentials = false,
                        Credentials = new NetworkCredential(senderEmail.Address, password)
                    };
                    using (var mess = new MailMessage(senderEmail, receiverEmail)
                    {
                        Subject = subject,
                        Body = body
                    })
                    {
                        smtp.Send(mess);
                    }
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
                // Name
                retval = retval + "First Name: " + person.FirstName + "\t\t";
                retval = retval + "Middle Init: " + person.MiddleInit+ "\t\t";
                retval = retval + "Last Name: " + person.LastName+ "\n";
                // Contact Info
                retval = retval + "Email: " + person.Email + "\t\t";
                retval = retval + "Day Phone: " + person.DayPhone + "\t\t";
                retval = retval + "EveningPhone: " + person.EveningPhone + "\t\t";
                retval = retval + "MobilePhone: " + person.MobilePhone + "\n";
                // Address
                retval = retval + "Address: " + person.Address + "\t\t";
                retval = retval + "City: " + person.City + "\t\t";
                retval = retval + "State: " + person.State + "\t\t";
                retval = retval + "Zip: " + person.Zip + "\n";
                // Is US Citizen
                retval = retval + "IsUSCitizen: " + person.IsUSCitizen + "\n";
                // Space
                retval = retval + "\n";
            }
            return retval;
        }

        // Gets the general portion of the message
        private static string GetGeneralMessage(List<General> GeneralList)
        {
            string retval = "";
            foreach (General general in GeneralList)
            {
                // General
                retval = retval + "Degree: " + general.Degree + "\t\t";
                retval = retval + "DegreeStatus: " + general.DegreeStatus + "\t\t";
                retval = retval + "Anticipated Graduation Date: " + general.AntiGradDate + "\n";
                // GPA
                retval = retval + "OverallGPA: " + general.OverallGPA + "\t\t";
                retval = retval + "MajorGPA: " + general.MajorGPA + "\t\t";
                // SAT
                retval = retval + "SATV: " + general.SATV + "\t\t";
                retval = retval + "SATM: " + general.SATM + "\n";
                // ACT
                retval = retval + "ACTV: " + general.ACTV + "\t\t";
                retval = retval + "ACTM: " + general.ACTM + "\t\t";
                // GRE
                retval = retval + "GREV: " + general.GREV + "\t\t";
                retval = retval + "GREQ: " + general.GREQ + "\t\t";
                retval = retval + "GREA: " + general.GREA + "\n";
                // Space
                retval = retval + "\n";
            }
            return retval;
        }

        // Gets the education portion of the message
        private static string GetEducationMessage(List<Education> EducationList)
        {
            string retval = "";
            foreach (Education education in EducationList)
            {
                // General
                retval = retval + "Name: " + education.Name + "\t\t";
                retval = retval + "Major: " + education.Major + "\t\t";
                retval = retval + "DegreeEarned: " + education.DegreeEarned + "\t\t";
                retval = retval + "DateEarned: " + education.DegreeDate + "\n";
                // Location
                retval = retval + "School City: " + education.SchoolCity + "\t\t";
                retval = retval + "School State: " + education.SchoolState + "\t\t";
                retval = retval + "School Zip: " + education.SchoolZip + "\n";
                // GPA
                retval = retval + "Overall GPA: " + education.OverallGPA + "\t\t";
                retval = retval + "Major GPA: " + education.MajorGPA + "\n";
                // SkillsGained
                retval = retval + "Skills Gained: \n";
                foreach (string skill in education.SkillsGained)
                {
                    retval = retval + "\t" + skill + "\n";
                }
            }
            // Space
            retval = retval + "\n";
            return retval;
        }

        // Gets the professional portion of the message
        private static string GetProfessionMessage(List<Professional> ProfessionList)
        {
            string retval = "";
            foreach (Professional profession in ProfessionList)
            {
                // Profession
                retval = retval + "Profession: " + profession.Name + "\t\t";
                retval = retval + "Position: " + profession.Position + "\n";
                // Date
                retval = retval + "Start Date: " + profession.StartDate + "\t\t";
                retval = retval + "End Date: " + profession.EndDate + "\t\t";
                // Location
                retval = retval + "City: " + profession.ProfessionCity + "\t\t";
                retval = retval + "State: " + profession.ProfessionState + "\n";
                // SkillsGained
                retval = retval + "Skills Gained: \n";
                foreach (string skill in profession.SkillsGained)
                {
                    retval = retval + "\t" + skill + "\n";
                }
            }
            // Space
            retval = retval + "\n";
            return retval;
        }

        // Gets the certification portion of the message
        private static string GetCertificationMessage(List<Certifications> CertificationList)
        {
            string retval = "";
            foreach (Certifications certification in CertificationList)
            {
                // Certification
                retval = retval + "\tCertification Name: " + certification.Certification;
                retval = retval + "\tEarn Date: " + certification.Date;
                retval = retval + "\tCompleted: " + certification.Completed;
                // Next
                retval = retval + "\n";
            }
            // Space
            retval = retval + "\n";
            return retval;
        }

        // Gets the skill portion of the message
        private static string GetSkillsMessage(List<Skills> SkillsList)
        {
            string retval = "";
            foreach (Skills skills in SkillsList)
            {
                // Skill
                retval = retval + "\t" + skills.Skill;
                // Next
                retval = retval + "\n";
            }
            // Space
            retval = retval + "\n";
            return retval;
        }

        // Gets the awards portion of the message
        private static string GetAwardsMessage(List<Awards> AwardList)
        {
            string retval = "";
            foreach (Awards award in AwardList)
            {
                // Awards
                retval = retval + "\tAward: " + award.Award;
                retval = retval + "\tDate: " + award.EarnDate;
                // Next
                retval = retval + "\n";
            }
            // Space
            retval = retval + "\n";
            return retval;
        }
    }
}
