using FinanzApp.Application.Mappings;
using FinanzApp.Infrastructure.Data;
using Mapster;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
// Mapster — escanea y registra todos los IRegister del Assembly
var mappingConfig = TypeAdapterConfig.GlobalSettings;
mappingConfig.Scan(typeof(MappingConfig).Assembly);
builder.Services.AddSingleton(mappingConfig);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Registro del DbContext
builder.Services.AddDbContext<FinanzAppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")));

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