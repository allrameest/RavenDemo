using System;
using System.Collections.ObjectModel;
using System.Linq;
using ConsoleApplication1.Entities;

namespace ConsoleApplication1
{
    internal class CreateCustomerAndOrderTask : IDemoTask
    {
        public void Execute()
        {
            int id;
            using (var session = Program.Store.OpenSession())
            {
                var customer = new Customer
                                   {
                                       FirstName = "Erik",
                                       LastName = "Juhlin",
                                       Email = "erik@juhlin.nu",
                                       AcceptNewsletter = true,
                                       Addresses = new Collection<Address>
                                                       {
                                                           new Address
                                                               {
                                                                   Street = "Helmfeltsgatan 7",
                                                                   City = "Helsingborg"
                                                               }
                                                       }
                                   };

                session.Store(customer);
                id = customer.Id;

                session.SaveChanges();

                var order = new Order {Customer = customer};
                session.Store(order);

                session.SaveChanges();
            }


            using (var session = Program.Store.OpenSession())
            {
                var customer = session.Load<Customer>(id);
                Console.WriteLine(customer.FirstName + " " + customer.Addresses.First().City);
            }
        }
    }
}