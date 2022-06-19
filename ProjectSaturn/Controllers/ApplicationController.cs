using Newtonsoft.Json;
using ProjectSaturn.Models;
using ProjectSaturn.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace ProjectSaturn.Controllers
{
    public class ApplicationController : Controller
    {
        public IActionResult Home() // Home Page, This is where the user will enter their email
        {
            ViewData["Title"] = "Home";
            return View();
        }

        public IActionResult Classic()
        {
            return View();
        }

        public IActionResult PersonalDetails() // Personal Page, This is where the user will enter basic personal info
        {         
            ViewData["Title"] = "Personal";
            ViewData["ApplicationPartial"] = "_PersonalPartial";

            return View("ApplicationPages");
        }

        public IActionResult GeneralDetails() // General Page, This is where the user will enter details important for the application
        {
            ViewData["Title"] = "General";
            ViewData["ApplicationPartial"] = "_GeneralPartial";

            return View("ApplicationPages");
        }

        public IActionResult EducationDetails() // Education Page, This is where the user will enter details regarding previous education
        {
            ViewData["Title"] = "Education";
            ViewData["ApplicationPartial"] = "_EducationPartial";
            
            return View("ApplicationPages");
        }

        public IActionResult ProfessionalDetails() // Professional Page, This is where the user will enter details regarding previous employment
        {
            ViewData["Title"] = "Professional";
            ViewData["ApplicationPartial"] = "_ProfessionalPartial";
            return View("ApplicationPages");
        }

        public IActionResult CertificationDetails() // Certification Page, This is where the user will add any certifications they have completed or planning to complete
        {
            ViewData["Title"] = "Certifications";
            ViewData["ApplicationPartial"] = "_CertificationsPartial";
            return View("ApplicationPages");
        }

        public IActionResult SkillsDetails() // Skills Page, This is where the user will add any applicable skills they possess.
        {
            ViewData["Title"] = "Knowledge / Skills / Abilites";
            ViewData["ApplicationPartial"] = "_SkillsPartial";
            return View("ApplicationPages");
        }

        public IActionResult AwardsDetails() // Awards Page, This is where the user will add any awards they have won.
        {
            ViewData["Title"] = "Awards";
            ViewData["ApplicationPartial"] = "_AwardsPartial";
            return View("ApplicationPages");
        }

        public IActionResult Finished() // Finished Page, This will congratulate the user for completing the application and informs them general information
        {
            return View("Finished");
        }



        public async Task<IActionResult> DownloadFile(string file) // File Downloader, Currently Only supplies the References document
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/downloadables", file);
            var memory = new MemoryStream();
            using (var stream = new FileStream(path, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;
            return File(memory, "application/pdf", Path.GetFileName(path));
        }


                     
        // Home Submission, This will accept the user's email and either create a new user or sign in as the user.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Home([Bind("Email")] Personal person)
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
                if (HttpContext.Request.Cookies["user"] == null)
                {
                    HttpContext.Response.Cookies.Append("user", person.Email, option);
                }
                else
                {
                    //TODO : if other cookies are present, submit them then remove them.
                    RedirectToAction("Mail", "Submission");
                    HttpContext.Response.Cookies.Delete("user");
                    HttpContext.Response.Cookies.Append("user", person.Email, option);
                }
            }
            else
            {
                ViewData["Error"] = "Email is not valid.";
                return View();
            }

            return RedirectToAction("PersonalDetails");
        }

        // Personal Submission, This takes in the Personal Data, verifies it for required fields, and stores it appropriately
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PersonalDetails(string jsonString) 
        {
            var settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
            };

            string email = HttpContext.Request.Cookies["user"];

            Personal personal = JsonConvert.DeserializeObject<Personal>(jsonString, settings); // Recieve data
            if (personal.FirstName == "" || personal.LastName == "" || personal.MobilePhone == "" || personal.Address == "" || personal.City == "" || personal.State == "" || personal.Zip == "") // Required Fields
            {
                return Json("required");
            }

            if (personal != null)
            {
                personal.Email = email;
            }

            string boolean = SetData<Personal>(personal); // Set Data (At bottom of page)

            return Json(boolean);
        }

        // General Submission, This takes in the General data, verifies it for required fields, and stores it appropriately
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GeneralDetails(string jsonString)
        {
            var settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
            };

            General general = JsonConvert.DeserializeObject<General>(jsonString, settings); // Recieve data
            if (general.Degree == null || general.DegreeStatus == null || general.AntiGradDate == null || general.OverallGPA == null || general.MajorGPA == null) // Required Fields
            {
                return Json("required");
            }

            string boolean = SetData<General>(general); // Set Data (At bottom of page)

            return Json(boolean);
        }

        // Education Submission, This takes in the Education data, verifies it for required fields, and stores it appropriately
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EducationDetails(string jsonString)
        {
            var settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
            };

            Education education = JsonConvert.DeserializeObject<Education>(jsonString, settings); // Recieve data
            if (education.Name == "" || education.Major == "" || education.OverallGPA == null || education.MajorGPA == null || education.SchoolCity == "" || education.SchoolState == "" || education.SchoolZip == "") // Required Fields
            {
                return Json("required");
            }

            string boolean = SetData<Education>(education);

            return Json(boolean);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ProfessionalDetails(string jsonString)
        {
            var settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
            };

            Professional profession = JsonConvert.DeserializeObject<Professional>(jsonString, settings); // Recieve data
            if (profession.Name == "" || profession.Position == "" || profession.ProfessionCity == "" || profession.ProfessionState == "" || profession.StartDate == null || profession.EndDate == null) // Required Fields
            {
                return Json("required");
            }

            string boolean = SetData<Professional>(profession);

            return Json(boolean);
        }

        // Certification submit, This takes in the Certification data, verifies it, and stores it appropriately
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CertificationDetails(string jsonString)
        {
            var settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
            };

            List<Certifications> tempList = JsonConvert.DeserializeObject<List<Certifications>>(jsonString, settings);
            List<Certifications> certificationList = new();

            foreach (Certifications certification in tempList)
            {
                if (certification.Certification != "" && certification.Date == null) // No partial entry
                {
                    return Json("drequired");
                }
                else if (certification.Certification != "") // No empty entries
                {
                    certificationList.Add(certification);
                }
            }

            string boolean = SetData<Certifications>(certificationList);

            return Json(boolean);
        }

        // Skill submit, This takes in the Skills data, verifies it, and stores it appropriately
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SkillsDetails(string jsonString)
        {
            var settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
            };

            List<Skills> tempList = JsonConvert.DeserializeObject<List<Skills>>(jsonString, settings);
            List<Skills> skillsList = new();

            foreach (Skills skill in tempList)
            {
                if (skill.Skill != "") // No empty entries
                {
                    skillsList.Add(skill);
                }
            }

            string boolean = SetData<Skills>(skillsList);

            return Json(boolean);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AwardsDetails(string jsonString)
        {
            var settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
            };

            List<Awards> tempList = JsonConvert.DeserializeObject<List<Awards>>(jsonString, settings);
            List<Awards> awardsList = new();

            foreach (Awards award in tempList)
            {
                if (award.Award != "" && award.EarnDate == null) // No partial entries
                {
                    return Json("drequired");
                }
                else if (award.Award != "") // No empty entries
                {
                    awardsList.Add(award);
                }
            }

            string boolean = SetData<Awards>(awardsList);

            return Json(boolean);
        }



        // This will submit the data provided into a cookie based on data type.
        private string SetData<T>(T t)
        {
            var settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
            };

            var option = new CookieOptions
            {
                Expires = new DateTimeOffset(DateTime.Now.AddDays(1))
            };

            if (t != null) // Store the Data
            {
                string cookie = "";
                string retval = "";
                if (t is Personal)
                {
                    cookie = "Personal";
                    retval = "true";
                }
                else if (t is General){
                    cookie = "General";
                    retval = "true";
                }
                else if (t is Education)
                {
                    cookie = "Education";
                    retval = "true another";
                }
                else if (t is Professional)
                {
                    cookie = "Professional";
                    retval = "true another";
                }

                if (HttpContext.Request.Cookies[cookie] == null) // Ensures no exsisting cookie
                {
                    List<T> tList = new();
                    tList.Add(t);
                    string json = JsonConvert.SerializeObject(tList, settings);
                    HttpContext.Response.Cookies.Append(cookie, json, option);
                }
                else
                {
                    string json = HttpContext.Request.Cookies[cookie];
                    List<T> tList = JsonConvert.DeserializeObject<List<T>>(json, settings);
                    tList.Add(t);
                    json = JsonConvert.SerializeObject(tList, settings);
                    HttpContext.Response.Cookies.Delete(cookie);
                    HttpContext.Response.Cookies.Append(cookie, json, option);
                }
                return retval;
            }
            return "false";
        }

        // This will submit the data provided into a cookie based on list data type
        private string SetData<T>(List<T> t)
        {
            var settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
            };

            var option = new CookieOptions
            {
                Expires = new DateTimeOffset(DateTime.Now.AddDays(1))
            };

            if (t.Count != 0) // Store the data
            {
                string cookie = "";
                if (t[0] is Certifications)
                {
                    cookie = "Certification";
                }
                else if (t[0] is Skills)
                {
                    cookie = "Skill";
                }
                else if (t[0] is Awards)
                {
                    cookie = "Award";
                }

                if (HttpContext.Request.Cookies[cookie] == null)
                {
                    string json = JsonConvert.SerializeObject(t, settings);
                    HttpContext.Response.Cookies.Append(cookie, json, option);
                }
                else
                {
                    string json = HttpContext.Request.Cookies[cookie];
                    List<T> currentList = JsonConvert.DeserializeObject<List<T>>(json, settings);
                    foreach (T singleT in t)
                    {
                        currentList.Add(singleT);
                    }
                    json = JsonConvert.SerializeObject(currentList, settings);
                    HttpContext.Response.Cookies.Delete(cookie);
                    HttpContext.Response.Cookies.Append(cookie, json, option);
                }
                return "true another";
            }
            return "";
        }
    }
}