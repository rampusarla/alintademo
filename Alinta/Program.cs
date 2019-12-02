using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Alinta.Core.Infrastructure;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using NLog.Web;

namespace Alinta
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var logger = NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();

            try
            {
                //1. Get the IWebHost which will host this application.
                var host = CreateWebHostBuilder(args).Build();

                //2. Find the service layer within our scope.
                using (var scope = host.Services.CreateScope())
                {
                    //3. Get the instance of BoardGamesDBContext in our services layer
                    var services = scope.ServiceProvider;
                    var context = services.GetRequiredService<CustomerDBContext>();

                    //4. Call the DataGenerator to create sample data
                    DataGenerator.Initialize(services);
                }

                //Continue to run the application
                host.Run();
            }            
            catch(Exception ex)
            {
                logger.Error(ex, "Program unexpectedly stopped because of an exception");
            }
            finally
            {
                NLog.LogManager.Shutdown();
            }
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .ConfigureLogging(logging =>
                {
                    logging.ClearProviders();
                    logging.SetMinimumLevel(LogLevel.Trace);
                })
                .UseNLog();
    }
}
