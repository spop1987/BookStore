using Microsoft.AspNetCore.Mvc;
using BookStore.Models.Entities;
using BookStore.Service.PersonService;
using BookStore.Api.ActionFilters.Validations;

namespace BookStore.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PersonController : ControllerBase
{
    private readonly IPersonService _personService;

    public PersonController(IPersonService personService)
    {
        _personService = personService;
    }

    [HttpGet("{personId}")]
    [ValidatePersonId]
    public async Task<IActionResult> GetPersonById(string personId)
    {
      var person = await _personService.GetById(long.Parse(personId));
      return Ok(person);
    }
}
