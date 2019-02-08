using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DC.Store.Web.Models
{
    public class CategoryViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }

        [NonSerialized]
        public ICollection<ProductViewModel> Product;
    }
}
