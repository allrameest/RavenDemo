using ConsoleApplication1.Entities;

namespace ConsoleApplication1
{
    internal class CleanupTask : IDemoTask
    {
        public void Execute()
        {
            using (var session = Program.Store.OpenSession())
            {
                foreach (var customer in session.Query<Customer>())
                {
                    session.Delete(customer);
                }

                session.SaveChanges();
            }
        }
    }
}