using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Flex.Models;

namespace Flex.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            
            if (User.Claims.Count() > 0)
            {
                var userType = User.FindFirst("usertype");
                if (userType != null)
                {
                    var userTypeValue = userType.Value;
                    if (userTypeValue == "0")
                    {
                        return View("Officer");
                        //return RedirectToAction("Index", "Courses");
                    }
                    else if (userTypeValue == "1")
                    {
                        return View("Faculty");
                    }
                    else if (userTypeValue == "2")
                    {
                        return View("Student");
                    }

                }
            }
            Console.WriteLine(User.Claims); 
            return View();
        }

        public IActionResult Officer()
        {
            return View();
        }
        public IActionResult Student()
        {
            return View();
        }
        public IActionResult Faculty()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}