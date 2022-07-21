using Microsoft.AspNetCore.Mvc;

namespace ProjectJanus.Controllers
{
    /*
     * This serves the Stay Connected related webpages found in the NICCDC folder.
     * These are the actions for the sub navbar for NICCDC.
     */
    public class ConnectController : Controller
    {
        public IActionResult MikePond()
        {
            return View();
        }

        public IActionResult News()
        {
            return View();
        }

        public IActionResult Q1_2021_PressRelease()
        {
            return View();
        }
    }
}
