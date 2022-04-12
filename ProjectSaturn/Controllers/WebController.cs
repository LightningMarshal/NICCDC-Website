using Microsoft.AspNetCore.Mvc;

namespace ProjectSaturn.Controllers
{
    public class WebController : Controller
    {
        public IActionResult Index()
        {
            return Redirect("/NICCDC/index.html");
        }
    }
}
