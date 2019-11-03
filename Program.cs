using System;
using System.Net;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace apicenter
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }


        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();

        // public static void Main(string[] args)
        // {
        //     var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
        //     var isDevelopment = environment == EnvironmentName.Development;

        //     if (!isDevelopment)
        //     {
        //         CreateWebHostBuilder(args).Build().Run();
        //     }
        //     else
        //     {
        //         BuildWebHost(args).Run();
        //     }

        // }


        // public static IWebHostBuilder CreateWebHostBuilder(string[] args)
        // {
        //     var port = Environment.GetEnvironmentVariable("PORT");

        //     return WebHost.CreateDefaultBuilder(args)
        //         .UseStartup<Startup>()
        //         .UseKestrel()
        //         .ConfigureKestrel((context, options) =>
        //         {
        //             options.Listen(IPAddress.IPv6Any, Convert.ToInt32(port));
        //         });
        // }


    }

}
