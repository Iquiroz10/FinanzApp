using FinanzApp.Application.Interfaces.Repositories;
using FinanzApp.Application.Interfaces.Services;
using FinanzApp.Application.Mappings;
using FinanzApp.Application.Services;
using FinanzApp.Infrastructure.Data;
using FinanzApp.Infrastructure.Repositories;
using Mapster;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Controllers
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters
            .Add(new System.Text.Json.Serialization.JsonStringEnumConverter());
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// DbContext
builder.Services.AddDbContext<FinanzAppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")));

// Mapster
var mappingConfig = TypeAdapterConfig.GlobalSettings;
mappingConfig.Scan(typeof(MappingConfig).Assembly);
builder.Services.AddSingleton(mappingConfig);

// Repositorios
builder.Services.AddScoped<IIncomeRepository, IncomeRepository>();
builder.Services.AddScoped<IExpenseRepository, ExpenseRepository>();
builder.Services.AddScoped<IDebtRepository, DebtRepository>();
builder.Services.AddScoped<IInvestmentRepository, InvestmentRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();

// Servicios
builder.Services.AddScoped<IIncomeService, IncomeService>();
builder.Services.AddScoped<IExpenseService, ExpenseService>();
builder.Services.AddScoped<IDebtService, DebtService>();
builder.Services.AddScoped<IInvestmentService, InvestmentService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();