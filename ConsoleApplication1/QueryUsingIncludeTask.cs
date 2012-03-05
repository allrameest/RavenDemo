using System;
using System.Linq;
using Raven.Client.Linq;
using Shared.Entities;

namespace ConsoleApplication1
{
	internal class QueryUsingIncludeTask : IDemoTask
	{
		public void Execute()
		{
			using (var session = Program.Store.OpenSession())
			{
				var orders = session.Query<Order>()
					.Include(x => x.CustomerId)
					.ToArray();

				Console.WriteLine(orders.Count());
				foreach (var order in orders)
				{
					var customer = session.Load<Customer>(order.CustomerId);
					Console.WriteLine(customer.FirstName);
				}
			}
		}
	}
}