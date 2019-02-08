using System.Collections.Generic;

namespace DC.Store.Core.Model
{
    public class CategoryDto
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public ICollection<ProductDto> Product { get; set; }
    }
}
