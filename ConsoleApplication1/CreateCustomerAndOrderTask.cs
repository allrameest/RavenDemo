using System.Transactions;
using ConsoleApplication1.Entities;

namespace ConsoleApplication1
{
    internal class CreateCustomerAndOrderTask : IDemoTask
    {
        public void Execute()
        {
            using (var session = Program.Store.OpenSession())
            {
                using (var t = new TransactionScope())
                {
                    session.Store(new Customer {FirstName = "Erik"});

                    session.SaveChanges();

                    //throw new Exception();

                    session.Store(new Order {Customer = new Customer {FirstName = "Erik"}});

                    session.SaveChanges();

                    t.Complete();
                }
            }
        }
    }
}