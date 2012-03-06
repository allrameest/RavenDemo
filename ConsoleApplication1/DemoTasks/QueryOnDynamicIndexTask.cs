using System;
using System.Linq;
using Raven.Client.Linq;
using Shared.Entities;

namespace ConsoleApplication1.DemoTasks
{
    internal class QueryOnDynamicIndexTask : IDemoTask
    {
        public void Execute()
        {
            using (var session = Program.Store.OpenSession())
            {
                var customers = session.Query<Customer>()
                    .Where(c => c.Addresses.Any(a => a.City.StartsWith("Helsing")))
                    .ToArray();

                Console.WriteLine(customers.Count());
                foreach (var customer in customers)
                {
                    Console.WriteLine(customer.FirstName);
                }
            }
        }
    }
}