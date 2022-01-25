using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using System.Runtime.InteropServices;
using System;

using Serilog;

namespace Service1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            IConfiguration configuration = new ConfigurationBuilder()
                            .AddJsonFile("appsettings.json")
                            .Build();

            var chost = configuration.GetSection("SEQ_IP").Value;


            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Error() // ставим минимальный уровень в Verbose для теста, по умолчанию стоит Information 
                .Enrich.WithProperty("Service", "#1")
                .WriteTo.Seq(chost.ToString())
                .WriteTo.Console()
                .WriteTo.EventLog(source: "Application", manageEventSource: true) 
                .CreateLogger();
            }
            else
            {
                Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Error() // ставим минимальный уровень в Verbose для теста, по умолчанию стоит Information 
                .Enrich.WithProperty("Service", "#1")
                .WriteTo.Seq(chost.ToString())
                .WriteTo.Console()
                //.WriteTo.EventLog(source: "Application", manageEventSource: true) 
                .CreateLogger();
            }
            Log.Logger.Information("TestInfoStartProgram");
            Log.Logger.Information(chost.ToString());

            CreateHostBuilder(args).Build().Run();
        }
       
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSerilog()
                //.ConfigureLogging(ConfigureLogging)
                //.ConfigureLogging((context, logging) =>
                //{
                //    // clear all previously registered providers
                //    logging.ClearProviders();

                //    // now register everything you *really* want
                //    // …
                //    if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                //    {
                //        logging.AddEventLog(eventLogSettings =>
                //        {
                //            eventLogSettings.SourceName = "Service1";
                //        });
                //    }
                //})
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });

        private void ConfigureLogging(ILoggingBuilder logging)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                logging.AddEventLog(eventLogSettings =>
                {
                    eventLogSettings.SourceName = "Service1";
                });
            }
        }

    }
}
