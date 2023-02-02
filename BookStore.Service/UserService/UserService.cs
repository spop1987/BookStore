using System.Text;
using BookStore.DataStore.DataAccess;
using BookStore.Models.Dtos;
using BookStore.Models.Entities;
using BookStore.Models.Translators;
using Microsoft.Extensions.Logging;
using System.Security.Cryptography;
using BookStore.DataStore;

namespace BookStore.Service.UserService
{
    public class UserService : IUserService
    {
        private readonly IUserQueries _queries;

        public UserService(IUserQueries queries)
        {
            _queries = queries;
        }
        public async Task<User> ValidateCredentials(UserDto userDto)
        {
            var password = ComputeHash(userDto.Password);
            return await _queries.GetUser(userDto.UserName, password);
        }

        public async Task<User> ValidateCredentials(string username)
        {
            return await _queries.GetUserByUsername(username);
        }

        public async Task<bool> RevokeToken(string username)
        {
            var user = await _queries.GetUserByUsername(username);
            if (user is null) return await Task.FromResult<bool>(false);
            user.RefreshToken = null;
            var userUpdate = await _queries.SaveUserChanges(user);
            if(userUpdate.RefreshToken is null)
                return await Task.FromResult<bool>(true);
            
            return await Task.FromResult<bool>(false);
        }

        public async Task<User> RefreshUserInfo(User user)
        {
            var userInDb = await _queries.GetUserById(user.UserId);
            if(userInDb is null)
                return await Task.FromResult<User>(null);

            return await _queries.SaveUserChanges(userInDb);
        }

        private string ComputeHash(string password)
        {
            Byte[] inputBytes = Encoding.UTF8.GetBytes(password);
            using(var algorithm = SHA512.Create())
            {
                string hex = "";
                var hashValue = algorithm.ComputeHash(inputBytes);
                foreach (byte item in hashValue)
                {
                    hex += String.Format("{0:x2}", item);
                }
                return hex;
            }
        }
    }
}