using FitnessRatingBeepRepository.Contracts;
using FitnessRatingBeepRepository.DataModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace FitnessRatingBeepRepository.Repository
{
    public class FitnessRatingBeepRepository : IFitnessRatingBeepRepository
    {
        private const string fitnessRatingBeepJsonPath = @"D:\Project\DigitalMinds\SourceCode\ServerSide\YoYo-Web-Ap\FitnessRatingBeepRepository\DataSource\fitnessrating_beeptest.json"; 
        public async Task<List<FitnessRatingBeepData>> GetAllFitnessRatingBeepDetails()
        {
            try
            {
                string jsonFromFile;
                using (var reader = new StreamReader(fitnessRatingBeepJsonPath))
                {
                    jsonFromFile = reader.ReadToEnd();
                }
                var fitnessRatingBeepData = JsonConvert.DeserializeObject<List<FitnessRatingBeepData>>(jsonFromFile);
                return fitnessRatingBeepData;
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
    }
}
