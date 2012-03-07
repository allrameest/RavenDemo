using System;
using System.Linq;
using ConsoleApplication1.Indexes;
using Raven.Client.Linq;
using Shared.Entities;

namespace ConsoleApplication1.DemoTasks
{
    internal class QueryOnStaticIndexTask : IDemoTask
    {
        public void Execute()
        {
            using (var session = Program.Store.OpenSession())
            {
                var customers = session.Query<Customer, Customers_ByFirstName>()
                    .Where(c => c.FirstName == "Erik")
                    .ToArray();

                foreach (var customer in customers)
                {
                    Console.WriteLine(customer.FirstName);
                }
            }
        }
    }
}