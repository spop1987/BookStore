using BookStore.Models.Dtos;
using BookStore.Models.Entities;
using Xunit;

namespace BookStore.Api.Integration.Tests
{
    public static class DtoAsserts
    {
        public static void AssertPersonDto(Person person, PersonDto personDto)
        {
            Assert.IsType<PersonDto>(personDto);
            Assert.Equal(person.FirstName, personDto.FirstName);
            Assert.Equal(person.LastName, personDto.LastName);
        }
    }
}