using Raven.Client;
using Shared.Entities;

namespace ConsoleApplication1
{
    internal class CleanupTask : IDemoTask
    {
        public void Execute()
        {
            using (var session = Program.Store.OpenSession())
            {
                Cleanup<Customer>(session);
                Cleanup<Order>(session);
                Cleanup<Product>(session);
                session.SaveChanges();
            }
        }

        private static void Cleanup<T>(IDocumentSession session)
        {
            foreach (var item in session.Query<T>())
            {
                session.Delete(item);
            }
        }
    }
}