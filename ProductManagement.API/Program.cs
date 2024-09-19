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

// Baðlantýyý test etme kodu
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
// Serilog'un sütun seçeneklerini belirleyin
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
    // AutofacModule sýnýfýný burada yükleyin
    containerBuilder.RegisterModule(new AutofacModule());
});

// Serilog'u kullanacak þekilde host'u yapýlandýrýn
builder.Host.UseSerilog();
builder.Host.ConfigureCustomLogging(builder.Configuration);
builder.Services.AddControllers();

// Baðlantýyý test etme kodu

builder.WebHost.UseUrls("http://*:5000");
builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(80); // HTTP için
});


builder.Services.AddCustomServices(builder.Configuration);
builder.Services.AddCustomSwagger();

var app = builder.Build();
await DatabaseInitializer.InitializeAsync(app.Services);

// Baðýmlýlýklarý ekleme

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