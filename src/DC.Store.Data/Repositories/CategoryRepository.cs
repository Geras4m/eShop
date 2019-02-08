using DC.Store.Data.Entities;
using DC.Store.Data.Repositories.Interfaces;

namespace DC.Store.Data.Repositories
{
    internal class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(DcStoreContext context) : base(context)
        {
        }
    }
}
