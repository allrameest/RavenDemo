using System;
using System.Linq;
using ConsoleApplication1.Indexes;
using Shared.Entities;

namespace ConsoleApplication1.DemoTasks
{
	internal class MapReduceQueryTask : IDemoTask
	{
		public void Execute()
		{
			using (var session = Program.Store.OpenSession())
			{
				session.Store(new BlogPost {Title = "TAKING CONTROL", Author = "Per Dervall"});
				session.Store(new BlogPost {Title = "LEETZEPPELIN", Author = "Fredrik Leijon"});
				session.Store(new BlogPost {Title = "WE LOVE NEW KNOWLEDGE!", Author = "Martin Thern"});
				session.Store(new BlogPost {Title = "HEY, THERE’S A NUGET FOR THAT!", Author = "Martin Thern"});
				session.SaveChanges();
			}

			using (var session = Program.Store.OpenSession())
			{
				var authors = session.Query<BlogPosts_ByAuthor.ReduceResult, BlogPosts_ByAuthor>()
					.Customize(c => c.WaitForNonStaleResults())
					.OrderByDescending(x => x.Count).ThenBy(x => x.Author);

				Console.WriteLine(authors.Count());

				foreach (var author in authors)
				{
					Console.WriteLine("{0} - {1}", author.Author, author.Count);
				}
			}
		}
	}
}