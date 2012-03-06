using System;
using System.Linq;
using Shared.Entities;

namespace ConsoleApplication1.DemoTasks
{
	internal class QueryForMigratedCustomerTask : IDemoTask
	{
		public void Execute()
		{
			using (var session = Program.Store.OpenSession())
			{
				var customers = session.Query<Customer>().ToArray();
				foreach (var customer in customers)
				{
					Console.WriteLine(customer.FirstName);
				}
			}
		}
	}
}