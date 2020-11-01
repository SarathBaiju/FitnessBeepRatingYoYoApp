using FitnessRatingBeepRepository.Contracts;
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

        public async Task<FitnessRatingBeepDto> GetFitnessRatingDetailsByStartTime(string startTime)
        {
            var fitnessRatingBeepData = await _fitnessRatingBeepRepository.GetAllFitnessRatingBeepDetails();
            return fitnessRatingBeepData.Where(s => s.StartTime.Equals(startTime)).FirstOrDefault().ToDto();
        }
    }
}
