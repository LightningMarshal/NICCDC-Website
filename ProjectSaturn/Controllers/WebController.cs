using Microsoft.AspNetCore.Mvc;

namespace ProjectSaturn.Controllers
{
    public class WebController : Controller // This serves the main NICCDC webpages found in the Regular Pages folder. This is the main website.
    {
        public IActionResult Index()
        {
            return Redirect("/NICCDC/index.html");
        }
    }
}