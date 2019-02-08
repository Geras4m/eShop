using DC.Store.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DC.Store.Data.Repositories.Interfaces
{
    public interface IProductRepository : IBaseRepository<Product>
    {
        Task<Product> SelectDeepAsync(int? id);
    }
}
