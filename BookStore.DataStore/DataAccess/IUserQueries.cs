using BookStore.Models.Entities;

namespace BookStore.DataStore.DataAccess
{
    public interface IUserQueries
    {
        Task<User> GetUser(string userName, string password);
        Task<User> GetUserById(long userId);
        Task<User> GetUserByUsername(string username);
        Task<User> SaveUserChanges(User userInDb);
    }
}