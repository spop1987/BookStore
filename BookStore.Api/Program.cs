using BookStore.DataStore;
using BookStore.DataStore.DataAccess;
using BookStore.Service.PersonService;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder => {
                webBuilder.UseStartup<StartUp>();
            });
        
    }
}