using Microsoft.AspNetCore.Mvc;

namespace ProjectJanus.Controllers
{
    /*
     * This serves all the NICCDC pages related to NIATEC.
     * Actions are from the About NIATEC nav bar.
     */
    public class NIATECController : Controller
    {
        //TODO: Disabled until the Alumni page is opperational
        public IActionResult Alumni()
        {
            return View();
        }

        public IActionResult Apply()
        {
            return View();
        }

        //TODO: Disabled until the Program page is opperational
        //public IActionResult Program()
        //{
        //    return View();
        //}

        public IActionResult Speaker()
        {
            return View();
        }
    }
}
