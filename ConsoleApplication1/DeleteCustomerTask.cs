namespace ConsoleApplication1
{
    internal class DeleteCustomerTask
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