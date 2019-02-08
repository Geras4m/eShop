using System;
using System.Collections.Generic;

namespace DC.Store.Data.Entities
{
    public partial class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }

        public Category Category { get; set; }
    }
}
