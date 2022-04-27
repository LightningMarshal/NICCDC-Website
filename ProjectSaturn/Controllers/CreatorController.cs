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

        public IActionResult Home()
        {
            ViewData["Title"] = "Home";
            return View();
        }

        public IActionResult PersonalDetails()
        {         
            ViewData["Title"] = "Personal";
            ViewData["CreatorPartial"] = "_PersonalPartial";

            return View("CreatorPages");
        }

        public IActionResult GeneralInfo()
        {
            ViewData["Title"] = "General";
            ViewData["CreatorPartial"] = "_GeneralInfoPartial";

            return View("CreatorPages");
        }

        public IActionResult EducationDetails()
        {
            ViewData["Title"] = "Education";
            ViewData["CreatorPartial"] = "_EducationPartial";

            
            return View("CreatorPages");
        }

        public IActionResult TrainingDetails()
        {
            ViewData["Title"] = "Certifications";
            ViewData["CreatorPartial"] = "_TrainingPartial";
            return View("CreatorPages");
        }

        public IActionResult ProfessionalDetails()
        {
            ViewData["Title"] = "Professional";
            ViewData["CreatorPartial"] = "_ProfessionalPartial";
            return View("CreatorPages");
        }

        public IActionResult KnowledgeDetails()
        {
            ViewData["Title"] = "Knowledge / Skills / Abilites";
            ViewData["CreatorPartial"] = "_KnowledgePartial";
            return View("CreatorPages");
        }

        public IActionResult AwardsDetails()
        {
            ViewData["Title"] = "Awards";
            ViewData["CreatorPartial"] = "_AwardsPartial";
            return View("CreatorPages");
        }

        [HttpPost]
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

        // The following code adds or edits an entry of the user's data
        [HttpPost]
        public ActionResult PersonalDetails(string jsonString) // This takes in the Personal Data and stores it appropriately
        {
            var settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
            };
            
            Guid currentUser = new(HttpContext.Request.Cookies["user"]); // Get the current user

            Personal personal = JsonConvert.DeserializeObject<Personal>(jsonString, settings); // Recieve and Verify the data
            personal.Email = _dal.GetEmail(currentUser);
            if (personal.FirstName == null || personal.LastName == null || personal.MobilePhone == null || personal.Address == null || personal.City == null || personal.State == null || personal.Zip == null) // Required Fields
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
                else if (id == -10) // NOTE : Duplicate Detection has been turned off in the SQL Stored Procedures
                {
                    ErrorLog.Msglist.Add("Duplicate Entry Detected: Skipping");
                    return Json("true");
                }
                return Json("false");
            }
            return Json("false");
        }

        [HttpPost]
        public ActionResult GeneralInfo(string jsonString) // This takes in the data Between the Personal and Educational in the Application Form
        {
            var settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
            };

            Guid currentUser = new(HttpContext.Request.Cookies["user"]); // Get the current user

            GeneralInfo general = JsonConvert.DeserializeObject<GeneralInfo>(jsonString, settings); // Recieve and Verify the data
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
                if (id == -10)
                {
                    ErrorLog.Msglist.Add("Duplicate Entry Detected: Skipping");
                    return Json("true");
                }
            }
            return Json("false");
        }

        [HttpPost]
        public ActionResult EducationDetails(string jsonString) // This takes in the Education Data and stores it appropriately
        {
            var settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
            };

            Guid currentUser = new(HttpContext.Request.Cookies["user"]); // Get the current user


            Education education = JsonConvert.DeserializeObject<Education>(jsonString, settings); // Recieve and Verify the data
            if (education.Name == "" || education.OverallGPA == null || education.SchoolState == "") // Required Fields
            {
                return Json("required");
            }
            

            if (education != null) // Store the Data
            {
                string SkillsList = JsonConvert.SerializeObject(education.SkillsGained); //TODO : Deserialize in a reviewer view to read the list of skills (DeserializeObject<List<string>>)
                int id = _dal.AddEducation(education, currentUser, SkillsList);
                if (id > 0)
                {
                    return Json("true another");
                }
                else if (id == -10)
                {
                    ErrorLog.Msglist.Add("Duplicate Entry Detected: Skipping");
                    return Json("true another");
                }
                return Json("false");
            }
            return Json("false");
        }

        [HttpPost]
        public ActionResult TrainingDetails(string jsonString) // This takes in the Training Data and stores it appropriately
        {
            var settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
            };

            Guid currentUser = new(HttpContext.Request.Cookies["user"]); // Get the current user
            List<Certifications> deSerTrainingList = new(); // Deserialized List
            List<Certifications> toSubmitList = new(); // List of correct entries to submit
            int successfullEntry = 0; // Verify successfull submission


            GenericList TrainingList = JsonConvert.DeserializeObject<GenericList>(jsonString, settings); // Recieve and Verify the data
            foreach (string trainingString in TrainingList.Strings)
            {
                Certifications training = JsonConvert.DeserializeObject<Certifications>(trainingString, settings); 
                deSerTrainingList.Add(training);
            }
            foreach (Certifications training in deSerTrainingList) // Verify the data is correctly formatted
            {
                if (training.Desc != "" && training.Date != null) // No null or empty
                {
                    toSubmitList.Add(training);
                } 
                else if (training.Desc != "" && training.Date == null) // No partial entry
                {
                    return Json("trequired");
                }
            }


            if (toSubmitList.Count != 0) // Store the data
            {
                foreach (Certifications training in toSubmitList)
                {
                    int id = _dal.AddTraining(training, currentUser);
                    if (id > 0)
                    {
                        successfullEntry++;
                    }
                    else if (id == -10)
                    {
                        ErrorLog.Msglist.Add("Duplicate Entry Detected: Skipping");
                        successfullEntry++;
                    }
                }
                if (successfullEntry == deSerTrainingList.Count)
                {
                    return Json("true");
                }
                return Json("false");
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

            Professional profession = JsonConvert.DeserializeObject<Professional>(jsonString, settings); // Recieve and verify the data
            if (profession.Name == "" || profession.PositionAtComp == "" || profession.Location == "" || profession.StartDate == null || profession.EndDate == null) // Required Fields
            {
                return Json("required");
            }


            if (profession != null) // Store the Data
            {
                string SkillsList = JsonConvert.SerializeObject(profession.SkillsGained); //TODO : Deserialize in a reviewer view to read the list of skills (DeserializeObject<List<string>>)
                int id = _dal.AddProfessional(profession, currentUser, SkillsList);
                if (id > 0)
                {
                    return Json("true another");
                }
                else if (id == -10)
                {
                    ErrorLog.Msglist.Add("Duplicate Entry Detected: Skipping");
                    return Json("true another");
                }
                return Json("false");
            }
            return Json("false");
        }

        [HttpPost]
        public IActionResult KnowledgeDetails(string jsonString)
        {
            var settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
            };

            Guid currentUser = new(HttpContext.Request.Cookies["user"]);
            List<Knowledge> deSerKnowledgeList = new(); // Deserialized List
            List<Knowledge> toSubmitList = new(); // List of correct entries to submit
            int successfullEntry = 0; // Verify successfull submission

            GenericList KnowledgeList = JsonConvert.DeserializeObject<GenericList>(jsonString, settings); // Recieve and Verify the data
            foreach (string knowledgeString in KnowledgeList.Strings)
            {
                Knowledge knowledge = JsonConvert.DeserializeObject<Knowledge>(knowledgeString, settings);
                deSerKnowledgeList.Add(knowledge);
            }
            foreach (Knowledge knowledge in deSerKnowledgeList) // Verify the data is correctly formatted
            {
                if (knowledge.Desc != "") // No null or empty
                {
                    toSubmitList.Add(knowledge);
                }
            }

            if (toSubmitList.Count != 0) // Store the data
            {
                foreach (Knowledge knowledge in toSubmitList)
                {
                    int id = _dal.AddKnowledge(knowledge, currentUser);
                    if (id > 0)
                    {
                        successfullEntry++;
                    }
                    else if (id == -10)
                    {
                        ErrorLog.Msglist.Add("Duplicate Entry Detected: Skipping");
                        successfullEntry++;
                    }
                }
                if (successfullEntry == deSerKnowledgeList.Count)
                {
                    return Json("true");
                }
                return Json("false");
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

            Guid currentUser = new(HttpContext.Request.Cookies["user"]); // Get the current user
            List<Awards> deSerAwardList = new(); // Deserialized List
            List<Awards> toSubmitList = new(); // List of correct entries to submit
            int successfullEntry = 0; // Verify successfull submission


            GenericList AwardList = JsonConvert.DeserializeObject<GenericList>(jsonString, settings); // Recieve and Verify the data
            foreach (string awardString in AwardList.Strings)
            {
                Awards award = JsonConvert.DeserializeObject<Awards>(awardString, settings);
                deSerAwardList.Add(award);
            }
            foreach (Awards award in deSerAwardList) // Verify the data is correctly formatted
            {
                if (award.Desc != "" && award.Date != null) // No null or empty
                {
                    toSubmitList.Add(award);
                }
                else if (award.Desc != "" && award.Date == null) // No partial entry
                {
                    return Json("trequired");
                }
            }


            if (toSubmitList.Count != 0) // Store the data
            {
                foreach (Awards award in toSubmitList)
                {
                    int id = _dal.AddAwards(award, currentUser);
                    if (id > 0)
                    {
                        successfullEntry++;
                    }
                    else if (id == -10)
                    {
                        ErrorLog.Msglist.Add("Duplicate Entry Detected: Skipping");
                        successfullEntry++;
                    }
                }
                if (successfullEntry == deSerAwardList.Count)
                {
                    return Json("true");
                }
                return Json("false");
            }
            return Json("false");
        }
    }
}