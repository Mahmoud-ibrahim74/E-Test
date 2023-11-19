using E_TestAPI.Context;
using E_TestAPI.DTO;
using E_TestAPI.Identity;
using E_TestAPI.JWT;
using E_TestAPI.Repo.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;

namespace HotelAPI.Repositories
{
    public class AccountRepo : IAccount
    {
        private readonly AppDbContext _dbContext;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        public AccountRepo(AppDbContext dbContext, UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
        {
            this._dbContext = dbContext;
            this._userManager = userManager;
            this._roleManager = roleManager;
        }



        #region Add oprtations 
        public async Task<IdentityResult> AddRole(string roleName)
        {
            var role = new ApplicationRole
            {
                Name = roleName,
                ConcurrencyStamp = Guid.NewGuid().ToString()
            };
            var result = await _roleManager.CreateAsync(role);
            return result;
        }

        public async Task<IdentityResult> AddUser(RegisterUserDTO model, IConfiguration _config)
        {
            var user = new ApplicationUser
            {
                UserName = model.Username,
                Email = model.Email,
                Address = model.Address,
                PhoneNumber = model.phoneNumber,
                ImageUrl = model.imgUrl,
                StudentClassId = model.StudentclassId
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            await AddUserRole(user.Id, model.roleType);
            await CreateUserToken(user.Id, model.roleType, _config);
            return result;
        }

        public async Task<IdentityResult> AddUserRole(string userId, string roleName)
        {
            var roleNameResult = await _roleManager.FindByNameAsync(roleName);
            var user = await _userManager.FindByIdAsync(userId);
            var result = await _userManager.AddToRoleAsync(user, roleNameResult.Name);
            return result;
        }

        #endregion

        #region Read oprtations 

        #endregion

        #region Authentication Area
        public async Task<string> AuthorizeUser(string username, string password,string roleType, IConfiguration config)
        {
            var getUser = await _userManager.FindByNameAsync(username);
            if (getUser != null)
            {
                var passwordExist = await _userManager.CheckPasswordAsync(getUser, password); // check username and password
                if (passwordExist)
                {
                    var userToken = await _userManager.GetAuthenticationTokenAsync(getUser, "E-Test", "AuthenticationUser"); // get user token in db
                    if (userToken != null)
                    {
                        var decodeToken = new JwtSecurityTokenHandler().ReadToken(userToken); // decode user token
                        if (decodeToken.ValidTo <= DateTime.Now) // check token expiration
                        {
                            if (await DeleteUserToken(getUser)) // delete expired token
                            {
                                return await CreateUserToken(getUser.Id, roleType, config); /*then create new UserToken*/
                            }
                        }
                        else // token valid ,then return current
                        {
                            return userToken;
                        }

                    }
                }
            }
            return null;
        }
        private async Task<string> CreateUserToken(string userId, string roleName, IConfiguration _config)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                var generatedToken = new TokenActions(user, roleName, _config).GenerateToken();
                var token = new JwtSecurityTokenHandler().WriteToken(generatedToken);
                var result = await _userManager.SetAuthenticationTokenAsync(user, "E-Test", "AuthenticationUser", token); // save token to AspNetUserTokens table
                return token;
            }
            return null;
        }
        private async Task<bool> DeleteUserToken(ApplicationUser user)
        {
            var userToken = await _userManager.RemoveAuthenticationTokenAsync(user, "E-Test", "AuthenticationUser");
            if (userToken.Succeeded)
            {
                return true;
            }
            return false;
        }
        #endregion



    }
}
