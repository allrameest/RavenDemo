using System;
using System.Diagnostics;
using Shared.Entities;

namespace ConsoleApplication1
{
    internal class SessionCachingStuffTask : IDemoTask
    {
        public void Execute()
        {
            int id;
            using (var session = Program.Store.OpenSession())
            {
                var product = new Product {Title = "Foo"};
                session.Store(product);
                id = product.Id;
                session.SaveChanges();
            }

            var stopwatch = Stopwatch.StartNew();
            //using (var session = Program.Store.OpenSession())
            //{
            //    for (int i = 0; i < 1000; i++)
            //    {
            //        session.Load<Product>(id);
            //    }
            //}

            for (int i = 0; i < 1000; i++)
            {
                using (var session = Program.Store.OpenSession())
                {
                    session.Load<Product>(id);
                }
            }

            stopwatch.Stop();
            Console.WriteLine("{0} ms", stopwatch.Elapsed.TotalMilliseconds);
        }
    }
}