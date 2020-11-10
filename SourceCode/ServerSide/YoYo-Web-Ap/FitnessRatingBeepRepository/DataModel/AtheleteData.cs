using System;
using System.Collections.Generic;
using System.Text;

namespace FitnessRatingBeepRepository.DataModel
{
    public class AtheleteData
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsWarning { get; set; }
        public bool IsError { get; set; }
        public List<ResultData> Result { get; set; }
    }
}
