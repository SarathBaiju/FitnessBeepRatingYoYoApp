using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FitnessRatingBeepServices.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace YoYo_Web_Ap.Controllers.API
{
    [Route("api/fitnessRating")]
    [ApiController]
    public class FitnessRatingBeepApiController : ControllerBase
    {
        private readonly IFitnessRatingService _fitnessRatingService;

        public FitnessRatingBeepApiController(IFitnessRatingService fitnessRatingService)
        {
            _fitnessRatingService = fitnessRatingService;
        }
        [Route("get-all")]
        [HttpGet]
        public async Task<IActionResult> GetAllFitnessRatingBeepDetails()
        {
            return Ok(await _fitnessRatingService.GetAllFitnessRatingBeepDetails());
        }
        [Route("get-ByStartTime")]
        [HttpGet]
        public async Task<IActionResult> GetFitnessRatingBeepDetailsByStartTime(string startTime)
        {
            return Ok(await _fitnessRatingService.GetFitnessRatingDetailsByStartTime(startTime.ToString()));
        }
    }
}