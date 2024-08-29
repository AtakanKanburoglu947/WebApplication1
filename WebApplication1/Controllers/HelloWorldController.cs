using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{
    public class HelloWorldController : Controller
    {
        public IActionResult Index()
        {
         
            return View();
        }
        public IActionResult Welcome(string name, int numtimes = 1)
        {
            return View();
        }
    }
}
