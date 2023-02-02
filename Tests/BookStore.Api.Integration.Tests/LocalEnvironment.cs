using System;
using System.Net.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.Sqlite;
using BookStore.DataStore;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;

namespace BookStore.Api.Integration.Tests
{
    public class LocalEnvironment : IEnvironment
    {
        public HttpClient Client { get; set; }
        private readonly TestServer _server;
        private readonly DbContextOptions<BookStoreContext> _options;
        public BookStoreContext BookStoreContext {get; internal set;}
        public LocalEnvironment()
        {
            var conection = new SqliteConnection("DataSource=:memory:");
            conection.Open();

            _options = new DbContextOptionsBuilder<BookStoreContext>().UseSqlite(conection).Options;

            BookStoreContext = new BookStoreContext(_options);
            BookStoreContext.Database.EnsureCreated();

            var builder = new WebHostBuilder()
                .ConfigureServices(servicesCollection => 
                        servicesCollection.AddScoped(_ => _options)).UseStartup<TestStartUp>();

            _server = new TestServer(builder);
            Client = _server.CreateClient();
        }

        public void RefreshDbConnection()
        {
            BookStoreContext = new BookStoreContext(_options);
        }

        public void Dispose()
        {
            Client.Dispose();
            _server.Dispose();
        }
    }
}