using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Filters;
using Serilog.Sinks.MSSqlServer;
using System;

namespace ProductManagement.API.Extensions
{
    public static class LoggingExtensions
    {
        public static void ConfigureCustomLogging(this IHostBuilder hostBuilder, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            try
            {
                Log.Logger = new LoggerConfiguration()
                    .MinimumLevel.Information()
                    .Filter.ByExcluding(Matching.FromSource("Microsoft"))
                    .Filter.ByExcluding(Matching.FromSource("System"))
                    .Filter.ByExcluding(Matching.FromSource("Microsoft.Hosting.Lifetime"))
                    .Filter.ByExcluding(logEvent => logEvent.MessageTemplate.Text.Contains("Some unneeded message"))
                    .WriteTo.Console()
                    .WriteTo.File("logs/log.txt", rollingInterval: RollingInterval.Day)
                    .WriteTo.MSSqlServer(
                        connectionString: connectionString,
                        sinkOptions: new MSSqlServerSinkOptions
                        {
                            TableName = "Logs",
                            AutoCreateSqlTable = true
                        })
                    .CreateLogger();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error configuring Serilog: {ex.Message}");
            }

        }
    }
}