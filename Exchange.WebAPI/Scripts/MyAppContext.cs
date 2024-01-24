using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class MyAppContext : DbContext
{
    public MyAppContext(DbContextOptions<MyAppContext> options) : base(options)
    {
    }

    public DbSet<CurrencyRate> CurrencyRates { get; set; }

}

