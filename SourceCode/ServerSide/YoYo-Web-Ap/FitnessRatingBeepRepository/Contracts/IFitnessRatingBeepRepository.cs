using FitnessRatingBeepRepository.DataModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FitnessRatingBeepRepository.Contracts
{
    public interface IFitnessRatingBeepRepository
    {
        Task<List<FitnessRatingBeepData>> GetAllFitnessRatingBeepDetails();
        Task<List<AtheleteData>> GetAtheleteData();
        Task<bool> InsertIntoAtheleJsonData(List<AtheleteData> atheleteData);
    }
}
