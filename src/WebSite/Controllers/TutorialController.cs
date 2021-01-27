using Microsoft.AspNetCore.Mvc;

namespace WebSite.Controllers
{
    public class TutorialController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Persistence()
        {
            return View();
        }

        public IActionResult Persistence1()
        {
            return View();
        }
        
        public IActionResult Persistence2()
        {
            return View();
        }

        public IActionResult MultiContainer()
        {
            return View();
        }
    }
}