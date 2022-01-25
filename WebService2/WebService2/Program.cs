using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Serilog;

namespace WebService2
{
    public class Program
    {
        public static void Main(string[] args)
        {
            IConfiguration configuration = new ConfigurationBuilder()
                              .AddJsonFile("appsettings.json")
                              .Build();

            var chost = configuration.GetSection("SEQ_IP").Value;

            Log.Logger = new LoggerConfiguration()
           .MinimumLevel.Error() // ставим минимальный уровень в Verbose для теста, по умолчанию стоит Information 
           .Enrich.WithProperty("Service", "#2")
           .WriteTo.Seq(chost.ToString())  
           .WriteTo.File(@"logs/logServiceTest.txt")  
           .WriteTo.Console()  
                                           
            .CreateLogger();
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSerilog()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
