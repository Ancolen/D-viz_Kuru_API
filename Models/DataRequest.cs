namespace Döviz_Kuru_API.Models
{
    public class DataRequest
    {
        public int Day { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public bool IsToday { get; set; }
    }
}
