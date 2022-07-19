using Microsoft.AspNetCore.Mvc;

namespace ProjectJanus.Controllers
{
    public class NICCDCController : Controller // This serves the main NICCDC webpages found in the Regular Pages folder. This is the main website.
    {
        public IActionResult Index()
        {
            return Redirect("/NICCDC/index.html");
        }
    }
}