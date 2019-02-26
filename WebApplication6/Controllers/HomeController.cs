using System.Diagnostics;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using WebApplication6.Models;

namespace WebApplication6.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
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

        public IActionResult GitHub()
        {
            return Challenge(new AuthenticationProperties() { RedirectUri = "/Home/" }, "Github");
        }

        public IActionResult Facebook()
        {
            return Challenge(new AuthenticationProperties() { RedirectUri = "/Home/" }, "Facebook");
        }

        public IActionResult Google()
        {
            return Challenge(new AuthenticationProperties() { RedirectUri = "/Home/" }, "Google");
        }

        public IActionResult Logout()
        {
            HttpContext.SignOutAsync();
            return Redirect("/Home/");
        }
    }
}
