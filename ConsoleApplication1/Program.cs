using System.Collections.ObjectModel;
using Raven.Client.Document;

namespace ConsoleApplication1
{
    internal class Program
    {
        private static void Main()
        {
            var store = new DocumentStore();

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
                session.SaveChanges();
            }
        }
    }
}