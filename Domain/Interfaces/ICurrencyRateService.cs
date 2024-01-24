using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface ICurrencyRateService
    {
        Task<List<CurrencyRate>> GetCurrencyRatesWithChangesAsync(DateTime date);
        Task<CurrencyRate> GetCurrencyRateByCodeAsync(string code);
    }
}
