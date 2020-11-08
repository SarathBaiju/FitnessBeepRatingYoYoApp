using FitnessRatingBeepCommon;

namespace YoYo_Web_Ap.Models
{
    public class AtheleteAlertViewModel
    {
        public int Id { get; set; }
        public EnumTypes.ErrorOrWarn ErrorOrWarn { get; set; }
        public int TotalDistance { get; set; }
    }
}
