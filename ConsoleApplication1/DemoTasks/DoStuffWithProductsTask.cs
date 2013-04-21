using System;
using System.Collections.Generic;
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
                var strangelove = new Product {Title = "Dr Strangelove"};
                strangelove.Fields["Length"] = 90;
                strangelove.Fields["Actors"] = new[]
                    {
                        new Actor {Name = "Peter Sellers", Url = "http://www.imdb.com/name/nm0000634/"},
                        new Actor {Name = "James Earl Jones"}
                    };
                session.Store(strangelove);

                var terminator = new Product {Title = "Terminator"};
                terminator.Fields["Length"] = 110;
                terminator.Fields["Actors"] = new[]
                    {
                        new Actor {Name = "Bob"},
                        new Actor {Name = "Arnold"}
                    };
                session.Store(terminator);

                var clerks = new Product {Title = "Clerks"};
                clerks.Fields["Length"] = 90;
                clerks.Fields["Actors"] = new[]
                    {
                        new Actor {Name = "Kevin Smith"},
                        new Actor {Name = "Jason Mewes"}
                    };
                session.Store(clerks);

                session.SaveChanges();

                var movies = session.Query<Product>()
                                    .Where(p => p.Fields["Length"] == (object) 90)
                                    .ToArray();

                foreach (var product in movies)
                {
                    var actors = (IEnumerable<Actor>) product.Fields["Actors"];
                    Console.WriteLine("{0}\r\n    Actors: {1}", product.Title, string.Join(", ", actors.Select(x => x.Name)));
                }
            }
        }
    }
}