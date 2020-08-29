using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ServiceLayer.ServiceLayer;

namespace ServiceLayer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var serviceLayer = new ServiceLayer.ServiceLayer();

            new Entity<Person>().Where(c => c.StringProperty1.StartsWith(null));

            






            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
