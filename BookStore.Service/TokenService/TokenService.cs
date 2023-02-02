using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using BookStore.Service.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace BookStore.Service.TokenService
{
    public class TokenService : ITokenService
    {
        private TokenConfiguration _tokenConfiguration;
        public TokenService(TokenConfiguration tokenConfiguration)
        {
            _tokenConfiguration = tokenConfiguration;
        }
        public string GenerateAccessToken(IEnumerable<Claim> calims)
        {
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenConfiguration.Secret));
            var signInCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            var options = new JwtSecurityToken(issuer: _tokenConfiguration.Issuer,
                                               audience: _tokenConfiguration.Audience,
                                               claims: calims,
                                               expires: DateTime.Now.AddMinutes(_tokenConfiguration.Minutes),
                                               signingCredentials: signInCredentials);

            string tokenString = new JwtSecurityTokenHandler().WriteToken(options);
            return tokenString;
        }

        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using(var rng = RandomNumberGenerator.Create()){
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }

        public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var tokenValidationParameters = new TokenValidationParameters{
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenConfiguration.Secret)),
                ValidateLifetime = false
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken;

            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);
            var jwtSecurityToken = securityToken as JwtSecurityToken;
            if(jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCulture))
                throw new SecurityTokenException("Invalid Token");
            
            return principal;
        }
    }
}