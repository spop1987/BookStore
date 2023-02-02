using BookStore.Models.Entities;

namespace BookStore.DataStore.DataAccess
{
    public interface IPersonQueries
    {
        Task<Person> GetPersonAsync(long personId);
    }
}