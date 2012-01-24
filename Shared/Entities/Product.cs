using System.Collections.Generic;

namespace Shared.Entities
{
    public class Product
    {
        private IDictionary<string, object> _fields;

        public int Id { get; set; }

        public string Title { get; set; }

        public IDictionary<string, object> Fields
        {
            get { return _fields ?? (_fields = new Dictionary<string, object>()); }
            set { _fields = value; }
        }
    }
}