using BookStore.Models.Dtos;
using BookStore.Models.Entities;

namespace BookStore.Service.UserService
{
    public interface IUserService
    {
        Task<User> ValidateCredentials(UserDto userDto);
        Task<User> ValidateCredentials(string username);
        Task<bool> RevokeToken(string username);
        Task<User> RefreshUserInfo(User user);
    }
}