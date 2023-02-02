using System;
using System.Net.Http;
using BookStore.Common.DataCreate;
using System.Net;
using System.Threading.Tasks;
using BookStore.Models.Entities;
using Newtonsoft.Json.Linq;

namespace BookStore.Api.Integration.Tests
{
    public interface IEnvironment
    {
        HttpClient Client {get;}
    }
    public class BookStoreApiFixture
    {
        private readonly IEnvironment _environment;
        public HttpClient Client => _environment.Client;

        public BookStoreApiFixture()
        {
            Console.WriteLine("Local environment has just been selected");
            _environment = new LocalEnvironment();
            InitializeDefaultValues();
        }

        private void InitializeDefaultValues()
        {
            // var localEnvironment = _environment as LocalEnvironment;

            // localEnvironment.BookStoreContext.Persons.AddRange(
            //     new Person { FirstName = "Sergio", LastName = "Ontiveros" }
            // );

            // localEnvironment.BookStoreContext.SaveChanges("", "");
        }

        public async Task<(T ResponseObject, HttpStatusCode StatusCode)> GetInApi<T>(string url)
        {
            var response = await Client.GetAsync(url);
            var responseContent = await response.Content.ReadAsStringAsync();
            var dto = JToken.Parse(responseContent).ToObject<T>();

            return (dto, response.StatusCode);
        }

        public Person SeedDbOnePerson(string firstName, string lastName, string? address, string? gender)
        {
            if(_environment is LocalEnvironment)
            {
                var localEnvironment = _environment as LocalEnvironment;
                return EntityCreate.SeedDbOnePerson(localEnvironment.BookStoreContext, firstName, lastName, address, gender);
            }
            return null;
        }
    }
}