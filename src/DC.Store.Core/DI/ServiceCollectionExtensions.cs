using DC.Store.Core.Services;
using DC.Store.Core.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace DC.Store.Core.DI
{
    public static class ServiceCollectionExtensions
    {
        public static void AddCoreServices(this IServiceCollection services)
        {
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ICategoryService, CategoryService>();
        }
    }
}
