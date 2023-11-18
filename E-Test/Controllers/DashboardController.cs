using Microsoft.AspNetCore.Mvc;

namespace E_Test.Controllers
{
	public class DashboardController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
