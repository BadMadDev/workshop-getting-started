using System.ComponentModel.Design.Serialization;
using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing.Constraints;

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

        public IActionResult DockerComposeIntro()
        {
            return View("docker-compose-intro");

        }

        public IActionResult Portainer()
        {
            return View();
        }

    }
}