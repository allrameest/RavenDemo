using System;
using ConsoleApplication1.DemoTasks;
using ConsoleApplication1.Migration;
using Raven.Client;
using Raven.Client.Document;
using Raven.Client.Indexes;

namespace ConsoleApplication1
{
	internal class Program
	{
		public static IDocumentStore Store { get; private set; }

		private static IDocumentStore InitializeStore()
		{
			var store = new DocumentStore {Url = "http://localhost:8081/"}
			//	.RegisterListener(new CustomerVersion1ToVersion2Converter())
				.Initialize();
			//store.AggressivelyCacheFor(TimeSpan.FromSeconds(1));
			IndexCreation.CreateIndexes(typeof (Program).Assembly, store);
			return store;
		}

		private static void Main()
		{
			AppDomain.CurrentDomain.UnhandledException += UnhandledExceptionHandler;

			Store = InitializeStore();

			var tasks = new IDemoTask[]
							{
								new CleanupTask(),
								new CreateCustomerTask(),
								new QueryOnStaticIndexTask(),
								//new QueryOnDynamicIndexTask(),
								//new CreateCustomerAndOrderTask(),
								//new QueryUsingIncludeTask(),
								//new SessionCachingStuffTask(),
								//new DoStuffWithProductsTask(),
								//new QueryEverythingTask(),
								//new StaleResultTask(),
								//new PagedQueryTask(),
								//new QueryForMigratedCustomerTask(),
								//new MapReduceQueryTask(),
							};

			foreach (var task in tasks)
			{
				Console.WriteLine("* " + task.GetType().Name);
				try
				{
					task.Execute();
				}
				catch (Exception ex)
				{
					Console.WriteLine(ex);
				}
				Console.WriteLine();
			}

			Console.ReadKey();
		}

		private static void UnhandledExceptionHandler(object sender, UnhandledExceptionEventArgs e)
		{
			Console.ForegroundColor = ConsoleColor.Red;
			Console.WriteLine("An unhandled exception occurred!");
			Console.WriteLine();

			var exception = e.ExceptionObject as Exception;
			if (exception == null)
			{
				Console.WriteLine("No details can be provided.");
			}
			else
			{
				Console.WriteLine(exception);
			}
			Console.ReadKey();
			Environment.Exit(1);
		}
	}
}