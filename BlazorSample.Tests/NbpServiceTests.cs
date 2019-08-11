using System;
using System.Collections.Generic;
using System.Linq;
using BlazorSample.Data;
using Xunit;

namespace BlazorSample.Tests
{
    public class NbpServiceTests
    {
        private NbpService _nbpService;

        public NbpServiceTests()
        {
            _nbpService = new NbpService();
        }

        [Fact]
        public void RefreshingWorking()
        {
            var nbpService = new NbpService();

            nbpService.RefreshAsync().Wait();

            Assert.Equal(DateTime.Today, nbpService.GetEffectiveDateAsync().Result);
        }

        [Fact]
        //NBP for "table a" returns 35 "most popular" currency rates
        public void RightNumberOfSupportedCurrencyCodeObtained()
        {
            var currencyCodes = _nbpService.GetCurrencyCodesAsync().Result;
            Assert.Equal(35, currencyCodes.Count());
        }

        [Theory]
        [MemberData(nameof(SupportedCurrencyCodeObtainedData))]
        //NBP for "table a" returns 35 "most popular" currency rates
        public void SupportedCurrencyCodeObtained(string currencyCode)
        {
            var currencyCodes = _nbpService.GetCurrencyCodesAsync().Result;
            Assert.Contains(currencyCodes, cc => cc == currencyCode);
        }

        public static IEnumerable<object[]> SupportedCurrencyCodeObtainedData =>
            new List<object[]>
            {
                new object[] {"THB" },
                new object[] {"USD"},
                new object[] {"AUD"},
                new object[] {"HDK"},
                new object[] {"CAD"},
                new object[] {"NZD"},
                new object[] {"SGD"},
                new object[] {"EUR"},
                new object[] {"HUF"},
                new object[] {"CHF"},
                new object[] {"GBP"},
                new object[] {"UAH"},
                new object[] {"JPY"},
                new object[] {"CZK"},
                new object[] {"DKK"},
                new object[] {"ISK"},
                new object[] {"NDK"},
                new object[] {"SEK"},
                new object[] {"HRK"},
                new object[] {"RON"},
                new object[] {"BGN"},
                new object[] {"TRY"},
                new object[] {"ILS"},
                new object[] {"CLP"},
                new object[] {"PHP"},
                new object[] {"MXN"},
                new object[] {"ZAR"},
                new object[] {"BRL"},
                new object[] {"MYR"},
                new object[] {"RUB"},
                new object[] {"IDR"},
                new object[] {"INR"},
                new object[] {"KRW"},
                new object[] {"CNY"},
                new object[] {"XDR"}
            };

        [Fact]
        public void SameCurrencyRate()
        {
            Assert.Equal(1, _nbpService.GetRate(NbpService.BaseCurrency, NbpService.BaseCurrency));
        }
    }
}
