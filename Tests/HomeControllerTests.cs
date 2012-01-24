using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using MvcApplication1.Controllers;
using NUnit.Framework;
using Shared.Entities;

namespace Tests
{
    [TestFixture]
    public class HomeControllerTests : TestBase
    {
        [Test]
        public void Returns_customer_with_first_name_starting_with_e()
        {
            DocumentSession.Store(new Customer {FirstName = "Erik"});
            DocumentSession.Store(new Customer {FirstName = "Mattias"});
            DocumentSession.SaveChanges();

            var controller = new HomeController(() => DocumentSession);

            var r = (ViewResult) controller.Index();
            var customers = (IEnumerable<Customer>) r.Model;

            Assert.That(customers.Count(), Is.EqualTo(1));
            Assert.That(customers.Single().FirstName, Is.EqualTo("Erik"));
        }
    }
}