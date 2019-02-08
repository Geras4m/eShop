using DC.Store.Data.Entities;
using DC.Store.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace DC.Store.Data.Repositories
{
    internal class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        public ProductRepository(DcStoreContext context) : base(context)
        {
        }

        public async Task<Product> SelectDeepAsync(int? id)
        {            
            var x = _context.Set<Product>().Include(e => e.Category);
            return await x.Where(p => p.Id == id).FirstOrDefaultAsync();
        }
    }
}
