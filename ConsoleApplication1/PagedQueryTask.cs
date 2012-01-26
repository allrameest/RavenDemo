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
                    session.Store(new BlogPost {Title = i.ToString(CultureInfo.InvariantCulture)});
                }
                session.SaveChanges();
            }

            using (var session = Program.Store.OpenSession())
            {
                RavenQueryStatistics stats;
                var blogPosts = session.Query<BlogPost>()
                    .Customize(c => c.WaitForNonStaleResults())
                    .Statistics(out stats)
                    .Skip(10)
                    .Take(10)
                    .ToArray();
                
                Console.WriteLine("Total: " + stats.TotalResults);
                foreach (var blogPost in blogPosts)
                {
                    Console.WriteLine(blogPost.Title);
                }
            }
        }
    }
}