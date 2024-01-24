using Application.Services;
using Domain.Interfaces;
using Exchange.WebAPI.Middlewares;
using Infrastructure.Repositories;
using Microsoft.OpenApi.Models;
using System.Reflection;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpClient<ICurrencyRateRepository, CurrencyRateRepository>();
builder.Services.AddScoped<ICurrencyRateService, CurrencyRateService>();

builder.Services.AddDbContext<MyAppContext>(options =>
    options.UseInMemoryDatabase("MyInMemoryDb"));

builder.Services.AddSwaggerGen(c =>
{
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var currencyRateService = services.GetRequiredService<ICurrencyRateService>();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseMiddleware<ErrorHandlingMiddleware>();

app.Run();
