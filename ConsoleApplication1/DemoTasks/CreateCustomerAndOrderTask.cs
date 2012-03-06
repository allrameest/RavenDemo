using System;
using System.Transactions;
using Shared.Entities;

namespace ConsoleApplication1.DemoTasks
{
    internal class CreateCustomerAndOrderTask : IDemoTask
    {
        public void Execute()
        {
            using (var session = Program.Store.OpenSession())
            {
                using (var t = new TransactionScope())
                {
                	var customer = new Customer {FirstName = "Erik"};
                	session.Store(customer);

                    session.SaveChanges();

                    //throw new Exception();

                    session.Store(new Order {CustomerId = customer.Id, Created = DateTime.Now});

                    session.SaveChanges();

                    t.Complete();
                }
            }
        }
    }
}