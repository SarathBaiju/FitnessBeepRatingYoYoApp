using FitnessRatingBeepRepository.DataModel;
using FitnessRatingBeepServices.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace FitnessRatingBeepServices.Helper
{
    public static class AtheleteDtoMappingHelperDto
    {
        public static List<AtheleteDto> ToDtos(this List<AtheleteData> atheleteDatas)
        {
            if(atheleteDatas == null)
            {
                return new List<AtheleteDto>();
            }
            var atheleteDtos = new List<AtheleteDto>();
            foreach (var atheleteData in atheleteDatas)
            {
                atheleteDtos.Add(atheleteData.ToDto());
            }
            return atheleteDtos;
        }
        public static AtheleteDto ToDto(this AtheleteData atheleteData)
        {
            if(atheleteData == null)
            {
                return new AtheleteDto();
            }
            var atheleteDto = new AtheleteDto();
            atheleteDto.Id = atheleteData.Id;
            atheleteDto.Name = atheleteData.Name;
            atheleteDto.IsWarning = atheleteData.IsWarning;
            atheleteDto.IsError = atheleteData.IsError;
            atheleteDto.ResultDto = atheleteData.Result != null? new ResultDto { ShuttleNo = atheleteData.Result.ShuttleNo, SpeedLevel = atheleteData.Result.SpeedLevel }:new ResultDto();
            return atheleteDto;
        }
    }
}
