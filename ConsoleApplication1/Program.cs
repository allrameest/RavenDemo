using System;
using Raven.Client;
using Raven.Client.Document;

namespace ConsoleApplication1
{
    internal class Program
    {
        private static readonly IDocumentStore _store = InitializeStore();

        private static IDocumentStore InitializeStore()
        {
            return new DocumentStore {Url = "http://localhost:8080/"}.Initialize();
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
                                new QueryForCustomerTask(),
                                new CreateCustomerAndOrderTask(),
                                new SessionCachingStuffTask(),
                                new DoStuffWithProductsTask(),
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