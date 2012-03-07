﻿using System;
using System.Collections.ObjectModel;
using System.Linq;
using Shared.Entities;

namespace ConsoleApplication1.DemoTasks
{
    internal class CreateCustomerTask : IDemoTask
    {
        public void Execute()
        {
            string id;
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
                session.Store(new Customer
                                  {
                                      FirstName = "Mattias",
                                      LastName = "Jonsson",
                                      AcceptNewsletter = true,
                                      Addresses = new Collection<Address>
                                                      {
                                                          new Address
                                                              {
                                                                  Street = "Tågagatan",
                                                                  City = "Helsingborg"
                                                              }
                                                      }
                                  });
                id = customer.Id;
                Console.WriteLine(id);
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