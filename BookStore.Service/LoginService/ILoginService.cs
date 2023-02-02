using BookStore.Models.Dtos;

namespace BookStore.Service.LoginService
{
    public interface ILoginService
    {
        Task<TokenDto> ValidateCredentials(UserDto userDto);
        Task<TokenDto> ValidateCredentials(TokenDto tokenDto);
        Task<bool> RevokeToken(string userName);
    }
}