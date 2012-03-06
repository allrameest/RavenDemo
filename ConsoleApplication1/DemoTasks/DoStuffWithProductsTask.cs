using System;
using System.Linq;
using Raven.Client.Linq;
using Shared.Entities;

namespace ConsoleApplication1.DemoTasks
{
    internal class DoStuffWithProductsTask : IDemoTask
    {
        public void Execute()
        {
            using (var session = Program.Store.OpenSession())
            {
                var strangelove = new Product { Title = "Dr Strangelove" };
                strangelove.Fields.Add("Length", 120);
                strangelove.Fields.Add("Actors", new[] { new Actor { Name = "Peter Sellers", Url = "..." } });
                session.Store(strangelove);


                var terminator = new Product { Title = "Terminator" };
                terminator.Fields.Add("Length", 140);
                terminator.Fields.Add("Actors", new[] { new Actor { Name = "Bob", Url = "..." }, new Actor { Name = "Arnold", Url = "..." } });
                session.Store(terminator);

                session.SaveChanges();

                var movies = session.Query<Product>()
                    .Where(p => p.Fields["Length"] == (object) 120)
                    .ToArray();

                foreach (var product in movies)
                {
                    Console.WriteLine(product.Title);
                }
            }
        }
    }
}