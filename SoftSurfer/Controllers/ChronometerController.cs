using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoftSurfer.Controllers
{
    public class ChronometerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
