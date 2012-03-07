using System.Linq;
using Raven.Client.Indexes;
using Shared.Entities;

namespace ConsoleApplication1.Indexes
{
	public class BlogPosts_ByAuthor : AbstractIndexCreationTask<BlogPost, BlogPosts_ByAuthor.ReduceResult>
	{
		public class ReduceResult
		{
			public string Author { get; set; }
			public int Count { get; set; }
		}

		public BlogPosts_ByAuthor()
		{
			Map = posts => from post in posts
			               select new
			                      	{
			                      		post.Author,
			                      		Count = 1
			                      	};

			Reduce = results => from result in results
			                    group result by result.Author
			                    into g
			                    select new
			                           	{
			                           		Author = g.Key,
			                           		Count = g.Sum(x => x.Count)
			                           	};
		}
	}
}