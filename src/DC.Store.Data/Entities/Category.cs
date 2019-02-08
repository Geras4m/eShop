using System;
using System.Collections.Generic;

namespace DC.Store.Data.Entities
{
    public partial class Category
    {
        public Category()
        {
            Product = new HashSet<Product>();
        }

        public int Id { get; set; }
        public string Title { get; set; }

        public ICollection<Product> Product { get; set; }
    }
}
