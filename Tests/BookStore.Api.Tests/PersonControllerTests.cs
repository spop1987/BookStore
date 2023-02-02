using System.Threading.Tasks;
using BookStore.Api.Controllers;
using BookStore.Models.Dtos;
using BookStore.Models.Exceptions;
using BookStore.Service.PersonService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace BookStore.Api.Tests;

public class PersonControllerTests
{
    private readonly Mock<IPersonService> _personService = new Mock<IPersonService>();
    private readonly PersonController _controller; 
    private const string USERNAME = "someUser";
    private const string SOURCE = "someSource";

    public PersonControllerTests()
    {
        _controller = new PersonController(_personService.Object);
    }

    #region GetPerson
    [Fact(DisplayName = "GetPerson [Success]")]
    public async Task GetPerson_Found()
    {
        // Arrange
        long personId = 123456;
        var expectedPersonDto = new PersonDto();
        _personService.Setup(p => p.GetById(personId)).ReturnsAsync(expectedPersonDto);

        // Act
        var response = await _controller.GetPersonById(personId.ToString());

        // Assert
        var statusResult = Assert.IsType<OkObjectResult>(response);
        Assert.Equal(StatusCodes.Status200OK, statusResult.StatusCode);
        var personDto = Assert.IsType<PersonDto>(statusResult.Value);
        Assert.Equal(expectedPersonDto.PersonId, personDto.PersonId);
    }

    [Fact(DisplayName = "GetPerson [Failure]")]
    public async Task GetPerson_NotFound()
    {
        // Arrange
        long personId = 123454553425;
        var exception = new BookStoreException(new Error {Number = (int)ErrorNumber.GetPersonAsync, Message = "Person Not Found in Database"});
        _personService.Setup(p => p.GetById(personId)).Throws(exception);
        
        // Assert
        var result = Assert.ThrowsAsync<BookStoreException>(() => _controller.GetPersonById(personId.ToString()));
        Assert.Equal(1404, result.Result.Error.Number);
        Assert.StartsWith("Person Not Found in Database", result.Result.Error.Message);
    }

    #endregion
}