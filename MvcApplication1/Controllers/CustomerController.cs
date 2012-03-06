using System.Linq;
using System.Web.Mvc;
using Raven.Client;
using Raven.Client.Linq;
using Shared.Entities;

namespace MvcApplication1.Controllers
{
    public class CustomerController : Controller
    {
        private readonly IDocumentSession _session;

        public CustomerController(IDocumentSession session)
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

        public ActionResult Create()
        {
            return View(new Customer());
        }

        [HttpPost]
        public ActionResult Create(Customer customer)
        {
            if (!ModelState.IsValid)
            {
                return View(customer);
            }

            _session.Store(customer);
            _session.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}