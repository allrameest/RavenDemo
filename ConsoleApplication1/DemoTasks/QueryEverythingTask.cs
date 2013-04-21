using System;
using System.Collections.Generic;
using System.Linq;
using Raven.Client;
using Raven.Client.Linq;
using Shared.Entities;

namespace ConsoleApplication1.DemoTasks
{
    internal class QueryEverythingTask : IDemoTask
    {
        public void Execute()
        {
            using (var session = Program.Store.OpenSession())
            {
                var customers = session.Query<Customer>()
                    .Where(c => c.Addresses.Any(a => a.City == "Helsingborg"))
                    .Lazily();

                var products = session.Query<Product>()
                    .Where(p => p.Title.StartsWith("Dr"))
                    .Lazily();

                Lazy<IEnumerable<Order>> orders = session.Query<Order>()
                    .Lazily();

                foreach (var customer in customers.Value)
                {
                    Console.WriteLine(customer.FirstName);
                }

                foreach (var product in products.Value)
                {
                    Console.WriteLine(product.Title);
                }

                foreach (var order in orders.Value)
                {
                    Console.WriteLine(order.Created);
                }
            }
        }
    }
}