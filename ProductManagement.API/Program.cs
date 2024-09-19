using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using ProductManagement.API.Extensions;
using ProductManagement.API.Initializers;
using ProductManagement.Infrastructure.DependencyInjection;
using Serilog;
using Serilog.Sinks.MSSqlServer;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Ba�lant�y� test etme kodu
using (var connection = new SqlConnection(connectionString))
{
    try
    {
        connection.Open();
        Console.WriteLine("Connection to SQL Server was successful!");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Connection to SQL Server failed: {ex.Message}");
    }
}
// Serilog'un s�tun se�eneklerini belirleyin
var columnOptions = new ColumnOptions
{
    AdditionalDataColumns = new[]
    {
        new System.Data.DataColumn("Exception", typeof(string)),
        new System.Data.DataColumn("Properties", typeof(string))
    }
};

builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
{
    // AutofacModule s�n�f�n� burada y�kleyin
    containerBuilder.RegisterModule(new AutofacModule());
});

// Serilog'u kullanacak �ekilde host'u yap�land�r�n
builder.Host.UseSerilog();
builder.Host.ConfigureCustomLogging(builder.Configuration);
builder.Services.AddControllers();

// Ba�lant�y� test etme kodu

builder.WebHost.UseUrls("http://*:5000");
builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(80); // HTTP i�in
});


builder.Services.AddCustomServices(builder.Configuration);
builder.Services.AddCustomSwagger();

var app = builder.Build();
await DatabaseInitializer.InitializeAsync(app.Services);

// Ba��ml�l�klar� ekleme

app.UseCustomMiddlewares();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "ProductManagement API v1");
    });
}

app.MapControllers();

app.Run();