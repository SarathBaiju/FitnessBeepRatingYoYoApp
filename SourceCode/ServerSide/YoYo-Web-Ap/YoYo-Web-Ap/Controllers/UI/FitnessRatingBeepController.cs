using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace YoYo_Web_Ap.Controllers.UI
{
    [Route("yoyo-app")]
    public class FitnessRatingBeepController : Controller
    {
        [Route("home")]
        public IActionResult Index()
        {
            return View();
        }
    }
}