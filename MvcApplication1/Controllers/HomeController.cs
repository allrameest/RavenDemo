using System;
using System.Linq;
using System.Web.Mvc;
using Raven.Client;
using Raven.Client.Linq;
using Shared.Entities;

namespace MvcApplication1.Controllers
{
    public class HomeController : Controller
    {
        private readonly Func<IDocumentSession> _currentSessionFactory;

        public HomeController(Func<IDocumentSession> currentSessionFactory)
        {
            _currentSessionFactory = currentSessionFactory;
        }

        public ActionResult Index()
        {
            var customers = _currentSessionFactory()
                .Query<Customer>()
                .Where(c => c.FirstName.StartsWith("e"))
                .ToArray();

            return View(customers);
        }

        public ActionResult About()
        {
            return View();
        }
    }
}