using System.Linq;
using System.Web.Mvc;
using Raven.Client;
using Raven.Client.Linq;
using Shared.Entities;

namespace MvcApplication1.Controllers
{
	public class HomeController : Controller
	{
		private readonly IDocumentSession _session;

		public HomeController(IDocumentSession session)
		{
			_session = session;
		}

		public ActionResult Index(string query = "")
		{
			var customers = _session
				.Query<Customer>()
				.Where(c => c.FirstName.StartsWith(query))
				.ToArray();

			return View(customers);
		}

		public ActionResult About()
		{
			return View();
		}
	}
}