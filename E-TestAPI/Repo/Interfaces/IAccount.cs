using E_TestAPI.DTO;
using E_TestAPI.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace E_TestAPI.Repo.Interfaces
{
    public interface IAccount
    {
        #region Add oprtations 
        public Task<IdentityResult> AddUser(RegisterUserDTO model, IConfiguration _config);
        public Task<IdentityResult> AddRole(string roleName);
        public Task<IdentityResult> AddUserRole(string userId, string roleName);
        #endregion

        #region Read oprtations 
        public Task<string> AuthUser(string username,string password, IConfiguration config);
        public Task<List<ApplicationRole>> GetAllRoles();
        #endregion

    }
}
