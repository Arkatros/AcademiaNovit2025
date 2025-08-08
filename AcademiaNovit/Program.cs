using AcademiaNovit;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using Serilog;
using Serilog.Sinks.Grafana.Loki;

var builder = WebApplication.CreateBuilder(args);

#region Configuración de Serilog

// Cargar configuración desde appsettings.json
builder.Configuration.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

// Configurar Serilog usando la configuración del JSON
builder.Host.UseSerilog((context, loggerConfiguration) =>
{
    loggerConfiguration
        .ReadFrom.Configuration(context.Configuration) // Lee sinks y niveles desde appsettings.json
        .Enrich.FromLogContext()
        .Enrich.WithProperty("app", "academia"); // Etiqueta global para Loki
});

#endregion

#region Variables de entorno

builder.Configuration.AddEnvironmentVariables();

#endregion

// Conexión a la base de datos
string connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddDbContext<AppDbContext>(options => options.UseNpgsql(connectionString));

// OpenAPI y controladores
builder.Services.AddOpenApi();
builder.Services.AddControllers();

var app = builder.Build();

// Migraciones automáticas al iniciar
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    dbContext.Database.Migrate();
}

app.MapOpenApi();
app.MapScalarApiReference();
app.MapControllers();

#region Endpoint Keep Alive
app.MapGet("/keep-alive", () => new
{
    status = "alive",
    timestamp = DateTime.UtcNow
});
#endregion

app.Run();

public partial class Program { }
