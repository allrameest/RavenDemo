using System;
using System.Collections.ObjectModel;
using System.Linq;
using Raven.Client.Document;

namespace ConsoleApplication1
{
    internal class Program
    {
        private static void Main()
        {
            var store = new DocumentStore() {Url = "http://localhost:8080/"}.Initialize();

            int id;
            using (var session = store.OpenSession())
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
                Console.WriteLine(id);

                session.SaveChanges();
            }


            using (var session = store.OpenSession())
            {
                var customer = session.Load<Customer>(id);
                Console.WriteLine(customer.FirstName + " " + customer.Addresses.First().City);
            }
            
            Console.ReadKey();
        }
    }
}