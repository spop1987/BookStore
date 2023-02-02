using BookStore.Models.Entities;
using BookStore.Models.Dtos;
using BookStore.Models.Translators;
using Microsoft.Extensions.Logging;
using BookStore.DataStore.DataAccess;
using BookStore.Models.Exceptions;

namespace BookStore.Service.PersonService
{
    public class PersonService : IPersonService
    {
        private readonly IPersonQueries _queries;
        private readonly IToDtoTranslator _toDtoTranslator;
        private readonly ILogger<PersonService> _logger;
        public PersonService(IPersonQueries queires,
                             IToDtoTranslator toDtoTranslator,
                             ILogger<PersonService> logger)
        {
            _queries = queires;
            _toDtoTranslator = toDtoTranslator;
            _logger = logger;
        }
        public Task<Person> Create(PersonDto person)
        {
            throw new NotImplementedException();
        }

        public async Task<PersonDto> GetById(long personId)
        {
            var person = await _queries.GetPersonAsync(personId);
            if(person == null)
             throw new BookStoreException(new Error {Number = (int)ErrorNumber.GetPersonAsync, Message = "Person Not Found in Database"});
            
            return _toDtoTranslator.ToPersonDto(person);
        }
        public  Task<IEnumerable<PersonDto>> GetAll()
        {
            throw new NotImplementedException();
        }
        public Task<Person> Update(PersonDto person)
        {
            throw new NotImplementedException();
        }
        public Task Delete(long id)
        {
            throw new NotImplementedException();
        }

    }
}