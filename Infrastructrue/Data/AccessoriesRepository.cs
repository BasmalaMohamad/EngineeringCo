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
    public class AccessoriesRepository : IAccessoryRepository
    {
        private readonly StoreContext _storeContext;
        private readonly IMapper _mapper;
        public AccessoriesRepository(StoreContext storeContext, IMapper mapper)
        {
            _storeContext = storeContext;
            _mapper = mapper;
        }
        public async Task<int> CountAsync()
        {
            return await _storeContext.Accessories.Include(p => p.Category)
               .CountAsync();
        }

        public async Task<IReadOnlyList<Accessories>> GetAccessoriesAsync()
        {
            return await _storeContext.Accessories.Include(p => p.Category)
                            .ToListAsync();
        }

        public async Task<IReadOnlyList<Accessories>> GetAccessoriesWithSpecificationsAsync(AccessoriesSpecParams accessoriesParams)
        {
            IQueryable<Accessories> query = _storeContext.Accessories.Include(p => p.Category);
            query = query.Where(p =>
              (string.IsNullOrEmpty(accessoriesParams.SearchValue)
                  ||
                   p.PumpName.ToLower().Contains(accessoriesParams.SearchValue.ToLower()) ||
                   p.Model.ToLower().Contains(accessoriesParams.SearchValue.ToLower()) ||
                   p.Construction.ToLower().Contains(accessoriesParams.SearchValue.ToLower()))
                  && (!accessoriesParams.CategoryId.HasValue || p.Category.Id == accessoriesParams.CategoryId)
                  && (string.IsNullOrEmpty(accessoriesParams.Model) || p.Model.ToLower() == accessoriesParams.Model.ToLower())

                  && (!accessoriesParams.SizeFrom.HasValue || p.Size >= accessoriesParams.SizeFrom)
                  && (!accessoriesParams.SizeTo.HasValue || p.Size <= accessoriesParams.SizeTo)
                  && (string.IsNullOrEmpty(accessoriesParams.Construction) || p.Construction == accessoriesParams.Construction)

                  && (string.IsNullOrEmpty(accessoriesParams.PumpName) || p.PumpName == accessoriesParams.PumpName)
                // Use ToString() for enum comparison
                );


            if (accessoriesParams.sortBy.ToLower() == "pumpname")
                accessoriesParams.sortBy = AccesSortByOptions.Name;
            if (accessoriesParams.sortBy.ToLower() == "size")
                accessoriesParams.sortBy = AccesSortByOptions.Size;
            if (accessoriesParams.sortBy.ToLower() == "model")
                accessoriesParams.sortBy = AccesSortByOptions.Model;

            query = query.OrderBy($"{accessoriesParams.sortBy} {accessoriesParams.sortDirection}");

            var products = await query.Skip(accessoriesParams.PageSize * (accessoriesParams.PageIndex - 1))
                                  .Take(accessoriesParams.PageSize)
                                  .ToListAsync();


            return products;
        }

        public async Task<Accessories> GetAccessoryByIdAsync(int id)
        {
            return await _storeContext.Accessories.Include(p => p.Category).FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<string> GetAccessoryNameAsync(int id)
        {
            var name = await _storeContext.Accessories.Where(p => p.Id == id)
                            .Include(p => p.Category)
                            .Select(p => p.PumpName)
                            .FirstOrDefaultAsync();

            return name;
        }
    }
}
