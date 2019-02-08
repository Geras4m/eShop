using DC.Store.Core.Model;
using System.Threading.Tasks;

namespace DC.Store.Core.Services.Interfaces
{
    public interface IProductService : IBaseService<ProductDto>
    {
        Task<ProductDto> GetWithCategoryAsync(int? id);
    }
}
