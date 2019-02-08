using DC.Store.Data.Repositories;
using DC.Store.Data.Repositories.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace DC.Store.Data.DI
{
    public static class ServiceCollectionExtensions
    {
        public static void AddRepositoryServices(this IServiceCollection services)
        {
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
        }
    }
}
