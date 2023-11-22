using E_Test.HelperAPI;
using E_Test.Security;
using E_Test.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace E_Test.Controllers
{
    public class AccountController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly CookieService _cookieService;

        public AccountController(IHttpClientFactory httpClientFactory, CookieService cookieService)
        {
            this._httpClientFactory = httpClientFactory;
            this._cookieService = cookieService;
        }
        [HttpGet]
        public async Task<IActionResult> Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginVM model)
        {
            var tokenResult = await new AccountAPI(_httpClientFactory).LoginAuth(model, "Login");
            if (tokenResult == null)
            {
                TempData["loginAuth"] = "username or password incorrect";
                return View();
            }
            else
            {
                _cookieService.AddCookieToken("t_user", tokenResult);
                return RedirectToAction("Index", "Home");
            }
        }
        public async Task<IActionResult> Logout()
        {
            _cookieService.DeleteCookie("t_user");
            return RedirectToAction("Login", "Account");
        }


	}
}
