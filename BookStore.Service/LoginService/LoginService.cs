using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using BookStore.Models.Dtos;
using BookStore.Service.Configuration;
using BookStore.Service.TokenService;
using BookStore.Service.UserService;

namespace BookStore.Service.LoginService
{
    public class LoginService : ILoginService
    {
        private const string DATE_FORMAT = "yyyy-MM-dd HH:mm:ss";
        private TokenConfiguration _tokenConfiguration;
        private IUserService _userService;
        private readonly ITokenService _tokenService;
        public LoginService(TokenConfiguration tokenConfiguration,
                            IUserService userService,
                            ITokenService tokenService)
        {
            _tokenConfiguration = tokenConfiguration;
            _userService = userService;
            _tokenService = tokenService;
        }
        public async Task<bool> RevokeToken(string userName)
        {
            return await _userService.RevokeToken(userName);
        }

        public async Task<TokenDto> ValidateCredentials(UserDto userDto)
        {
            var user = await _userService.ValidateCredentials(userDto);
            if(user == null) return await Task.FromResult<TokenDto>(null);

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
                new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName)
            };

            var accessToken = _tokenService.GenerateAccessToken(claims);
            var refreshToken = _tokenService.GenerateRefreshToken();

            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.Now.AddDays(_tokenConfiguration.DaysToExpiry);

            await _userService.RefreshUserInfo(user);
            DateTime createDate = DateTime.Now;
            DateTime expirationDate = createDate.AddMinutes(_tokenConfiguration.Minutes);
            return new TokenDto(
                true,
                createDate.ToString(DATE_FORMAT),
                expirationDate.ToString(DATE_FORMAT),
                accessToken,
                refreshToken);
        }

        public async Task<TokenDto> ValidateCredentials(TokenDto tokenDto)
        {
            var accessToken = tokenDto.AccessToken;
            var refreshToken = tokenDto.RefreshToken;

            var principal = _tokenService.GetPrincipalFromExpiredToken(accessToken);
            var userName = principal?.Identity?.Name;

            var user = await _userService.ValidateCredentials(userName);
            if(user == null || user.RefreshToken != refreshToken || user.RefreshTokenExpiryTime <= DateTime.Now) return null;

            accessToken = _tokenService.GenerateAccessToken(principal.Claims);
            refreshToken = _tokenService.GenerateRefreshToken();

            user.RefreshToken = refreshToken;
            await _userService.RefreshUserInfo(user);

            DateTime createDate = DateTime.Now;
            DateTime expirationDate = createDate.AddMinutes(_tokenConfiguration.Minutes);

            return new TokenDto(
                true,
                createDate.ToString(DATE_FORMAT),
                expirationDate.ToString(DATE_FORMAT),
                accessToken,
                refreshToken
                );
        }
    }
}