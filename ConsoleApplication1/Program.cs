using System;
using Raven.Client;
using Raven.Client.Document;
using Raven.Client.Indexes;

namespace ConsoleApplication1
{
    internal class Program
    {
        private static readonly IDocumentStore _store = InitializeStore();

        private static IDocumentStore InitializeStore()
        {
            var store = new DocumentStore { Url = "http://localhost:8080/" }.Initialize();
            IndexCreation.CreateIndexes(typeof (Program).Assembly, store);
            return store;
        }

        public static IDocumentStore Store
        {
            get { return _store; }
        }

        private static void Main()
        {
            var tasks = new IDemoTask[]
                            {
                                new CleanupTask(),
                                new CreateCustomerTask(),
                                new QueryOnStaticIndexTask(),
                                //new QueryForCustomerTask(),
                                //new CreateCustomerAndOrderTask(),
                                //new SessionCachingStuffTask(),
                                //new DoStuffWithProductsTask(),
                                //new QueryEverythingTask(),
                                //new StaleResultTask(),
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
    }
}