using System;
using System.Transactions;
using Shared.Entities;

namespace ConsoleApplication1.DemoTasks
{
    internal class CreateCustomerAndOrderTask : IDemoTask
    {
        public void Execute()
        {
            string customerId;
            string orderId;
            using (var session = Program.Store.OpenSession())
            {
                using (var t = new TransactionScope())
                {
                	var customer = new Customer {FirstName = "Erik"};
                	session.Store(customer);
                    customerId = customer.Id;

                    session.SaveChanges();

                    //throw new Exception();

                    var order = new Order {CustomerId = customer.Id, Created = DateTime.Now};
                    session.Store(order);
                    orderId = order.Id;

                    session.SaveChanges();

                    t.Complete();
                }
            }

            Console.WriteLine("Customer {0} and order {1} saved!", customerId, orderId);
        }
    }
}