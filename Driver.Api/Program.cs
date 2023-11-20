using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace Driver.Api
{
    /// <summary>
    /// Start Point
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class Program
    {


       
      
        /// <summary>
        /// Kick Off
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.EventLog("Application")
                .WriteTo.Console()
                .WriteTo.File(Path.Combine("./Logs", $"{DateTime.Today:dd-MMM-yyyy}.txt"))
                .CreateLogger();

            CreateHostBuilder(args).Build().Run();
        }

        /// <summary>
        /// Web Host Builder
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder
                        .UseStartup<Startup>();
                }).UseSerilog();
    }
}
