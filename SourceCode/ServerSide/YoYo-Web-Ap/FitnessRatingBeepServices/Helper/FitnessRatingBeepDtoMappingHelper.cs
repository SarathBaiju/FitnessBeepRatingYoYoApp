using FitnessRatingBeepRepository.DataModel;
using FitnessRatingBeepServices.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace FitnessRatingBeepServices.Helper
{
    public static class FitnessRatingBeepDtoMappingHelper
    {
        public static List<FitnessRatingBeepDto> ToDtos(this List<FitnessRatingBeepData> fitnessRatingBeepDatas)
        {
            if (fitnessRatingBeepDatas == null)
            {
                return null;
            }
            var fitnessRatingBeepDtos = new List<FitnessRatingBeepDto>();
            foreach (var fitnessRatingBeepData in fitnessRatingBeepDatas)
            {
                fitnessRatingBeepDtos.Add(fitnessRatingBeepData.ToDto());
            }
            return fitnessRatingBeepDtos;
        }
        public static FitnessRatingBeepDto ToDto(this FitnessRatingBeepData fitnessRatingBeepData)
        {
            if(fitnessRatingBeepData == null)
            {
                return null;
            }
            return new FitnessRatingBeepDto
            {
                AccumulatedShuttleDistance = fitnessRatingBeepData.AccumulatedShuttleDistance,
                SpeedLevel = fitnessRatingBeepData.SpeedLevel,
                ShuttleNo = fitnessRatingBeepData.ShuttleNo,
                Speed= fitnessRatingBeepData.Speed,
                LevelTime = fitnessRatingBeepData.LevelTime,
                CommulativeTime = fitnessRatingBeepData.CommulativeTime,
                StartTime = fitnessRatingBeepData.StartTime,
                ApproxVo2Max = fitnessRatingBeepData.ApproxVo2Max
            };
        }
    }
}
