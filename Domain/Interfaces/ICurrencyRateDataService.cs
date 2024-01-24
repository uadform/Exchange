using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    internal interface ICurrencyRateDataService
    {
        Task<List<CurrencyRate>> GetAllCurrencyRatesAsync();
        Task<CurrencyRate> GetCurrencyRateByCodeAsync(string code);
        Task UpdateCurrencyRatesAsync(IEnumerable<CurrencyRate> currencyRates);
    }
}
