using System;

namespace Shared.Entities
{
    public class Order
    {
        public string Id { get; set; }

        public string CustomerId { get; set; }

    	public DateTime Created { get; set; }
    }
}