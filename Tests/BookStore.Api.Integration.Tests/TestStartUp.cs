using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Hosting;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using BookStore.DataStore;

namespace BookStore.Api.Integration.Tests
{
    public class TestStartUp : StartUp
    {
        public IConfiguration Confiduration { get; set; }

        public TestStartUp(IConfiguration configuration) : base(configuration)
        {
            Confiduration = configuration;
        }

        protected override void ConfigureDb(IServiceCollection services)
        {
            var controllerAssemblyType = typeof(StartUp).GetTypeInfo().Assembly;
            services.AddControllers().AddApplicationPart(controllerAssemblyType);

            var serviceProvider = services.BuildServiceProvider();
            var options = serviceProvider.GetService<DbContextOptions<BookStoreContext>>();
            services.AddScoped(s => new BookStoreContext(options));
        }
    }
}