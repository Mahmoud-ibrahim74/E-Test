using E_TestAPI.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace E_TestAPI.JWT
{
    public class TokenActions
    {
        private ApplicationUser _user { get; set; }
        private string _userRole { get; set; }
        private IConfiguration _config { get; set; }


        public TokenActions(ApplicationUser _user, string _userRole, IConfiguration _config)
        {
            this._user = _user;
            this._userRole = _userRole;
            this._config = _config;
        }
        public JwtSecurityToken GenerateToken()
        {
            var CliamsTokenList = CreateCliams();
            //create SecurityKey
            SecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:SecretKey"]));  // create symantic key to pass in SigningCredentials
            var signInCredential = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);  // for signature in token
            JwtSecurityToken jwtSecurityToken = new(
            issuer: _config["JWT:Issuer"],   // url provider
            audience: _config["JWT:Audience"],  // url consumer
            claims: CliamsTokenList,
            expires: DateTime.Now.AddDays(30),
            signingCredentials: signInCredential
            );
            return jwtSecurityToken;
        }
        public List<Claim> CreateCliams()
        {
            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Name, _user.UserName));
            claims.Add(new Claim(ClaimTypes.NameIdentifier, _user.Id));
            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            claims.Add(new Claim(ClaimTypes.Role, _userRole));
            return claims;
        }
    }
}
