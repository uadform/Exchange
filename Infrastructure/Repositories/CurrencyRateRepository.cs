using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class CurrencyRateRepository : ICurrencyRateRepository
    {
        private readonly HttpClient _httpClient;

        public CurrencyRateRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> GetCurrencyDataByDateAsync(DateTime date)
        {
            var response = await _httpClient.GetAsync($"http://www.lb.lt/webservices/ExchangeRates/ExchangeRates.asmx/getExchangeRatesByDate?Date={date:yyyy-MM-dd}");
            return await response.Content.ReadAsStringAsync();
        }
    }
}
