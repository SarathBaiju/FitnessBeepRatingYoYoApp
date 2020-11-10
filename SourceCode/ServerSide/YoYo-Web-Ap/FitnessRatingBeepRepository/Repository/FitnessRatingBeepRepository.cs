﻿using FitnessRatingBeepRepository.Contracts;
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
        private const string fitnessRatingBeepJsonPath = @"C:\Users\sarath.bu\Source\Repos\FitnessBeepRatingYoYoApp\SourceCode\ServerSide\YoYo-Web-Ap\FitnessRatingBeepRepository\DataSource\fitnessrating_beeptest.json";
        private const string atheleteBeepJsonPath = @"C:\Users\sarath.bu\Source\Repos\FitnessBeepRatingYoYoApp\SourceCode\ServerSide\YoYo-Web-Ap\FitnessRatingBeepRepository\DataSource\atheleteBeepRating.json";
        public async Task<List<FitnessRatingBeepData>> GetAllFitnessRatingBeepDetails()
        {
            bool IsRead = false;
            var fitnessRatingBeepData = new List<FitnessRatingBeepData>();
            while (!IsRead)
            {
                try
                {
                    string jsonFromFile;
                    using (var reader = new StreamReader(fitnessRatingBeepJsonPath))
                    {
                        jsonFromFile = reader.ReadToEnd();
                        reader.Close();
                    }
                    fitnessRatingBeepData = JsonConvert.DeserializeObject<List<FitnessRatingBeepData>>(jsonFromFile);
                    IsRead = true;
                }
                catch (Exception exception)
                {
                    System.Threading.Thread.Sleep(100);
                }
            }
            return fitnessRatingBeepData;
        }

        public async Task<List<AtheleteData>> GetAtheleteData()
        {
            try
            {
                string jsonFromFile;
                using (var reader = new StreamReader(atheleteBeepJsonPath))
                {
                    jsonFromFile = reader.ReadToEnd();
                    reader.Close();
                }
                var atheleteBeepData = JsonConvert.DeserializeObject<List<AtheleteData>>(jsonFromFile);
                return atheleteBeepData;
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public async Task<bool> InsertIntoAtheleJsonData(List<AtheleteData> atheleteData)
        {
            try
            {
                JsonSerializer serializer = new JsonSerializer();
                using (var streamWriter = new StreamWriter(atheleteBeepJsonPath))
                {
                    using (var jsonWriter = new JsonTextWriter(streamWriter))
                    {
                        serializer.Serialize(jsonWriter,atheleteData);
                    }
                    streamWriter.Close();
                }
                return true;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
