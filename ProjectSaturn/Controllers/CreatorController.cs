using Newtonsoft.Json;
using ProjectSaturn.Models;
using ProjectSaturn.Service;
using Microsoft.AspNetCore.Mvc;

namespace ProjectSaturn.Controllers
{
    public class CreatorController : Controller
    {
        private readonly DAL _dal;

        public CreatorController (DAL dal)
        {
            _dal = dal;
        }

        public IActionResult Home() // Home Page, This is where the user will enter their email
        {
            ViewData["Title"] = "Home";
            return View();
        }

        public IActionResult PersonalDetails() // Personal Page, This is where the user will enter basic personal info
        {         
            ViewData["Title"] = "Personal";
            ViewData["CreatorPartial"] = "_PersonalPartial";

            return View("CreatorPages");
        }

        public IActionResult GeneralDetails() // General Page, This is where the user will enter details important for the application
        {
            ViewData["Title"] = "General";
            ViewData["CreatorPartial"] = "_GeneralPartial";

            return View("CreatorPages");
        }

        public IActionResult EducationDetails() // Education Page, This is where the user will enter details regarding previous education
        {
            ViewData["Title"] = "Education";
            ViewData["CreatorPartial"] = "_EducationPartial";

            
            return View("CreatorPages");
        }

        public IActionResult ProfessionalDetails() // Professional Page, This is where the user will enter details regarding previous employment
        {
            ViewData["Title"] = "Professional";
            ViewData["CreatorPartial"] = "_ProfessionalPartial";
            return View("CreatorPages");
        }
        public IActionResult CertificationDetails() // Certification Page, This is where the user will add any certifications they have completed or planning to complete
        {
            ViewData["Title"] = "Certifications";
            ViewData["CreatorPartial"] = "_CertificationsPartial";
            return View("CreatorPages");
        }

        public IActionResult SkillsDetails() // Skills Page, This is where the user will add any applicable skills they possess.
        {
            ViewData["Title"] = "Knowledge / Skills / Abilites";
            ViewData["CreatorPartial"] = "_SkillsPartial";
            return View("CreatorPages");
        }

        public IActionResult AwardsDetails() // Awards Page, This is where the user will add any awards they have won.
        {
            ViewData["Title"] = "Awards";
            ViewData["CreatorPartial"] = "_AwardsPartial";
            return View("CreatorPages");
        }

        public IActionResult Finished() // Finished Page, This will congratulate the user for completing the application and informs them general information
        {
            ViewData["CreatorPartial"] = "_FinishedPartial";
            return View("CreatorPages");
        }

        public async Task<IActionResult> DownloadFile() // File Downloader, Currently Only supplies the References document
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/downloadables", "SFS Recommendation form.pdf");
            var memory = new MemoryStream();
            using (var stream = new FileStream(path, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;
            return File(memory, "application/pdf", Path.GetFileName(path));
        }



        [HttpPost]
        public ActionResult Home([Bind("Email")] Personal person) // Home Submission, This will accept the user's email and either create a new user or sign in as the user.
        {
            var option = new CookieOptions
            {
                Expires = new DateTimeOffset(DateTime.Now.AddDays(1))
            };

            if (person.Email == null) 
            {
                person.Email = "not null";
            }

            if (person.Email.ToLower().Contains('@')){
                Guid guid = _dal.AddUser(person.Email);
                string strguid = guid.ToString();
                if (HttpContext.Request.Cookies["user"] == null)
                {
                    HttpContext.Response.Cookies.Append("user", strguid, option);
                }
                else
                {
                    HttpContext.Response.Cookies.Delete("user");
                    HttpContext.Response.Cookies.Append("user", strguid, option);
                }
            }
            else
            {
                ViewData["Error"] = "Email is not valid.";
                return View();
            }

            return RedirectToAction("PersonalDetails");
        }

        [HttpPost]
        public ActionResult PersonalDetails(string jsonString) // Personal Submission, This takes in the Personal Data, verifies it for required fields, and stores it appropriately
        {
            var settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
            };
            
            Guid currentUser = new(HttpContext.Request.Cookies["user"]);

            Personal personal = JsonConvert.DeserializeObject<Personal>(jsonString, settings); // Recieve data
            personal.Email = _dal.GetEmail(currentUser);
            if (personal.FirstName == "" || personal.LastName == "" || personal.MobilePhone == "" || personal.Address == "" || personal.City == "" || personal.State == "" || personal.Zip == "") // Required Fields
            {
                return Json("required");
            }


            if (personal != null) // Store the Data
            {
                int id = _dal.AddPersonal(personal, currentUser);
                if (id > 0)
                {
                    return Json("true");
                }
                // NOTE : Duplicate Detection has been turned off in the SQL Stored Procedures
                //else if (id == -10) 
                //{
                //    ErrorLog.Msglist.Add("Duplicate Entry Detected: Skipping");
                //    return Json("true");
                //}
            }
            return Json("false");
        }

        [HttpPost]
        public ActionResult GeneralDetails(string jsonString) // General Submission, This takes in the General data, verifies it for required fields, and stores it appropriately
        {
            var settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
            };

            Guid currentUser = new(HttpContext.Request.Cookies["user"]); 

            General general = JsonConvert.DeserializeObject<General>(jsonString, settings); // Recieve data
            if (general.Degree == null || general.DegreeStatus == null || general.AntiGradDate == null || general.OverallGPA == null || general.MajorGPA == null) // Required Fields
            {
                return Json("required");
            }

            if (general != null) // Store the Data
            {
                int id = _dal.AddGeneral(general, currentUser);
                if (id > 0)
                {
                    return Json("true");
                }
                // NOTE : Duplicate Detection has been turned off in the SQL Stored Procedures
                //else if (id == -10)
                //{
                //    ErrorLog.Msglist.Add("Duplicate Entry Detected: Skipping");
                //    return Json("true");
                //}
            }
            return Json("false");
        }

        [HttpPost]
        public ActionResult EducationDetails(string jsonString) //Education Submission, This takes in the Education data, verifies it for required fields, and stores it appropriately
        {
            var settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
            };

            Guid currentUser = new(HttpContext.Request.Cookies["user"]);

            Education education = JsonConvert.DeserializeObject<Education>(jsonString, settings); // Recieve data
            if (education.Name == "" || education.Major == "" || education.OverallGPA == null || education.MajorGPA == null || education.SchoolCity == "" || education.SchoolState == "" || education.SchoolZip == "") // Required Fields
            {
                return Json("required");
            }
            

            if (education != null) // Store the Data
            {
                string SkillsList = JsonConvert.SerializeObject(education.SkillsGained); 
                int id = _dal.AddEducation(education, currentUser, SkillsList);
                if (id > 0)
                {
                    return Json("true another");
                }
                // NOTE : Duplicate Detection has been turned off in the SQL Stored Procedures
                //else if (id == -10)
                //{
                //    ErrorLog.Msglist.Add("Duplicate Entry Detected: Skipping");
                //    return Json("true another");
                //}
            }
            return Json("false");
        }

        [HttpPost]
        public IActionResult ProfessionalDetails(string jsonString)
        {
            var settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
            };

            Guid currentUser = new(HttpContext.Request.Cookies["user"]);

            Professional profession = JsonConvert.DeserializeObject<Professional>(jsonString, settings); // Recieve data
            if (profession.Name == "" || profession.Position == "" || profession.ProfessionCity == "" || profession.ProfessionState == "" || profession.StartDate == null || profession.EndDate == null) // Required Fields
            {
                return Json("required");
            }


            if (profession != null) // Store the data
            {
                string SkillsList = JsonConvert.SerializeObject(profession.SkillsGained);
                int id = _dal.AddProfessional(profession, currentUser, SkillsList);
                if (id > 0)
                {
                    return Json("true another");
                }
                // NOTE : Duplicate Detection has been turned off in the SQL Stored Procedures
                //else if (id == -10)
                //{
                //    ErrorLog.Msglist.Add("Duplicate Entry Detected: Skipping");
                //    return Json("true another");
                //}
            }
            return Json("false");
        }

        [HttpPost]
        public ActionResult CertificationDetails(string jsonString) // Certification submit, This takes in the Certification data, verifies it, and stores it appropriately

        //NOTE: Several certifications come back and are submitted separately in order to help prevent accidental loss.

        {
            var settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
            };

            Guid currentUser = new(HttpContext.Request.Cookies["user"]);
            List<Certifications> DeserializedCertificationList = new();
            List<Certifications> CertificationsToSubmitList = new();
            int successfullEntry = 0;


            GenericList CertificationList = JsonConvert.DeserializeObject<GenericList>(jsonString, settings); // Recieve and Verify the data
            try
            {
                foreach (string certificationString in CertificationList.Strings)
                {
                    Certifications certification = JsonConvert.DeserializeObject<Certifications>(certificationString, settings);
                    DeserializedCertificationList.Add(certification);
                }
                foreach (Certifications certification in DeserializedCertificationList) // Verify the data is correctly formatted
                {
                    if (certification.Certification != "" && certification.Date == null) // No partial entry
                    {
                        return Json("crequired");
                    }
                    CertificationsToSubmitList.Add(certification);

                }
            }
            catch
            {
                return Json("nothing");
            }

            bool added = false;
            if (CertificationsToSubmitList.Count != 0) // Store the data
            {
                foreach (Certifications certification in CertificationsToSubmitList)
                {
                    if (certification.Certification == "") // Null fields not counted
                    {
                        successfullEntry++;
                    }
                    else
                    {
                        int id = _dal.AddCertification(certification, currentUser);
                        if (id > 0)
                        {
                            successfullEntry++;
                            added = true;
                        }
                        // NOTE : Duplicate Detection has been turned off in the SQL Stored Procedures
                        //else if (id == -10)
                        //{
                        //    ErrorLog.Msglist.Add("Duplicate Entry Detected: Skipping");
                        //    successfullEntry++;
                        //}
                    }
                }
                if (successfullEntry == DeserializedCertificationList.Count)
                {
                    if (added == false)
                    {
                        return Json("blank");
                    }
                    else
                    {
                        return Json("true");
                    }
                }
            }
            return Json("false");
        }

        [HttpPost]
        public IActionResult SkillsDetails(string jsonString)
        {
            var settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
            };

            Guid currentUser = new(HttpContext.Request.Cookies["user"]);
            List<Skills> DeserializedSkillsList = new(); // Deserialized List
            List<Skills> SkillsToSubmitList = new(); // List of correct entries to submit
            int successfullEntry = 0; // Verify successfull submission


            GenericList SkillsList = JsonConvert.DeserializeObject<GenericList>(jsonString, settings); // Recieve data
            foreach (string skillString in SkillsList.Strings)
            {
                Skills skill = JsonConvert.DeserializeObject<Skills>(skillString, settings);
                DeserializedSkillsList.Add(skill);
            }
            foreach (Skills skill in DeserializedSkillsList) // Verify the data is correctly formatted
            {
                SkillsToSubmitList.Add(skill);
            }
            

            bool added = false;
            if (SkillsToSubmitList.Count != 0) // Store the data
            {
                foreach (Skills skill in SkillsToSubmitList)
                {
                    if (skill.Skill == "") // Null fields not counted
                    {
                        successfullEntry++;
                    }
                    else 
                    { 
                        int id = _dal.AddSkills(skill, currentUser);
                        if (id > 0)
                        {
                            successfullEntry++;
                            added = true;
                        }
                        // NOTE : Duplicate Detection has been turned off in the SQL Stored Procedures
                        //else if (id == -10)
                        //{
                        //    ErrorLog.Msglist.Add("Duplicate Entry Detected: Skipping");
                        //    successfullEntry++;
                        //}
                    }
                }
                if (successfullEntry == DeserializedSkillsList.Count)
                {
                    if (added == false)
                    {
                        return Json("blank");
                    }
                    else
                    {
                        return Json("true");
                    }
                }
            }
            return Json("false");
        }
        
        [HttpPost]
        public IActionResult AwardsDetails(string jsonString)
        {
            var settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
            };

            Guid currentUser = new(HttpContext.Request.Cookies["user"]); 
            List<Awards> DeserializedAwardsList = new(); 
            List<Awards> AwardsToSubmitList = new(); 
            int successfullEntry = 0; 


            GenericList AwardsList = JsonConvert.DeserializeObject<GenericList>(jsonString, settings); // Recieve data
            foreach (string awardString in AwardsList.Strings)
            {
                Awards award = JsonConvert.DeserializeObject<Awards>(awardString, settings);
                DeserializedAwardsList.Add(award);
            }
            foreach (Awards award in DeserializedAwardsList) // Verify the data is correctly formatted
            {
                if (award.Award != "" && award.EarnDate == null) // No partial entry
                {
                    return Json("crequired");
                }
                AwardsToSubmitList.Add(award);
            }



            bool added = false;
            if (AwardsToSubmitList.Count != 0) // Store the data
            {
                foreach (Awards award in AwardsToSubmitList)
                {
                    if (award.Award == "")
                    {
                        successfullEntry++;
                    }
                    else
                    {
                        int id = _dal.AddAwards(award, currentUser);
                        if (id > 0)
                        {
                            successfullEntry++;
                            added = true;
                        }
                        // NOTE : Duplicate Detection has been turned off in the SQL Stored Procedures
                        //else if (id == -10)
                        //{
                        //    ErrorLog.Msglist.Add("Duplicate Entry Detected: Skipping");
                        //    successfullEntry++;
                        //}
                    }
                }
                if (successfullEntry == DeserializedAwardsList.Count)
                {
                    if (added == false)
                    {
                        return Json("blank");
                    }
                    else
                    {
                        return Json("true");
                    }
                }
            }
            return Json("false");
        }
    }
}