using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using FitnessRatingBeepCommon;
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
        [Route("get-athelete-details")]
        [HttpGet]
        public async Task<IActionResult> GetAtheleteDetails()
        {
            return Ok(await _fitnessRatingService.GetAtheleteDtos());
        }
        [Route("change-athelete-status")]
        [HttpPost]
        public async Task<IActionResult> WarnOrErrorAthelete(int Id, EnumTypes.ErrorOrWarn errorOrWarn)
        {
            return Ok(await _fitnessRatingService.UpdateAtheleteWarningOrErrorFlagById(Id, errorOrWarn));
        }
    }
}

