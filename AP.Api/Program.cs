using Serilog;
using AP.Logging;
using AP.Api.Configuracion;
using AP.Infraestructura.Persistencia;
using AP.Infraestructura.Configuracion;
using AP.Aplicacion.Configuracion;
using Microsoft.AspNetCore.Mvc;
using AP.Validadores;

var builder = WebApplication.CreateBuilder(args);
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});



// Add services to the container.

builder.Services.AddSingleton<IDbConfiguracion, DbConfiguracion>();
builder.Services.InyeccionInfraestructura();
builder.Services.InyeccionAplicacion();
builder.Services.InyeccionValidadores();
builder.Services.AddControllers();




// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Configuracion Serilog
var logsDirectory = Path.Combine(Directory.GetCurrentDirectory(), "Logs");
SerilogConfigurator.ConfigureGlobalLogger(logsDirectory);
builder.Host.UseSerilog(); // Configurar Serilog como proveedor de logging

builder.Services.AddLogging(); // Agregar ILogger al contenedor


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
