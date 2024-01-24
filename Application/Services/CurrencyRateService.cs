using Domain.Exceptions;
using Domain.Interfaces;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Application.Services
{
    public class CurrencyRateService : ICurrencyRateService
    {
        private readonly ICurrencyRateRepository _repository;
        private readonly List<CurrencyRate> _inMemoryRates;

        public CurrencyRateService(ICurrencyRateRepository repository)
        {
            _repository = repository;
            _inMemoryRates = new List<CurrencyRate>
            {
                new CurrencyRate { CurrencyCode = "EUR", Rate = 1.00M, RateChange = 0.02M },
                new CurrencyRate { CurrencyCode = "USD", Rate = 1.10M, RateChange = -0.01M }
            };
        }

        public async Task<List<CurrencyRate>> GetCurrencyRatesWithChangesAsync(DateTime date)
        {
            if (date > new DateTime(2014, 01, 31))
                throw new InvalidRequestException("The requested date cannot be later than January 31, 2014.");

            var xmlDataToday = await _repository.GetCurrencyDataByDateAsync(date);
            var xmlDataYesterday = await _repository.GetCurrencyDataByDateAsync(date.AddDays(-1));

            var ratesToday = ParseXmlData(xmlDataToday);
            var ratesYesterday = ParseXmlData(xmlDataYesterday);

            if (date.Month == 1 && date.Day == 1)
                throw new NoPreviousDayDataException("No previous day data available for the first day of the year.");
            if (!ratesToday.Any())
                throw new NoDataAvailableException("No currency rate data available for the selected date.");

            foreach (var rate in ratesToday)
            {
                var previousRate = ratesYesterday.FirstOrDefault(r => r.CurrencyCode == rate.CurrencyCode);
                rate.RateChange = previousRate != null ? rate.Rate - previousRate.Rate : 0;
            }

            _inMemoryRates.Clear();
            _inMemoryRates.AddRange(ratesToday);

            return ratesToday.OrderByDescending(r => r.RateChange).ToList();
        }

        public async Task<CurrencyRate> GetCurrencyRateByCodeAsync(string code)
        {
            return _inMemoryRates.FirstOrDefault(c => c.CurrencyCode == code)!;
        }

        private List<CurrencyRate> ParseXmlData(string xmlData)
        {
            var xDoc = XDocument.Parse(xmlData);
            return xDoc.Descendants("item")
                       .Select(item => new CurrencyRate
                       {
                           CurrencyCode = item.Element("currency").Value,
                           Rate = decimal.Parse(item.Element("rate").Value)
                       }).ToList();
        }
    }
}
