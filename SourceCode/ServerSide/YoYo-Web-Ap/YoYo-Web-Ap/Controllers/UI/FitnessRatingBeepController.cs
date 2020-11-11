using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FitnessRatingBeepServices.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace YoYo_Web_Ap.Controllers.UI
{
    [Route("yoyo-app")]
    public class FitnessRatingBeepController : Controller
    {
        #region PRIVATE INSTANCE FIELDS

        private readonly IFitnessRatingService _fitnessRatingService;

        #endregion

        #region PUBLIC METHODS
        public FitnessRatingBeepController(IFitnessRatingService fitnessRatingService)
        {
            this._fitnessRatingService = fitnessRatingService;
        }
        [Route("home")]
        public async Task<IActionResult> Index()
        {
            return View(await _fitnessRatingService.GetAtheleteDtos());
        }
        #endregion
    }
}