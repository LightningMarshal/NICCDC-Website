using Microsoft.AspNetCore.Mvc;

namespace ProjectJanus.Controllers
{
    /*
     * This serves the competition related webpages found in the NICCDC folder.
     * These are the actions for the sub navbar for NICCDC.
     */
    public class CompetitionController : Controller
    {
        //TODO: Uncomment when History Page is built out
        //public IActionResult History()
        //{
        //    return View();
        //}

        public IActionResult Mission()
        {
            return View();
        }

        public IActionResult Winners()
        {
            return View();
        }

        public IActionResult Standings()
        {
            return View();
        }
    }
}
