using System;
using System.Linq;
using System.Threading.Tasks;
using Raven.Client.Linq;
using Shared.Entities;

namespace ConsoleApplication1
{
    internal class StaleResultTask : IDemoTask
    {
        public void Execute()
        {
            Parallel.For(0, 5, i =>
                                   {
                                       using (var session = Program.Store.OpenSession())
                                       {
                                           for (int j = 0; j < 10; j++)
                                           {
                                               session.Store(new BlogPost {Title = string.Format("{0} {1}", i, j)});
                                           }
                                           session.SaveChanges();
                                       }
                                   });

            using (var session = Program.Store.OpenSession())
            {
                RavenQueryStatistics stats;
                var blogPosts = session.Query<BlogPost>()
                    .Statistics(out stats)
                    .Customize(c => c.WaitForNonStaleResultsAsOfNow());

                Console.WriteLine(blogPosts.Count());
            }
        }
    }
}