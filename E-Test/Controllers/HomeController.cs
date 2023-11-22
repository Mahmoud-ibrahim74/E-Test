using E_Test.Models;
using E_Test.Security;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace E_Test.Controllers
{
    public class HomeController : Controller
    {
        private readonly CookieService _cookieService;

        public HomeController(CookieService cookieService)
        {
            this._cookieService = cookieService;
        }

        public IActionResult Index()
        {
            var getCookieAthun = _cookieService.FindCookies("t_user");
            if(getCookieAthun == null)
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                _cookieService.AddCookieTokenExpired("t_user", getCookieAthun);
            }
            var tokenValues = _cookieService.TokenValues(getCookieAthun);
            return View(tokenValues);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}