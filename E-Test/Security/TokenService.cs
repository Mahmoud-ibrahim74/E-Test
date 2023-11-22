using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
namespace E_Test.Security
{
    public class TokenService
    {
        protected static bool IsTokenExpired(string token)
        {
            var decodeToken = new JwtSecurityTokenHandler().ReadToken(token); // decode user token
            if (decodeToken.ValidTo <= DateTime.Now) // check token expiration
            {
                return true;
            }
            return false;
        }
        protected bool IsValidToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                // Validate token without checking the signature
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = false,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                }, out _);

                // If validation succeeds, the token is considered valid
                return true;
            }
            catch (SecurityTokenException)
            {
                // If validation fails, the token is considered invalid
                return false;
            }
        }
        protected  TokenDecoder DecodeToken(string token)
        {
			var tokenHandler = new JwtSecurityTokenHandler();
			var decodeToken = (JwtSecurityToken)tokenHandler.ReadToken(token); // decode user token
            TokenDecoder decoder = new TokenDecoder()
            {
                Id = decodeToken.Id,
                Issuer = decodeToken.Issuer,
                ValidFrom = decodeToken.ValidFrom,
                ValidTo = decodeToken.ValidTo,
                RoleName = decodeToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value,
                username = decodeToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value

            };
            return decoder;
        }
    }
}