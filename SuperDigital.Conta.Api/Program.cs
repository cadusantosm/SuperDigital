using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;
using Microsoft.Extensions.Configuration;

namespace SuperDigital.Conta.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            Serilog.Debugging.SelfLog.Enable(Console.Error);

            return Host.CreateDefaultBuilder(args)
                .UseSerilog((hostingContext, loggerConfiguration) => loggerConfiguration
                    .ReadFrom.Configuration(hostingContext.Configuration)
                    .WriteTo.Console()
                    .Enrich.FromLogContext()
                    .Enrich.WithAssemblyName()
                    .Enrich.WithAssemblyInformationalVersion()
                )
                .ConfigureAppConfiguration(config =>
                {
                    var configurationsRoots = config.Build();

                    config.AddAzureKeyVault(configurationsRoots["AzureKeyVault:DNS"],
                        configurationsRoots["AzureKeyVault:ClientId"],
                        configurationsRoots["AzureKeyVault:ClientSecret"]);
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                    webBuilder.ConfigureKestrel(options => options.AllowSynchronousIO = true);
                }).ConfigureAppMetricsHostingConfiguration(options =>
                    options.MetricsEndpoint = "/internal/metrics");
        }
    }
}