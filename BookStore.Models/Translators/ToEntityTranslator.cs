using BookStore.Models.Dtos;
using BookStore.Models.Entities;

namespace BookStore.Models.Translators
{
    public interface IToEntityTranslator
    {
        User ToUser(UserDto userDto, User user = null); 
    }
    public class ToEntityTranslator : IToEntityTranslator
    {
        public User ToUser(UserDto userDto, User user = null)
        {
            // if(user == null){
            //     return new User{
            //         UserName = userDto.UserName,
            //         FullName = userDto.FullName,
            //         Password = userDto.Password,
            //         RefreshToken = userDto.RefreshToken,
            //         RefreshTokenExpiryTime = userDto.RefreshTokenExpiryTime
            //     };
            // }

            // user.FullName = userDto.FullName;
            // user.Password = userDto.Password;
            // user.UserName = userDto.UserName;
            // user.RefreshToken = userDto.RefreshToken;
            // user.RefreshTokenExpiryTime = userDto.RefreshTokenExpiryTime;

            // return user;
            throw new NotImplementedException();
        }
    }
}