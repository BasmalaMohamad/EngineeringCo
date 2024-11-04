using AutoMapper;
using Core.Entities;
using Core.Entities.Consts;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructrue.Data
{
    public class ProductRepository : IProductRepository
    {
        private readonly StoreContext _storeContext;
        private readonly IMapper _mapper;

        public ProductRepository(StoreContext storeContext, IMapper mapper)
        {
            _storeContext = storeContext;
            _mapper = mapper;
        }
        public async Task<Product> GetProductByIdAsync(int id)
        {
            return await _storeContext.Products.FindAsync(id);
        }
        public async Task<Documentation> GetDocByIdAsync(int id)
        {
            return await _storeContext.Documentations.Include(d => d.Product).FirstOrDefaultAsync(d => d.DocumentID == id);
        }

        public async Task<IReadOnlyList<Product>> GetProductsAsync()
        {
            return await _storeContext.Products.Include(p => p.Documentation)
                .Include(p => p.Model).ToListAsync();
        }

        public async Task<int> CountAsync()
        {
            return await _storeContext.Products.Include(p => p.Documentation)
                .Include(p => p.Model).CountAsync();
        }

        public async Task<string> GetProductNameAsync(int carId)
        {
            var name = await _storeContext.Products.Where(p => p.ProductID == carId)
                .Include(p => p.Documentation)
                .Include(p => p.Model)
                .Select(p => p.ProductName.ToString())
                .FirstOrDefaultAsync();
            
            return name;
        }

        public async Task<IReadOnlyList<Product>> GetProductsWithSpecificationsAsync(ProductSpecParams productParams)
        {
            IQueryable<Product> query = _storeContext.Products
                .Include(p => p.Documentation)
                .Include(p => p.Model);

            query = query.Where(p =>
            (string.IsNullOrEmpty(productParams.SearchValue)
            || p.ProductName.ToLower().Contains(productParams.SearchValue)
            || p.Construction.ToLower().Contains(productParams.SearchValue))
            && (!productParams.documentId.HasValue || p.Documentation.DocumentID == productParams.documentId)
            && (!productParams.modelId.HasValue || p.ModelId == productParams.modelId));

            if(productParams.sortBy.ToLower() == "name")
                productParams.sortBy = SortByOptions.Name;

            query = query.OrderBy($"{productParams.sortBy} {productParams.sortDirection}");

            var products = await query.Skip(productParams.PageSize * (productParams.PageIndex - 1))
                                  .Take(productParams.PageSize)
                                  .ToListAsync();

            return products;

        }
    }
}
