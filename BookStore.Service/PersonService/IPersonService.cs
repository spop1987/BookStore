using BookStore.Models.Entities;
using BookStore.Models.Dtos;
using BookStore.Service.Generic;

namespace BookStore.Service.PersonService
{
    public interface IPersonService : IGenericRepository<PersonDto>
    {
         Task<Person> Create(PersonDto person);
         Task<Person> Update(PersonDto person);
    }
}