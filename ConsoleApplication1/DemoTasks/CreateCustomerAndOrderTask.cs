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

            using (var t = new TransactionScope())
            {
                using (var session = Program.Store.OpenSession())
                {
                    var customer = new Customer {FirstName = "Erik"};
                    session.Store(customer);
                    customerId = customer.Id;

                    session.SaveChanges();
                }

                //throw new Exception();

                using (var session = Program.Store.OpenSession())
                {
                    var order = new Order {CustomerId = customerId, Created = DateTime.Now};
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