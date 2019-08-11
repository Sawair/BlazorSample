namespace BlazorSample.Data
{
    public class ExchangeModel
    {
        public decimal FromValue { get; set; } = 0.0M;
        public string FromCurrency { get; set; } = NbpService.BaseCurrency;
        public decimal? ToValue { get; set; }
        public string ToCurrency { get; set; } = NbpService.BaseCurrency;
    }
}
