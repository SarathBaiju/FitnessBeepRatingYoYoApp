using FitnessRatingBeepCommon;
using FitnessRatingBeepRepository.Contracts;
using FitnessRatingBeepRepository.DataModel;
using FitnessRatingBeepServices.Contracts;
using FitnessRatingBeepServices.DTO;
using FitnessRatingBeepServices.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessRatingBeepServices.Services
{
    public class FitnessRatingService : IFitnessRatingService
    {
        private readonly IFitnessRatingBeepRepository _fitnessRatingBeepRepository;

        public FitnessRatingService(IFitnessRatingBeepRepository fitnessRatingBeepRepository)
        {
            _fitnessRatingBeepRepository = fitnessRatingBeepRepository;
        }
        public async Task<List<FitnessRatingBeepDto>> GetAllFitnessRatingBeepDetails()
        {
            var fitnessRatingBeepData = await _fitnessRatingBeepRepository.GetAllFitnessRatingBeepDetails();
            return fitnessRatingBeepData.ToDtos();
        }

        public async Task<AtheleteFitnessBeepDto> GetAtheleteDtos()
        {
            var atheleteDto = _fitnessRatingBeepRepository.GetAtheleteData().Result.ToDtos();
            return ResolveAtheleteFitnessBeepDto(atheleteDto);
        }

        private AtheleteFitnessBeepDto ResolveAtheleteFitnessBeepDto(List<AtheleteDto> atheleteDtos)
        {
            var atheleteFitnessBeepDto = new AtheleteFitnessBeepDto();
            atheleteFitnessBeepDto.AtheleteDtos = atheleteDtos;
            atheleteFitnessBeepDto.IsAllAtheleteComplete = atheleteDtos.Any(s => !s.IsError) ? false : true;
            return atheleteFitnessBeepDto;
        }

        public async Task<FitnessRatingBeepDto> GetFitnessRatingDetailsByStartTime(string startTime)
        {
            var fitnessRatingBeepData = await _fitnessRatingBeepRepository.GetAllFitnessRatingBeepDetails();
            return fitnessRatingBeepData.Where(s => s.StartTime.Equals(startTime)).FirstOrDefault().ToDto();
        }

        public async Task<bool> UpdateAtheleteWarningOrErrorFlagById(int Id, EnumTypes.ErrorOrWarn errorOrWarn)
        {
            var atheleDatas = await _fitnessRatingBeepRepository.GetAtheleteData();
            var atheleteData = atheleDatas.Where(s => s.Id.Equals(Id)).FirstOrDefault();
            atheleDatas.Remove(atheleteData);

            switch (errorOrWarn)
            {
                case EnumTypes.ErrorOrWarn.Error:
                    atheleteData.IsError = true;
                    break;
                case EnumTypes.ErrorOrWarn.Warn:
                    atheleteData.IsWarning = true;
                    break;
                default:
                    break;
            }

            atheleDatas.Add(atheleteData);
            return await _fitnessRatingBeepRepository.InsertIntoAtheleJsonData(atheleDatas);
        }

        public async Task<bool> UpdateAtheleteResultByTotalDistance(int totalDistance, int id)
        {
            var fitnessBeepData = await _fitnessRatingBeepRepository.GetAllFitnessRatingBeepDetails();
            //var fitnessBeepDataByTotalDistance = fitnessBeepData.Where(s => s.AccumulatedShuttleDistance.Equals(totalDistance.ToString())).FirstOrDefault();
            var fitnessBeepDataByTotalDistance = fitnessBeepData.Where((s) => { 
                return Int32.Parse(s.AccumulatedShuttleDistance)<=totalDistance ;
            }).ToList();
            var allAtheleData = await _fitnessRatingBeepRepository.GetAtheleteData();
            var atheleData = allAtheleData.Where(s => s.Id.Equals(id)).FirstOrDefault();
            allAtheleData.Remove(atheleData);

            atheleData.Result = new List<FitnessRatingBeepRepository.DataModel.ResultData>();

            if(fitnessBeepDataByTotalDistance != null && fitnessBeepDataByTotalDistance.Count>0)
            {
                foreach (var atheleCompleteLevel in fitnessBeepDataByTotalDistance)
                {
                    if(atheleCompleteLevel == null)
                    {
                        atheleData.Result.Add(new ResultData
                        {
                            ShuttleNo = 0,
                            SpeedLevel = 0,
                            IsCurrentStatus = Int32.Parse(atheleCompleteLevel.AccumulatedShuttleDistance) == totalDistance?true:false
                        });
                    }
                    else
                    {
                        atheleData.Result.Add(new ResultData
                        {
                            ShuttleNo = Int32.Parse(atheleCompleteLevel.ShuttleNo),
                            SpeedLevel = Int32.Parse(atheleCompleteLevel.SpeedLevel),
                            IsCurrentStatus = Int32.Parse(atheleCompleteLevel.AccumulatedShuttleDistance) == totalDistance ? true : false
                        });
                    }
                }
            }
            else
            {
                atheleData.Result.Add(new ResultData
                {
                    ShuttleNo = 0,
                    SpeedLevel = 0,
                    IsCurrentStatus = true
                });
            }
           
            allAtheleData.Add(atheleData);
            return await _fitnessRatingBeepRepository.InsertIntoAtheleJsonData(allAtheleData);
        }

        public async Task<AtheleteDto> GetAtheleteDtoById(int id)
        {
            return  _fitnessRatingBeepRepository.GetAtheleteData().Result.Where(s => s.Id.Equals(id)).FirstOrDefault().ToDto();
        }

        public async Task<bool> CheckAllAtheleteFinished()
        {
            return _fitnessRatingBeepRepository.GetAtheleteData().Result.All(s => s.IsError);
        }

        public async Task<bool> UpdateAtheleteResult(AtheleteResultUpdateDto atheleteResultUpdateDto)
        {
            var allAtheleteData = await _fitnessRatingBeepRepository.GetAtheleteData();
            var athelete = _fitnessRatingBeepRepository.GetAtheleteData().Result.Where(s => s.Id.Equals(atheleteResultUpdateDto.UserId)).FirstOrDefault();

            var count = allAtheleteData.RemoveAll(athelete => athelete.Id == atheleteResultUpdateDto.UserId);

            var newAtheleteDataList = allAtheleteData;

            var atheleteResult = athelete.Result;
            var IsCurrentResultData = atheleteResult.Where(s => s.IsCurrentStatus).FirstOrDefault();
            atheleteResult.Remove(IsCurrentResultData);

            //updating old resultData flag to false
            atheleteResult.Add(new ResultData
            {
                IsCurrentStatus = false,
                ShuttleNo = IsCurrentResultData.ShuttleNo,
                SpeedLevel = IsCurrentResultData.SpeedLevel
            });

            //Remove newly Updated resultData from list

            var getResultDataForCurrentShuttle = atheleteResult.Where(s => s.SpeedLevel.Equals(atheleteResultUpdateDto.SpeedLevel)&& s.ShuttleNo.Equals(atheleteResultUpdateDto.ShuttleNo)).FirstOrDefault();

            atheleteResult.Remove(getResultDataForCurrentShuttle);

            //updating new resultData flag to true
            atheleteResult.Add(new ResultData
            {
                IsCurrentStatus = true,
                ShuttleNo = atheleteResultUpdateDto.ShuttleNo,
                SpeedLevel = atheleteResultUpdateDto.SpeedLevel
            });

            athelete.Result = atheleteResult;
            newAtheleteDataList.Add(athelete);
            return await _fitnessRatingBeepRepository.InsertIntoAtheleJsonData(allAtheleteData);
        }
    }
}
