namespace Döviz_Kuru_API.Models
{
    public class DataResponse
    {
        public string? Name { get; set; }
        public int? Unit { get; set; }
        public string? CurrencyCode { get; set; }
        public decimal? ForexBuying { get; set; }
        public decimal? ForexSelling { get; set;}
        public decimal? BanknoteBuying { get; set; }
        public decimal? BanknoteSelling { get;set;}
    }

    public class DataErrorResponse
    { 
        public List<DataResponse>? Errors { get; set; }
        public string Error { get; set; }

    }
}
