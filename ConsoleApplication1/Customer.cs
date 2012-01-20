using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ConsoleApplication1
{
    public class Customer
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public DateTime? BirthDate { get; set; }

        public bool AcceptNewsletter { get; set; }

        public int ShoeSize { get; set; }

        public Collection<Address> Addresses { get; set; }
    }
}