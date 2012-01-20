using System;
using Raven.Client;
using Raven.Client.Document;

namespace ConsoleApplication1
{
    internal class Program
    {
        private static readonly IDocumentStore _store = new DocumentStore {Url = "http://localhost:8080/"}.Initialize();

        public static IDocumentStore Store
        {
            get { return _store; }
        }

        private static void Main()
        {
            new DeleteCustomerTask().Execute();
            new CreateCustomerTask().Execute();
            new QueryForCustomerTask().Execute();

            Console.ReadKey();
        }
    }
}