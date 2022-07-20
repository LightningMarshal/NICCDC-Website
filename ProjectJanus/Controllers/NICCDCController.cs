using Microsoft.AspNetCore.Mvc;

namespace ProjectJanus.Controllers
{
    /*
     * This serves the main NICCDC webpages found in the root of the RegularPages folder (NICCDC-Website).
     * These are the actions for the main navbar when on the main pages.
     */
    public class NICCDCController : Controller
    {
        public IActionResult Home()
        {
            return View("Index");
        }

        public IActionResult Media()
        {
            return View();
        }

        public IActionResult NIATEC()
        {
            return View();
        }

        public IActionResult NICCDC()
        {
            return View();
        }

        public IActionResult Participants()
        {
            return View();
        }

        public IActionResult Sponsors()
        {
            return View();
        }

        public IActionResult Connect()
        {
            return View();
        }

        public IActionResult Volunteers()
        {
            return View();
        }
    }
}