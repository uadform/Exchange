﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class CurrencyRateChangeResponse
    {
        public string? CurrencyCode { get; set; }
        public decimal Rate { get; set; }
        public decimal RateChange { get; set; }
    }
}
