using System.Linq;
using Raven.Client.Indexes;
using Shared.Entities;

namespace ConsoleApplication1.Indexes
{
    public class Customers_ByFirstName : AbstractIndexCreationTask<Customer>
    {
        public Customers_ByFirstName()
        {
            Map = customers => from customer in customers
                              select new {customer.FirstName};
        }
    }
}