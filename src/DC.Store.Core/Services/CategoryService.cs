using DC.Store.Core.Model;
using DC.Store.Core.Services.Interfaces;
using DC.Store.Data.Entities;
using DC.Store.Data.Repositories.Interfaces;

namespace DC.Store.Core.Services
{
    internal class CategoryService : BaseService<CategoryDto, Category>, ICategoryService
    {
        public CategoryService(ICategoryRepository categoryRepository)
            : base(categoryRepository)
        {
        }
    }
}
