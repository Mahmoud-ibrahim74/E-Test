using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using E_TestAPI.Repo.Interfaces;
using E_TestAPI.DTO;

namespace E_TestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class AccountController : ControllerBase
    {
        private readonly IAccount _account;
        private readonly IConfiguration _configuration;
        public AccountController(IAccount account, IConfiguration configuration)
        {
            this._account = account;
            _configuration = configuration;
        }
        #region Add End-Points
        [HttpPost("AddNewRole")]
        public async Task<IActionResult> AddNewRole(string roleName)
        {
            var result = await _account.AddRole(roleName);
            if (result.Succeeded)
                return Ok(result);
            else
                return BadRequest(result.Errors.FirstOrDefault());
        }

        //[HttpPost("AddNewUserRole")]
        //public async Task<IActionResult> AddNewUserRole(string userId, string roleName)
        //{
        //    var result = await _account.AddUserRole(userId, roleName);
        //    if (result.Succeeded)
        //        return Ok(result);
        //    else
        //        return BadRequest(result.Errors.FirstOrDefault());
        //} 
        [HttpPost("Login")]
        public async Task<IActionResult> Login(string username, string password,string roleType)
        {
            var authResult = await _account.AuthorizeUser(username, password, roleType, _configuration);
            if(authResult !=  null)
                return Ok(authResult);
            else
                return Unauthorized("username or password incorrect");
        }
        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserDTO model)
        {
            var result = await _account.AddUser(model, _configuration);
            if (result.Succeeded)
                return Ok(result);
            else
                return BadRequest(result.Errors.FirstOrDefault());
        }
        #endregion

    }
}
