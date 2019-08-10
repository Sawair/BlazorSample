using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace BlazorSample.Data
{
    public class NbpService
    {
        public const string BaseCurrency = "PLN";

        public async Task<DateTime> GetEffectiveDateAsync()
        {
            await RefreshIfNeededAsync();
            return _nbpTable.EffectiveDate;
        }

        public async Task<IEnumerable<string>> GetCurrencyCodesAsync()
        {
            await RefreshIfNeededAsync();
            return _nbpTable.Rates.Select(r => r.Code).ToList();
        }

        private async Task RefreshIfNeededAsync()
        {
            if (NeedsToRefresh)
            {
                await this.RefreshAsync();
            }
        }

        private bool NeedsToRefresh => _nbpTable == null || (_nbpTable.EffectiveDate.Date - DateTime.Now) > TimeSpan.FromDays(1);

        private NbpTable _nbpTable;

        public async Task RefreshAsync(bool force = false)
        {
            if (!NeedsToRefresh && !force)
            {
                return;
            }

            var httpClient = new HttpClient();

            var httpResponseMessage =
                await httpClient.GetAsync(@"http://api.nbp.pl/api/exchangerates/tables/a?format=json");

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                var nbpTables = await httpResponseMessage.Content.ReadAsAsync<NbpTable[]>();
                _nbpTable = nbpTables.First();
            }
        }

        public decimal GetRate(string baseCurrCode, string targetCurrCode)
        {
            if (baseCurrCode == targetCurrCode)
            {
                return 1;
            }
            else if (baseCurrCode == BaseCurrency)
            {
                return 1 / GetRate(targetCurrCode);
            }
            else if (targetCurrCode == BaseCurrency)
            {
                return GetRate(baseCurrCode);
            }
            else
            {
                var firstRate = GetRate(baseCurrCode);
                var secondRate = GetRate(targetCurrCode);

                return firstRate / secondRate;
            }
        }

        private decimal GetRate(string currCode)
        {
            return _nbpTable.Rates.First(r => r.Code == currCode).Mid;
        }
    }
}
