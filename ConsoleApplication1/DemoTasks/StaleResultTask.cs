using System;
using System.Linq;
using System.Threading.Tasks;
using Shared.Entities;

namespace ConsoleApplication1.DemoTasks
{
	internal class StaleResultTask : IDemoTask
	{
		public void Execute()
		{
			Parallel.For(0, 5, i =>
			                   	{
			                   		using (var session = Program.Store.OpenSession())
			                   		{
			                   			for (int j = 0; j < 100; j++)
			                   			{
			                   				session.Store(new BlogPost {Title = string.Format("{0} {1}", i, j)});
			                   			}
			                   			session.SaveChanges();
			                   		}
			                   	});

			using (var session = Program.Store.OpenSession())
			{
				var blogPosts = session.Query<BlogPost>();
				//.Customize(c => c.WaitForNonStaleResultsAsOfNow());

				Console.WriteLine(blogPosts.Count());
			}
		}
	}
}