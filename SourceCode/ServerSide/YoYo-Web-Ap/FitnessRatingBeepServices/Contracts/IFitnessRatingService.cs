using FitnessRatingBeepServices.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FitnessRatingBeepServices.Contracts
{
    public interface IFitnessRatingService
    {
        Task<List<FitnessRatingBeepDto>> GetAllFitnessRatingBeepDetails();
        Task<FitnessRatingBeepDto> GetFitnessRatingDetailsByStartTime(string startTime);
    }
}
