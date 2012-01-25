using System;
using System.Globalization;
using System.Linq;
using Raven.Client.Linq;
using Shared.Entities;

namespace ConsoleApplication1
{
    internal class PagedQueryTask : IDemoTask
    {
        public void Execute()
        {
            using (var session = Program.Store.OpenSession())
            {
                for (int i = 0; i < 100; i++)
                {
                    session.Store(new Product {Title = i.ToString(CultureInfo.InvariantCulture)});
                }
                session.SaveChanges();

                RavenQueryStatistics stats;
                var products = session.Query<Product>()
                    .Customize(c => c.WaitForNonStaleResultsAsOfNow())
                    .Statistics(out stats)
                    .Skip(10)
                    .Take(10)
                    .ToArray();

                Console.WriteLine("Total: " + stats.TotalResults);
                foreach (var product in products)
                {
                    Console.WriteLine(product.Title);
                }
            }
        }
    }
}