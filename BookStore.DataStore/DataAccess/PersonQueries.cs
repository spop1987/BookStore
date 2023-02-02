using BookStore.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookStore.DataStore.DataAccess
{
    public class PersonQueries : IPersonQueries
    {
        private readonly BookStoreContext _bookStoreContext;
        public PersonQueries(BookStoreContext bookStoreContext)
        {
            _bookStoreContext = bookStoreContext;
        }
        public async Task<Person> GetPersonAsync(long personId)
        {   
            return await PersonWithIncludes().FirstOrDefaultAsync(p => p.PersonId == personId);
        }

        private IQueryable<Person> PersonWithIncludes()
        {
            return _bookStoreContext.Persons.AsQueryable();
        }
    }
}