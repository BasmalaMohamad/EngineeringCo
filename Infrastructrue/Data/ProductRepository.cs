using AutoMapper;
using Core.Entities;
using Core.Entities.Consts;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Dynamic.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.ConstrainedExecution;

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
            return await _storeContext.Products.Include(p => p.Documentation).FirstOrDefaultAsync(p => p.ProductID == id);
        }
        public async Task<Documentation> GetDocByIdAsync(int id)
        {
            return await _storeContext.Documentations.FirstOrDefaultAsync(d => d.DocumentID == id);
        }

        public async Task<IReadOnlyList<Product>> GetProductsAsync()
        {
            return await _storeContext.Products.Include(p => p.Documentation)
                .ToListAsync();
        }

        public async Task<int> CountAsync()
        {
            return await _storeContext.Products.Include(p => p.Documentation)
               .CountAsync();
        }

        public async Task<string> GetProductNameAsync(int productId)
        {
            var name = await _storeContext.Products.Where(p => p.ProductID == productId)
                .Include(p => p.Documentation)
                .Select(p => p.ProductName)
                .FirstOrDefaultAsync();
            
            return name;
        }

        public async Task<IReadOnlyList<Product>> GetProductsWithSpecificationsAsync(ProductSpecParams productParams)
        {
            
            IQueryable<Product> query = _storeContext.Products.Include(p => p.Documentation);
            query = query.Where(p =>
              (string.IsNullOrEmpty(productParams.SearchValue)
                  ||
                   p.ProductName.ToLower().Contains(productParams.SearchValue.ToLower()) ||
                   p.Model.ToLower().Contains(productParams.SearchValue.ToLower()) ||
                   p.Construction.ToLower().Contains(productParams.SearchValue.ToLower()))
                  && (!productParams.documentId.HasValue || p.Documentation.DocumentID == productParams.documentId)
                  && (string.IsNullOrEmpty(productParams.model) || p.Model.ToLower() == productParams.model.ToLower())

                  && (!productParams.inletSizeFrom.HasValue || p.InletSize >= productParams.inletSizeFrom)
                  && (!productParams.inletSizeTo.HasValue || p.InletSize <= productParams.inletSizeTo)
                  && (!productParams.outletSizeTo.HasValue || p.OutletSize <= productParams.outletSizeTo)
                  && (!productParams.outletSizeFrom.HasValue || p.OutletSize >= productParams.outletSizeFrom)
                  && (string.IsNullOrEmpty(productParams.construction) || p.Construction == productParams.construction)
     
                  && (string.IsNullOrEmpty(productParams.productName) || p.ProductName == productParams.productName)
                    // Use ToString() for enum comparison

                );


            if (productParams.sortBy.ToLower() == "name")
                productParams.sortBy = SortByOptions.Name;
            if (productParams.sortBy.ToLower() == "inlet")
                productParams.sortBy = SortByOptions.Inlet;
            if (productParams.sortBy.ToLower() == "outlet")
                productParams.sortBy = SortByOptions.Outlet;

            query = query.OrderBy($"{productParams.sortBy} {productParams.sortDirection}");

            var products = await query.Skip(productParams.PageSize * (productParams.PageIndex - 1))
                                  .Take(productParams.PageSize)
                                  .ToListAsync();


            return products;
        }

        public async Task<Product> AddProduct(Product product)
        {
            _storeContext.Products.Add(product);
            await _storeContext.SaveChangesAsync();
            return product; 
        }

        public async Task RemoveProduct(int id)
        {
            var product = await GetProductByIdAsync(id);
            if (product == null)
            {
                return;
            }

            _storeContext.Products.Remove(product);
            _storeContext.SaveChanges();

        }

        public async Task<Product> EditProduct(int productID, Product updatedproduct)
        {
            var product = await GetProductByIdAsync(productID);
            product.ProductName = updatedproduct.ProductName;
            product.Model = updatedproduct.Model;
            product.OutletSize = updatedproduct.OutletSize;
            product.InletSize = updatedproduct.InletSize;
            product.Construction = updatedproduct.Construction;
            product.ImageURL = updatedproduct.ImageURL;
            product.Documentation.FileURL = updatedproduct.Documentation.FileURL;
            _storeContext.Products.Update(product);
            await _storeContext.SaveChangesAsync();
            return product;
        }
        public async Task<bool> SaveAsync()
        {
            var saved = await _storeContext.SaveChangesAsync();
            return saved > 0 ? true : false;
        }
    }
    }

