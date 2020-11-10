using FitnessRatingBeepCommon;
using System.Collections.Generic;

namespace YoYo_Web_Ap.Models
{
    public class AtheleteAlertViewModel
    {
        public int Id { get; set; }
        public EnumTypes.ErrorOrWarn ErrorOrWarn { get; set; }
        public int TotalDistance { get; set; }
    }
    public class AtheleteFitnessBeepViewModel
    {
        public List<AtheleteViewModel> AtheleteViewModels { get; set; }
        public bool IsAllAtheleteComplete { get; set; }
    }
    public class AtheleteViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsWarning { get; set; }
        public bool IsError { get; set; }
        public ResultViewModel ResultViewModel { get; set; }
    }
    public class ResultViewModel
    {
        public int SpeedLevel { get; set; }
        public int ShuttleNo { get; set; }
        public bool IsCurrentStatus { get; set; }
    }
}
