using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication6.Controllers
{
    public class Home2Controller : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}