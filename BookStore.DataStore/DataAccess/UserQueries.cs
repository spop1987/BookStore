using BookStore.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookStore.DataStore.DataAccess
{
    public class UserQueries : IUserQueries
    {
        private readonly BookStoreContext _bookStoreContext;
        public UserQueries(BookStoreContext bookStoreContext)
        {
            _bookStoreContext = bookStoreContext;
        }
        public async Task<User> GetUser(string userName, string password)
        {
            return await _bookStoreContext.Users.FirstOrDefaultAsync(u => (u.UserName == userName) && (u.Password == password));
        }

        public async Task<User> GetUserByUsername(string username)
        {
            return await _bookStoreContext.Users.FirstOrDefaultAsync(u => u.UserName == username);
        }

        public async Task<User> GetUserById(long userId)
        {
            return await _bookStoreContext.Users.FirstOrDefaultAsync(u => u.UserId == userId);
        }

        public async Task<User> SaveUserChanges(User userInDb)
        {
            _bookStoreContext.Entry(userInDb).CurrentValues.SetValues(userInDb);
            await _bookStoreContext.SaveChangesAsync();
            return userInDb;
        }
    }
}