using System;
using Xunit;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using BookStore.Models.Dtos;

namespace BookStore.Api.Integration.Tests
{
    public class CommonFixture
    {
        public BookStoreApiFixture BookStoreApiFixture { get; set; }
        public CommonFixture()
        {
            BookStoreApiFixture = new BookStoreApiFixture();
        }
    }

    public class BookStoreIntegrationTests : IClassFixture<CommonFixture>
    {
        private readonly BookStoreApiFixture _apiFixture;
        public BookStoreIntegrationTests(CommonFixture commonFixture)
        {
            _apiFixture = commonFixture.BookStoreApiFixture;
        }

        [Fact(DisplayName = "GetPerson - Success")]
        public async Task GetPerson_Found()
        {
            //Arrange
            var person = _apiFixture.SeedDbOnePerson(firstName: "TestSergio", lastName: "TestOntiveros", null, null);
            
            //Act
            var (responseObject, statusCode) = await _apiFixture.GetInApi<PersonDto>($"/api/Person/{person.PersonId}");

            //Assert
            Assert.Equal(200, (int)statusCode);
            var personDto = Assert.IsType<PersonDto>(responseObject);
            DtoAsserts.AssertPersonDto(person, personDto);
        }
    }

}