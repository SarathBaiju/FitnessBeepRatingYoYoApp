using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using FitnessRatingBeepCommon;
using FitnessRatingBeepServices.Contracts;
using FitnessRatingBeepServices.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using YoYo_Web_Ap.Models;

namespace YoYo_Web_Ap.Controllers.API
{
    [Route("api/fitnessRating")]
    [ApiController]
    public class FitnessRatingBeepApiController : ControllerBase
    {

        #region PRIVATE INSTANCE FIELDS
        private readonly IFitnessRatingService _fitnessRatingService;
        #endregion

        #region CONSTRUCTOR

        public FitnessRatingBeepApiController(IFitnessRatingService fitnessRatingService)
        {
            _fitnessRatingService = fitnessRatingService;
        }

        #endregion

        #region PUBLIC METHODS
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
            var atheleteFitnessBeepDto = await _fitnessRatingService.GetAtheleteDtos();
            return Ok(MapAtheleteFitnessBeepDtoToViewModel(atheleteFitnessBeepDto));
        }

        [Route("get-athelete-ById")]
        [HttpGet]
        public async Task<IActionResult> GetAtheleteDetailsById(int id)
        {
            return Ok(await _fitnessRatingService.GetAtheleteDtoById(id));
        }
        [Route("check-all-athelete-completed")]
        [HttpGet]
        public async Task<IActionResult> CheckAllAtheleteFinished()
        {
            return Ok(await _fitnessRatingService.CheckAllAtheleteFinished());
        }
        [Route("change-athelete-status")]
        [HttpPost]
        public async Task<IActionResult> WarnOrErrorAthelete([FromBody] AtheleteAlertViewModel atheleteAlertViewModel)
        {
            await _fitnessRatingService.UpdateAtheleteWarningOrErrorFlagById(atheleteAlertViewModel.Id, atheleteAlertViewModel.ErrorOrWarn);

            if(atheleteAlertViewModel.ErrorOrWarn == EnumTypes.ErrorOrWarn.Error)
            await _fitnessRatingService.UpdateAtheleteResultByTotalDistance(atheleteAlertViewModel.TotalDistance, atheleteAlertViewModel.Id);

            return Ok(true);
        }
        [Route("update-athele-result")]
        [HttpPut]
        public async Task<IActionResult> UpdateAtheleteResult([FromBody] AtheleteUpdateResultViewModel atheleteUpdateResultViewModel)
        {
            var atheleUpdateResultDto = new AtheleteResultUpdateDto
            {
                UserId = atheleteUpdateResultViewModel.UserId,
                SpeedLevel = atheleteUpdateResultViewModel.SpeedLevel,
                ShuttleNo = atheleteUpdateResultViewModel.ShuttleNumber
            };
            return Ok(_fitnessRatingService.UpdateAtheleteResult(atheleUpdateResultDto));
        }

        #endregion

        #region PRIVATE METHODS
        private AtheleteFitnessBeepViewModel MapAtheleteFitnessBeepDtoToViewModel(AtheleteFitnessBeepDto atheleteFitnessBeepDto)
        {
            var atheleteFitnessBeepViewModel = new AtheleteFitnessBeepViewModel();
            if (atheleteFitnessBeepDto == null)
            {
                return atheleteFitnessBeepViewModel;
            }
            else
            {
                atheleteFitnessBeepViewModel.AtheleteViewModels = new List<AtheleteViewModel>();
                foreach (var atheleteDto in atheleteFitnessBeepDto.AtheleteDtos)
                {
                    if (atheleteDto != null)
                    {
                        atheleteFitnessBeepViewModel.AtheleteViewModels.Add(new AtheleteViewModel
                        {
                            Id = atheleteDto.Id,
                            Name = atheleteDto.Name,
                            IsError = atheleteDto.IsError,
                            IsWarning = atheleteDto.IsWarning,
                            ResultViewModel = MapToResultViewModel(atheleteDto.ResultDto.Where(s => s.IsCurrentStatus).FirstOrDefault())
                        });
                    }
                }

            }
            return atheleteFitnessBeepViewModel;
        }

        private ResultViewModel MapToResultViewModel(ResultDto resultDto)
        {
            var resultViewModel = new ResultViewModel();
            if (resultDto == null)
            {
                return resultViewModel;
            }
            resultViewModel.IsCurrentStatus = resultDto.IsCurrentStatus;
            resultViewModel.ShuttleNo = resultDto.ShuttleNo;
            resultViewModel.SpeedLevel = resultDto.SpeedLevel;
            return resultViewModel;
        }

        #endregion
    }
}

