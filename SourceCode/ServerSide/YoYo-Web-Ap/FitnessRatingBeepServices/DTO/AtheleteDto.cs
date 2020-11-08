using System;
using System.Collections.Generic;
using System.Text;

namespace FitnessRatingBeepServices.DTO
{
    public class AtheleteDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsWarning { get; set; }
        public bool IsError { get; set; }
        public ResultDto ResultDto { get; set; }
    }
    public class AtheleteFitnessBeepDto
    {
        public List<AtheleteDto> AtheleteDtos { get; set; }
        public bool IsAllAtheleteComplete { get; set; }
    }
}
