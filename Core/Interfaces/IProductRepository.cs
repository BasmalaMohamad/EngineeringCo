using Core.Entities;
using Core.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IProductRepository
    {
        Task<Product> GetProductByIdAsync(int id);
        Task<Documentation> GetDocByIdAsync(int id);
        Task<int> CountAsync();
        Task<string> GetProductNameAsync(int carId);

        Task<IReadOnlyList<Product>> GetProductsAsync();
        Task<IReadOnlyList<Product>> GetProductsWithSpecificationsAsync(ProductSpecParams productParams);

    }
}
