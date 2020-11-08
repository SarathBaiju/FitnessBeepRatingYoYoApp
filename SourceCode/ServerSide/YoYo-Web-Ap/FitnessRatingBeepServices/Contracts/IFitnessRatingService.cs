using FitnessRatingBeepCommon;
using FitnessRatingBeepServices.DTO;
using FitnessRatingBeepServices.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FitnessRatingBeepServices.Contracts
{
    public interface IFitnessRatingService
    {
        Task<List<FitnessRatingBeepDto>> GetAllFitnessRatingBeepDetails();
        Task<FitnessRatingBeepDto> GetFitnessRatingDetailsByStartTime(string startTime);
        Task<AtheleteFitnessBeepDto> GetAtheleteDtos();
        Task<bool> UpdateAtheleteWarningOrErrorFlagById(int Id, EnumTypes.ErrorOrWarn errorOrWarn);
    }
}
