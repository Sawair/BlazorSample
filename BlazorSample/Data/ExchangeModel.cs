namespace BlazorSample.Data
{
    public class ExchangeModel
    {
        public decimal FromValue { get; set; }
        public string FromCurrency { get; set; }
        public decimal? ToValue { get; set; }
        public string ToCurrency { get; set; }
    }
}
