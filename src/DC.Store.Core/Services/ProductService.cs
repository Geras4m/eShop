using DC.Store.Core.Model;
using DC.Store.Core.Services.Interfaces;
using DC.Store.Data.Entities;
using DC.Store.Data.Repositories.Interfaces;
using System.Threading.Tasks;
using static AutoMapper.Mapper;

namespace DC.Store.Core.Services
{
    internal class ProductService : BaseService<ProductDto, Product>, IProductService
    {
        readonly IProductRepository _productRepository;
        public ProductService(IProductRepository productRepository)
            : base(productRepository)
        {
            _productRepository = productRepository;
        }

        /// <summary>
        /// Returns Product by ID including Category.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ProductDto> GetWithCategoryAsync(int? id) =>
            await OnSelectWithCategory(id);

        public async Task<ProductDto> OnSelectWithCategory(int? id)
        {
            var product = await _productRepository.SelectDeepAsync(id);
            var result = Map<Product, ProductDto>(product);

            return result;
        }

    }
}
