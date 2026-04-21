using Microsoft.AspNetCore.Mvc;

namespace Handball4RO.Controllers
{
    public class AstaziController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
