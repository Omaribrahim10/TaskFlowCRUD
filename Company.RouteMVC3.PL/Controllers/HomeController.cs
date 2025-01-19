using System.Diagnostics;
using System.Text;
using Company.RouteMVC3.PL.ViewModels;
using Company.RouteMVC3.PL.Services;
using Microsoft.AspNetCore.Mvc;

namespace Company.RouteMVC3.PL.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private readonly IScopedService _scoped01;
		private readonly IScopedService _scoped02;
		private readonly ITransientService _transient01;
		private readonly ITransientService _transient02;
		private readonly ISingltonService _singlton01;
		private readonly ISingltonService _singlton02;

		public HomeController(
			ILogger<HomeController> logger,
			IScopedService scoped01,
			IScopedService scoped02,
			ITransientService transient01,
			ITransientService transient02,
			ISingltonService singlton01,
			ISingltonService singlton02
			)
		{
			_logger = logger;
			_scoped01 = scoped01;
			_scoped02 = scoped02;
			_transient01 = transient01;
			_transient02 = transient02;
			_singlton01 = singlton01;
			_singlton02 = singlton02;
		}

		public string TestLifeTime()
		{
			StringBuilder builder = new StringBuilder();
			builder.Append($"scoped01 :: {_scoped01.GetGuid()} \n");
			builder.Append($"scoped02 :: {_scoped02.GetGuid()} \n\n");

			builder.Append($"transient01 :: {_transient01.GetGuid()} \n");
			builder.Append($"transient02 :: {_transient02.GetGuid()} \n\n");

			builder.Append($"singlton01 :: {_singlton01.GetGuid()} \n");
			builder.Append($"singlton02 :: {_singlton02.GetGuid()} \n\n");

			return builder.ToString();
		}

		public IActionResult Index()
		{
			return View();
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
