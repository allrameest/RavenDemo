using System;
using Raven.Client;
using Raven.Client.Document;
using Raven.Client.Indexes;

namespace ConsoleApplication1
{
	internal class Program
	{
		private static IDocumentStore _store;

		private static IDocumentStore InitializeStore()
		{
			var store = new DocumentStore {Url = "http://localhost:8081/"}.Initialize();
			IndexCreation.CreateIndexes(typeof (Program).Assembly, store);
			return store;
		}

		public static IDocumentStore Store
		{
			get { return _store; }
		}

		private static void Main()
		{
			AppDomain.CurrentDomain.UnhandledException += UnhandledExceptionHandler;

			_store = InitializeStore();

			var tasks = new IDemoTask[]
			            	{
								new CleanupTask(),
								//new CreateCustomerTask(),
			            		//new QueryOnStaticIndexTask(),
			            		//new QueryForCustomerTask(),
			            		new CreateCustomerAndOrderTask(),
								new QueryUsingIncludeTask(),
			            		//new SessionCachingStuffTask(),
			            		//new DoStuffWithProductsTask(),
			            		//new QueryEverythingTask(),
			            		//new StaleResultTask(),
			            		//new CleanupTask(),
			            		//new PagedQueryTask(),
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