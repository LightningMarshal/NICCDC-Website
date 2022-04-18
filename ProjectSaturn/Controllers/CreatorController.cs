using Newtonsoft.Json;
using ProjectSaturn.Models;
using ProjectSaturn.Service;
using ProjectSaturn.Extensions;
using System.Diagnostics;
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

        //TODO : Add a Welcome Page

        public IActionResult PersonalDetails()
        {         
            ViewData["Title"] = "Personal";
            ViewData["CreatorPartial"] = "_PersonalPartial";

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
            ViewData["Title"] = "Trainings";
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
            ViewData["Title"] = "Knowledge";
            ViewData["CreatorPartial"] = "_KnowledgePartial";
            return View("CreatorPages");
        }

        public IActionResult AwardsDetails()
        {
            ViewData["Title"] = "Awards";
            ViewData["CreatorPartial"] = "_AwardsPartial";
            return View("CreatorPages");
        }



        // The following code adds or edits an entry of the user's data
        [HttpPost]
        public ActionResult PersonalDetails(string jsonString) // This takes in the Personal Data and stores it appropriately
        {
            var settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
            };

            Personal personal = JsonConvert.DeserializeObject<Personal>(jsonString, settings); // Recieve and Verify the data
            if (personal.FirstName == "" || personal.LastName == "" || personal.Email == "" || personal.PhoneNumber == "" || personal.Address == "")
            {
                return Json("required");
            }

            Guid currentUser = new(HttpContext.Request.Cookies["user"]); // Get the current user

            if (personal != null) // Store the Data
            {
                int id = _dal.AddPersonal(personal, currentUser);
                if (id > 0)
                {
                    return Json("true");
                }
                return Json("false");
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

            Education education = JsonConvert.DeserializeObject<Education>(jsonString, settings); // Recieve and Verify the data
            if (education.Name == "" || education.GPA == null || education.Location == "" || education.StartDate == null || education.EndDate == null)
            {
                return Json("required");
            }

            Guid currentUser = new(HttpContext.Request.Cookies["user"]); // Get the current user

            if (education != null) // Store the Data
            {
                string SkillsList = JsonConvert.SerializeObject(education.SkillsGained); //TODO : Deserialize in a reviewer view to read the list of skills (DeserializeObject<List<string>>)
                int id = _dal.AddEducation(education, currentUser, SkillsList);
                if (id > 0)
                {
                    return Json("true");
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

            GenericList TrainingList = JsonConvert.DeserializeObject<GenericList>(jsonString, settings); // Deserialize to a list of jsonstrings
            int lengthTrainingList = TrainingList.Strings.Count;
            int correctEntry = 0;

            List<Trainings> destraininglist = new();
            foreach (string trainingString in TrainingList.Strings)
            {
                Trainings training = JsonConvert.DeserializeObject<Trainings>(trainingString, settings); // Deserialize the jsonstrings within the list and add to a non-serialized list
                destraininglist.Add(training);
            }

            foreach (Trainings training in destraininglist) // Verify the data is correctly formatted
            {
                if (training.Desc == "" || training.Date == null)
                {
                    return Json("required");
                }
                correctEntry++;
            }

            Guid currentUser = new(HttpContext.Request.Cookies["user"]); // Get the current user

            int successfullEntry = 0;
            if (lengthTrainingList == correctEntry && destraininglist[0] != null) // Store the data
            {
                foreach (Trainings training in destraininglist)
                {
                    int id = _dal.AddTraining(training, currentUser);
                    if (id > 0)
                    {
                        successfullEntry++;
                    }
                }
                if (successfullEntry == lengthTrainingList)
                {
                    return Json("true");
                }
                return Json("false");
            }
            //TODO : Make changes to the training details
            return Json("false");
        }

        [HttpPost]
        public IActionResult ProfessionalDetails(string jsonString)
        {
            //TODO : Make changes to the professional details
            return RedirectToAction("Creator", "Creator");
        }

        [HttpPost]
        public IActionResult KnowledgeDetails(string jsonString)
        {
            //TODO : Make changes to the skills details
            return RedirectToAction("Creator", "Creator");
        }
        
        [HttpPost]
        public IActionResult AwardsDetails(string jsonString)
        {
            //TODO : Make changes to the awards details
            return RedirectToAction("Creator", "Creator");
        }
    }
}